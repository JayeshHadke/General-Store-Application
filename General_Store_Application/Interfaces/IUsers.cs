using General_Store_Application.Models;

namespace General_Store_Application.Interfaces
{
	public interface IUsers
	{
		public List<User> addUser(User user);
		public List<User> deleteUser(int userId);
		public List<User> manageUser(int userId, User user);
	}
}
