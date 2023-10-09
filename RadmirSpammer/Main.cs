using RadmirSpammer.Services;
using System.Collections.Concurrent;
using static System.Windows.Forms.LinkLabel;

namespace RadmirSpammer
{
	public partial class Main : Form
	{
		private readonly List<RadmirAccountCredentials> forumAccounts = new List<RadmirAccountCredentials>();
		private readonly ConcurrentBag<string> linksToSpam = new ConcurrentBag<string>();

		private readonly ConcurrentBag<EmailCredentials> emailAccountsToConfirm = new ConcurrentBag<EmailCredentials>();

		private readonly ConcurrentQueue<string> proxyList = new ConcurrentQueue<string>();

		private CancellationTokenSource emailCTS = new();
		private CancellationTokenSource accountCTS = new();

		private int messagesSend = 0;
		private int emailConfirmed = 0;

		private string accountsFilePath = null!;

		private object accountFileLocker = new object();

		public Main()
		{
			InitializeComponent();
		}

		private void loadAccounts_Click(object sender, EventArgs e)
		{
			try
			{
				ofd.Title = "Загрузить аккаунты";

				var result = ofd.ShowDialog();

				if (result != DialogResult.OK)
				{
					MessageBox.Show("Вы не выбрали файл с аккаунтами!");
					return;
				}
				accountsFilePath = ofd.FileName;
				var accountsUnparsed = File.ReadAllLines(accountsFilePath);

				var accounts = accountsUnparsed
					.Select(x =>
					{
						var splitted = x.Split(':');
						return new RadmirAccountCredentials()
						{
							Email = splitted[0],
							Password = splitted[1]
						};
					});

				forumAccounts.AddRange(accounts);

				MessageBox.Show("Аккаунты успешно загружены!");

				loadedAccountsLabel.Text = forumAccounts.Count.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Аккаунты не получилось загрузить.\nПричина: {ex}");
			}
		}

		private void loadLinks_Click(object sender, EventArgs e)
		{
			try
			{
				ofd.Title = "Загрузить ссылки";

				var result = ofd.ShowDialog();

				if (result != DialogResult.OK)
				{
					MessageBox.Show("Вы не выбрали файл с ссылками!");
					return;
				}

				var links = File
					.ReadAllLines(ofd.FileName)
					.OrderBy(_ => Random.Shared.Next());

				foreach (var link in links)
				{
					linksToSpam.Add(link);
				}

				MessageBox.Show("Ссылки успешно загружены!");

				linksLoadedLabel.Text = linksToSpam.Count.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ссылки не получилось загрузить.\nПричина: {ex}");
			}
		}

		private void loadProxy_Click(object sender, EventArgs e)
		{
			try
			{
				ofd.Title = "Загрузить прокси";

				var result = ofd.ShowDialog();

				if (result != DialogResult.OK)
				{
					MessageBox.Show("Вы не выбрали файл с прокси!");
					return;
				}

				var proxies = File.ReadAllLines(ofd.FileName);

				foreach (var proxy in proxies)
				{
					proxyList.Enqueue(proxy);
				}

				MessageBox.Show("Прокси успешно загружены!");

				proxyLoadedLabel.Text = proxyList.Count.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Прокси не получилось загрузить.\nПричина: {ex}");
			}
		}

		private void loadEmails_Click(object sender, EventArgs e)
		{
			try
			{
				ofd.Title = "Загрузить почты для подтверждения аккаунтов";

				var result = ofd.ShowDialog();

				if (result != DialogResult.OK)
				{
					MessageBox.Show("Вы не выбрали файл с почтами!");
					return;
				}

				var emailsUnparsed = File.ReadAllLines(ofd.FileName);

				var emails = emailsUnparsed
					.Select(x =>
					{
						var splitted = x.Split(':');
						return new EmailCredentials()
						{
							MailAddress = splitted[0],
							MailPassword = splitted[1]
						};
					});

				foreach (var email in emails)
				{
					emailAccountsToConfirm.Add(email);
				}

				MessageBox.Show("Почты успешно загружены!");

				emailsLoadedLabel.Text = emailAccountsToConfirm.Count.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Почты не получилось загрузить.\nПричина: {ex}");
			}
		}

