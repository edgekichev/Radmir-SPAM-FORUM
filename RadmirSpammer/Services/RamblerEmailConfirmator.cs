using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace RadmirSpammer.Services
{
	public class RamblerEmailConfirmator : IEmailConfirmator
	{
		private const string HOSTNAME = "imap.rambler.ru";
		private const int PORT = 993;

		private MimeMessage? FindMessage(ImapClient mailClient, Predicate<MimeMessage> Predicate)
		{
			var folders = mailClient.GetFolders(mailClient.PersonalNamespaces[0]);

			foreach(var folder in folders)
			{
				folder.Open(FolderAccess.ReadOnly);
				var messageIds = folder.Search(SearchQuery.All);
				foreach(var messageId in messageIds)
				{
					var message = folder.GetMessage(messageId);
					if (Predicate(message))
						return message;
				}
				folder.Close();
			}

			return null;
		}

		public async Task<string> GetConfirmLink(EmailCredentials credentials)
		{
			using var mailClient = new ImapClient();

			mailClient.Connect(HOSTNAME, PORT, true);
			mailClient.Authenticate(credentials.MailAddress, credentials.MailPassword);

			var message = FindMessage(mailClient,
				x => x.Headers[HeaderId.Subject].Contains("\"Radmir RP | Forum\""))
				?? throw new Exception("There's no confirmation email.");

			var raw = message.TextBody;

			var rawSplitted = raw.Split('\n');

			return rawSplitted[4];
		}
	}

	public record struct EmailCredentials
	{
		public string MailAddress { get; set; }
		public string MailPassword { get; set; }
	}
}
