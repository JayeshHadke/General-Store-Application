using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using System.Data.SqlClient;

namespace My_Store_API.Services
{
	public class AuthenticateUserService : IAuthenticateUser
	{
		SqlConnection connection;
		public AuthenticateUserService(IConfiguration configuration)
		{
			connection = new SqlConnection(configuration.GetConnectionString("myStore"));
		}
		public Users? AuthenticateUser(string userName, string password)
		{
			List<Users> users = new List<Users>();
			SqlCommand command = new SqlCommand("select * from users", connection);
			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					users.Add(new Users() { userId = (int)reader["userId"], userName = (string)reader["userName"], password = (string)reader["password"], userRole = (string)reader["userRole"], userEmail = (string)reader["userEmail"], userPhoneNo = (long)reader["userPhoneNo"] });
				}
			}
			catch (Exception ex) { throw; }
			finally
			{
				connection.Close();
			}
			return users.SingleOrDefault(user => user.userName == userName && user.password == password);
		}
	}
}
