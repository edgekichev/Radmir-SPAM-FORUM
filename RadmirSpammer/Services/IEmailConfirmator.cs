namespace RadmirSpammer.Services
{
	public interface IEmailConfirmator
	{
		Task<string> GetConfirmLink(EmailCredentials credentials);
	}
}