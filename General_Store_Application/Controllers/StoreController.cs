using General_Store_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Store_API.Models;

namespace My_Store_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoreController : ControllerBase
	{

		// accessible by anonymous user
		[HttpGet]
		[AllowAnonymous]
		[Route("getItems")]
		public IActionResult getItems()
		{
			return Ok();
		}
		public IActionResult getItemsByName(string itemName) { return Ok(); }
		public IActionResult getItemsById(int itemId) { return Ok(); }

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
