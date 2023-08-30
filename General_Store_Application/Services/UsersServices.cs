using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using System.Data.SqlClient;

namespace My_Store_API.Services
{
	public class UsersServices : IUsers
	{
		SqlConnection connection;
		public UsersServices(IConfiguration configuration)
		{
			connection = new SqlConnection(configuration.GetConnectionString("myStore"));
		}

		public bool addUser(User user)
		{
			try
			{
				SqlCommand sqlCommand = new SqlCommand("insert into users values(@userName,@password,@userRole,@userPhoneNo,@userEmail)", connection);
				sqlCommand.Parameters.AddWithValue("@userName", user.userName);
				sqlCommand.Parameters.AddWithValue("@password", user.password);
				sqlCommand.Parameters.AddWithValue("@userRole", user.userRole);
				sqlCommand.Parameters.AddWithValue("@userPhoneNo", user.userPhoneNo);
				sqlCommand.Parameters.AddWithValue("@userEmail", user.userEmail);
				connection.Open();
				return sqlCommand.ExecuteNonQuery() > 0 ? true : false;
			}
			catch (Exception e) { throw; }
			finally { connection.Close(); }
		}

		public bool deleteUser(int userId)
		{
			try
			{
				SqlCommand sqlCommand = new SqlCommand("delete from users where userId=@userId", connection);
				sqlCommand.Parameters.AddWithValue("@userId", userId);
				connection.Open();
				return sqlCommand.ExecuteNonQuery() > 0 ? true : false;
			}
			catch (Exception e) { throw; }
			finally { connection.Close(); }
		}

		public bool manageUser(int userId, User user)
		{
			try
			{
				SqlCommand sqlCommand = new SqlCommand("udpate users userName=@userName,password=@password,userRole=@userRole,userPhoneNo=@userPhoneNo,userEmail=@userEmail where userId=@userId", connection);
				sqlCommand.Parameters.AddWithValue("@userId", userId);
				sqlCommand.Parameters.AddWithValue("@userName", user.userName);
				sqlCommand.Parameters.AddWithValue("@password", user.password);
				sqlCommand.Parameters.AddWithValue("@userRole", user.userRole);
				sqlCommand.Parameters.AddWithValue("@userPhoneNo", user.userPhoneNo);
				sqlCommand.Parameters.AddWithValue("@userEmail", user.userEmail);
				connection.Open();
				return sqlCommand.ExecuteNonQuery() > 0 ? true : false;
			}
			catch (Exception e) { throw; }
			finally { connection.Close(); }
		}
	}
}
