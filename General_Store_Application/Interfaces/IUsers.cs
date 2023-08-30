using General_Store_Application.Models;

namespace General_Store_Application.Interfaces
{
	public interface IUsers
	{
		public bool addUser(User user);
		public bool deleteUser(int userId);
		public bool manageUser(int userId, User user);
	}
}
