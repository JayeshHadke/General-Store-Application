using General_Store_Application.Models;

namespace General_Store_Application.Interfaces
{
	public interface IAuthenticateUser
	{
		public User? AuthenticateUser(string userName, string password);
	}
}
