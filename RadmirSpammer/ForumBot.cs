using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RadmirSpammer.Services;
using System.Collections.Concurrent;

namespace RadmirSpammer
{
	public class ForumBot : IDisposable
	{
		private ChromeDriver? browser;

		private readonly string linkToPost;

		private readonly RadmirAccountCredentials credentials;
		private readonly ThreadSettigns threadSettigns;

		private readonly ConcurrentQueue<string> proxies;

		private string? currentProxy;

		public ForumBot(string linkToPost,
			RadmirAccountCredentials credentials,
			ThreadSettigns threadSettigns,
			ConcurrentQueue<string> proxies)
		{
			this.linkToPost = linkToPost;
			this.credentials = credentials;
			this.threadSettigns = threadSettigns;
			this.proxies = proxies;
		}

		public async Task SpamMessage()
		{
			await Login();

			switch (linkToPost.Split('/')[3])
			{
				case "threads":
					await PostMessage();
					break;
				case "forums":
					await PostThread();
					break;
			}

			await Task.Delay(1000);
		}

		public static async Task ConfirmMail(IEmailConfirmator emailConfirmator, EmailCredentials emailCredentials)
		{
			var chromeDriverService = ChromeDriverService.CreateDefaultService();
			chromeDriverService.HideCommandPromptWindow = true;

			ChromeOptions options = new();
			options.AddArgument("--safebrowsing-enable-enhanced-protection");
			options.AddArgument("--allow-running-insecure-content");
			options.AddArgument("--disable-blink-features=AutomationControlled");
			options.AddUserProfilePreference("safebrowsing.enabled", "true");
			options.AddExcludedArguments("excludeSwitches", "enable-automation");
			options.AddAdditionalOption("useAutomationExtension", "false");
			options.AddArgument("--disable-images");
			#if !DEBUG
			options.AddArgument("-headless");
			#endif

			var browser = new ChromeDriver(chromeDriverService, options);
			browser
				.Manage()
				.Timeouts()
				.PageLoad = TimeSpan.FromMinutes(2);

			var link = await emailConfirmator.GetConfirmLink(emailCredentials);

			browser!.Navigate().GoToUrl(link);

			await AsyncUtils.TryUntilAsync(() => browser.FindElement(By.CssSelector("#logo > a > img")));

			browser.Close();
			browser.Quit();
		}

		private async Task Login()
		{
			await AsyncUtils.DoUntilAsync(async () =>
			{
				ReloadBrowser(currentProxy is null);

				if (browser is not null)
				{
					try
					{
						browser!.Navigate().GoToUrl("https://forum.radmir.games/login");
					}
					catch
					{
						currentProxy = null;

						if (proxies.IsEmpty)
						{
							throw new Exception("Не получается войти в аккаунт. Возможная причина: Закончились прокси");
						}

						return false;
					}

					try
					{
						browser.FindElement(By.CssSelector("#t"));

						currentProxy = null;

						if (proxies.IsEmpty)
						{
							throw new Exception("Не получается войти в аккаунт. Возможная причина: Закончились прокси");
						}

						return false;
					} catch { }

					if (await AsyncUtils.TryUntilAsync(() => browser.FindElement(By.CssSelector("#ctrl_pageLogin_login")), maxTrys: 400))
						return true;
				}
				return false;
			}, 250);

			var emailInput = browser!.FindElement(By.CssSelector("#ctrl_pageLogin_login"));
			var passwordInput = browser.FindElement(By.CssSelector("#ctrl_pageLogin_password"));
			var loginButton = browser.FindElement(By.CssSelector("#pageLogin > dl.ctrlUnit.submitUnit > dd > input"));

			emailInput.SendKeys(credentials.Email);
			passwordInput.SendKeys(credentials.Password);

			loginButton.Click();
		}

		private async Task PostMessage()
		{
			browser!.Navigate().GoToUrl(linkToPost);

			await AsyncUtils.TryUntilAsync(() =>
			{
				try
				{
					browser.FindElement(By.CssSelector("#content > div.pageWidth > div > div.errorOverlay > div > label"));
					return;
				} catch { }

				browser.FindElement(By.CssSelector("#QuickReply > div:nth-child(1) > div > iframe"));
			}, maxTrys: 500);

			var sendButton = browser.FindElement(By.CssSelector("#QuickReply > div.submitUnit > input.button.primary"));

			var frame = browser.SwitchTo()
				.Frame(browser.FindElement(By.CssSelector("#QuickReply > div:nth-child(1) > div > iframe")));

			var textbox = frame.FindElement(By.CssSelector("body"));

			textbox.Click();
			textbox.Clear();
			textbox.SendKeys(threadSettigns.Body);

			frame.SwitchTo().ParentFrame();

			sendButton.Click();
		}

		private async Task PostThread()
		{
			var link = linkToPost + "\\create-thread";

			browser!.Navigate().GoToUrl(link);

			await AsyncUtils.TryUntilAsync(() =>
			{
				try
				{
					browser.FindElement(By.CssSelector("#content > div.pageWidth > div > div.errorOverlay > div > label"));
					return;
				}
				catch { }

				browser.FindElement(By.CssSelector("#ctrl_title_thread_create"));
			}, maxTrys: 500);

			var headerInput = browser.FindElement(By.CssSelector("#ctrl_title_thread_create"));
			var createButton = browser.FindElement(By.CssSelector("#ThreadCreate > dl:nth-child(3) > dd > input.button.primary"));

			headerInput.Clear();
			headerInput.SendKeys(threadSettigns.Header);

			var frame = browser
				.SwitchTo()
				.Frame(browser.FindElement(By.CssSelector("#ThreadCreate > fieldset:nth-child(1) > dl:nth-child(2) > dd > div > div > iframe")));

			var bodyInput = frame.FindElement(By.CssSelector("body"));

			bodyInput.Click();
			bodyInput.Clear();
			bodyInput.SendKeys(threadSettigns.Body);

			frame.SwitchTo().ParentFrame();

			createButton.Click();
		}

		private void ReloadBrowser(bool newProxy = false)
		{
			browser?.Dispose();

			var chromeDriverService = ChromeDriverService.CreateDefaultService();
			chromeDriverService.HideCommandPromptWindow = true;

			ChromeOptions options = new();
			options.AddArgument("--safebrowsing-enable-enhanced-protection");
			options.AddArgument("--allow-running-insecure-content");
			options.AddArgument("--disable-blink-features=AutomationControlled");
			options.AddUserProfilePreference("safebrowsing.enabled", "true");
			options.AddExcludedArguments("excludeSwitches", "enable-automation");
			options.AddAdditionalOption("useAutomationExtension", "false");
			options.AddArgument("--disable-images");
			options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
#if !DEBUG
			options.AddArgument("-headless");
#endif

			if (newProxy && proxies.TryDequeue(out string? proxy))
			{
				options.AddArguments($"--proxy-server={proxy}");
				currentProxy = proxy!;
			}
			else if(currentProxy is not null)
			{
				options.AddArguments($"--proxy-server={currentProxy}");
			}

			browser = new ChromeDriver(chromeDriverService, options);
			browser
				.Manage()
				.Timeouts()
				.PageLoad = TimeSpan.FromMinutes(2);
		}

		public void Dispose()
		{
			browser?.Close();
			browser?.Quit();
			GC.SuppressFinalize(this);
		}
	}
	public record struct RadmirAccountCredentials
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public record struct ThreadSettigns
	{
		public string Header { get; set; }
		public string Body { get; set; }
	}
}
