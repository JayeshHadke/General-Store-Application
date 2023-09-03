using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using My_Store_API.Models;
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
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("insert into itmes values(@itemName,@itemPrice,@quantity)", connection);
				command.Parameters.AddWithValue("@itemName", item.itemName);
				command.Parameters.AddWithValue("@itemPrice", item.itemPrice);
				command.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
				command.ExecuteNonQuery();

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

		public BuyItemResponse buyItem(BuyItemRequest itemRequest)
		{
			List<Items> list = new List<Items>();
			list.AddRange(getItems());
			Items? item = list.SingleOrDefault(i => i.itemId == itemRequest.itemId);
			if (item != null)
			{
				BuyItemResponse itemResponse = new BuyItemResponse();
				if (item.itemQuantity >= itemRequest.itemQuantity)
				{
					itemResponse.itemId = item.itemId;
					itemResponse.itemName = item.itemName;
					itemResponse.itemPrice = item.itemPrice;
					itemResponse.itemQuantity = itemRequest.itemQuantity;
					itemResponse.totalPrice = itemRequest.itemQuantity * item.itemPrice;
					itemResponse.message = "Order Placed Successfully!";
					itemResponse.status = true;
					try
					{
						connection.Open();
						SqlCommand command = new SqlCommand("update item set quantity=@quantity where itemId=@itemId", connection);
						command.Parameters.AddWithValue("@quantity", item.itemQuantity - itemRequest.itemQuantity);
						command.Parameters.AddWithValue("@itemId", item.itemId);
						if (command.ExecuteNonQuery() > 0)
						{
							return itemResponse;
						}
						else
						{
							throw new Exception("Internal Server Issue!");
						}
					}
					catch (Exception ex) { throw; }
					finally { connection.Close(); }
				}
				else
				{
					itemResponse.itemId = item.itemId;
					itemResponse.itemName = item.itemName;
					itemResponse.itemPrice = item.itemPrice;
					itemResponse.itemQuantity = itemRequest.itemQuantity;
					itemResponse.totalPrice = itemRequest.itemQuantity * item.itemPrice;
					itemResponse.message = "Requested Quantity is out of stock!";
					itemResponse.status = false;
				}
				return itemResponse;
			}
			return null;
		}

		public List<BuyItemResponse> buyItems(List<BuyItemRequest> itemsRequest)
		{
			throw new NotImplementedException();
		}

		public List<Items> deleteItemStore(int itmeId)
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("delete from items where itemId=@itemId", connection);
				command.Parameters.AddWithValue("@itemId", itmeId);
				if (command.ExecuteNonQuery() > 0)
				{
					List<Items> list = new List<Items>();
					SqlCommand sqlCommand = new SqlCommand("select * from items", connection);
					SqlDataReader reader = sqlCommand.ExecuteReader();
					while (reader.Read())
					{
						list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = (int)reader["quantity"] });
					}
					return list;
				}
				else return null;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> getItems()
		{
			try
			{

				connection.Open();
				SqlCommand command = new SqlCommand("select * from items", connection);
				List<Items> list = new List<Items>();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = (int)reader["quantity"] });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> getItemsById(int itemId)
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("select * from items where itemId= @itemId", connection);
				command.Parameters.AddWithValue("@itemId", itemId);
				List<Items> list = new List<Items>();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = 0 });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> getItemsByIdStore(int itemId)
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("select * from items where itemId= @itemId", connection);
				command.Parameters.AddWithValue("@itemId", itemId);
				List<Items> list = new List<Items>();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = 0 });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> getItemsByName(string itemName)
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("select * from items where itemName= @itemName", connection);
				command.Parameters.AddWithValue("@itemName", itemName);
				List<Items> list = new List<Items>();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = 0 });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> getItemsByNameStore(string itemName)
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("select * from items where itemName= @itemName", connection);
				command.Parameters.AddWithValue("@itemName", itemName);
				List<Items> list = new List<Items>();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = (int)reader["quantity"] });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> getItemsStore()
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("select * from items", connection);
				List<Items> list = new List<Items>();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = (int)reader["quantity"] });
				}
				return list;
			}
			catch (Exception ex) { throw; }
			finally { connection.Close(); }
		}

		public List<Items> manageItemStore(Items item)
		{
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("update items set itemName=@itemName, itemPrice=@itemPrice,quantity=@quantity where itemId=@itemId", connection);
				command.Parameters.AddWithValue("@itemId", item.itemId);
				command.Parameters.AddWithValue("@itemName", item.itemName);
				command.Parameters.AddWithValue("@itemPrice", item.itemPrice);
				command.Parameters.AddWithValue("@quantity", item.itemQuantity);
				if (command.ExecuteNonQuery() > 0)
				{
					command = new SqlCommand("select * from items");
					SqlDataReader reader = command.ExecuteReader();
					List<Items> list = new List<Items>();
					while (reader.Read())
					{
						list.Add(new Items() { itemId = (int)reader["itemId"], itemName = (string)reader["itemName"], itemPrice = (float)reader["itemPrice"], itemQuantity = (int)reader["quantity"] });
					}
					return list;
				}
				else
					return null;

			}
			catch (Exception e) { throw; }
			finally { connection.Close(); }
		}
	}
}
