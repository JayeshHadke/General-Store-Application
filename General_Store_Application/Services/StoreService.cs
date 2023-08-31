using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using System.Data.SqlClient;

namespace My_Store_API.Services
{
	public class StoreService : IStore
	{
		SqlConnection connection;
		public StoreService(IConfiguration configuration)
		{
			connection = new SqlConnection(configuration.GetConnectionString("myStore"));
		}
		public List<Items> addItemsStore(List<Items> items)
		{
			try
			{
				connection.Open();
				foreach (var item in items)
				{
					SqlCommand command = new SqlCommand("insert into itmes values(@itemName,@itemPrice,@quantity)", connection);
					command.Parameters.AddWithValue("@itemName", item.itemName);
					command.Parameters.AddWithValue("@itemPrice", item.itemPrice);
					command.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
					command.ExecuteNonQuery();
				}
				List<Items> list = new List<Items>();
				SqlCommand sqlCommand = new SqlCommand("select * from items", connection);
				SqlDataReader reader = sqlCommand.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = (int)reader["quantity"] });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> addItemStore(Items item)
		{
			throw new NotImplementedException();
		}

		public string buyItem(Items item)
		{
			throw new NotImplementedException();
		}

		public string buyItems(List<Items> items)
		{
			throw new NotImplementedException();
		}

		public List<Items> deleteItemStore(int itmeId)
		{
			throw new NotImplementedException();
		}

		public List<Items> getItems()
		{
			throw new NotImplementedException();
		}

		public List<Items> getItemsById()
		{
			throw new NotImplementedException();
		}

		public List<Items> getItemsByIdStore()
		{
			throw new NotImplementedException();
		}

		public List<Items> getItemsByName()
		{
			throw new NotImplementedException();
		}

		public List<Items> getItemsByNameStore()
		{
			throw new NotImplementedException();
		}

		public List<Items> getItemsStore()
		{
			throw new NotImplementedException();
		}

		public List<Items> manageItemStore(int id, Items item)
		{
			throw new NotImplementedException();
		}
	}
}
