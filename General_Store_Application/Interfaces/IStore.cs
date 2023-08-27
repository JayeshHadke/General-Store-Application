using General_Store_Application.Models;

namespace General_Store_Application.Interfaces
{
	public interface IStore
	{

		// accessible by all users
		public List<Items> getItems();
		public List<Items> getItemsByName();
		public List<Items> getItemsById();
		public string buyItem(Items item);
		public string buyItems(List<Items> items);

		// accessible by admin,manager
		public List<Items> getItemsStore();
		public List<Items> getItemsByNameStore();
		public List<Items> getItemsByIdStore();
		public List<Items> addItemStore(Items item);
		public List<Items> addItemsStore(List<Items> items);
		public List<Items> manageItemStore(int id, Items item);

		// accessible by admin
		public List<Users> addUserStore(Users user);
		public List<Items> deleteItemStore(int itmeId);
		public List<Users> deleteUserStore(int userId);
		public List<Users> manageUserStore(int userId, Users user);

	}
}