		private async void startEmailBots_Click(object sender, EventArgs e)
		{
			startEmailBots.Enabled = false;
			stopEmailBots.Enabled = true;

			loadEmails.Enabled = false;

			var emailConfirmator = Program.serviceContainer.Resolve<IEmailConfirmator>();

			await Parallel.ForEachAsync(emailAccountsToConfirm, emailCTS.Token,
				async (x, ct) =>
				{
					if (ct.IsCancellationRequested)
						return;

					await ForumBot.ConfirmMail(emailConfirmator, x);

					emailConfirmed++;
					emailsConfirmedLabel.Text = emailConfirmed.ToString();
				});

			MessageBox.Show("Закончили подтверждать почты.");
		}

		private async void stopEmailBots_Click(object sender, EventArgs e)
		{
			startEmailBots.Enabled = true;
			stopEmailBots.Enabled = false;

			loadEmails.Enabled = true;

			emailCTS.Cancel();
			MessageBox.Show("Останавливаем");
		}

		private async void startBots_Click(object sender, EventArgs e)
		{
			messagesSend = 0;

			stopBots.Enabled = true;
			startBots.Enabled = false;

			headerTextBox.Enabled = false;
			bodyTextBox.Enabled = false;
			loadAccounts.Enabled = false;
			loadLinks.Enabled = false;
			loadEmails.Enabled = false;
			loadProxy.Enabled = false;

			if (linksToSpam.Count == 0)
			{
				MessageBox.Show("Загружено 0 ссылок. Загрузите ссылки.");
				return;
			}
			if (forumAccounts.Count == 0)
			{
				MessageBox.Show("Загружено 0 аккаунтов. Загрузите аккаунты.");
				return;
			}
			if (proxyList.IsEmpty)
			{
				var result = MessageBox.Show("Загружено 0 прокси.\nВы уверены что хотите продолжить?", "Нет прокси.", MessageBoxButtons.YesNo);
				if (result == DialogResult.No)
					return;
			}

			var threadSettings = new ThreadSettigns
			{
				Header = headerTextBox.Text,
				Body = bodyTextBox.Text
			};

			await Parallel.ForEachAsync(forumAccounts.ToList(), accountCTS.Token,
				async (account, ct) =>
				{
					var randomedLiks = linksToSpam.OrderBy(x => Random.Shared.Next());

					foreach (var link in randomedLiks)
					{
						if (ct.IsCancellationRequested)
							return;

						try
						{
							using var bot = new ForumBot(link, account, threadSettings, proxyList);
							await bot.SpamMessage();

							messagesSend++;
							messagesSendLabel.Invoke(() => messagesSendLabel.Text = messagesSend.ToString());

							await Task.Delay(30000, ct);
						}
						catch
						{
							lock (accountFileLocker)
							{
								forumAccounts.Remove(account);

								File.WriteAllLines(accountsFilePath, forumAccounts.Select(x => $"{x.Email}:{x.Password}"));

								loadedAccountsLabel.Invoke(() => loadedAccountsLabel.Text = forumAccounts.Count.ToString());
							}
							return;
						}
					}
				});

			MessageBox.Show("Закончили со всеми темами.");

			stopBots.Enabled = false;
			startBots.Enabled = true;

			headerTextBox.Enabled = true;
			bodyTextBox.Enabled = true;
			loadAccounts.Enabled = true;
			loadLinks.Enabled = true;
			loadEmails.Enabled = true;
			loadProxy.Enabled = true;
		}

		private async void stopBots_Click(object sender, EventArgs e)
		{
			stopBots.Enabled = false;
			startBots.Enabled = true;

			headerTextBox.Enabled = true;
			bodyTextBox.Enabled = true;
			loadAccounts.Enabled = true;
			loadLinks.Enabled = true;
			loadEmails.Enabled = true;
			loadProxy.Enabled = true;

			accountCTS.Cancel();
			MessageBox.Show("Останавливаем");
		}
	}
}