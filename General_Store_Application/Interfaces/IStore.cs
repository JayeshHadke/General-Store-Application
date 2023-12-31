﻿using General_Store_Application.Models;
using My_Store_API.Models;

namespace General_Store_Application.Interfaces
{
	public interface IStore
	{

		// accessible by anonymous user
		public List<Items> getItems();
		public List<Items> getItemsByName(string itemName);
		public List<Items> getItemsById(int itemId);

		// accessible by all users
		public BuyItemResponse buyItem(BuyItemRequest itemRequest);
		public List<BuyItemResponse> buyItems(List<BuyItemRequest> itemsRequest);

		// accessible by admin,manager
		public List<Items> getItemsStore();
		public List<Items> getItemsByNameStore(string itemName);
		public List<Items> getItemsByIdStore(int itemId);
		public List<Items> addItemStore(Items item);
		public List<Items> addItemsStore(List<Items> items);
		public List<Items> manageItemStore(Items item);

		// accessible by admin
		public List<Items> deleteItemStore(int itmeId);

	}
}
