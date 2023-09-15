using General_Store_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Store_API.Models;
using My_Store_API.Services;

namespace My_Store_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoreController : ControllerBase
	{
		StoreService storeService;
		StoreController(StoreService storeService)
		{
			this.storeService = storeService;
		}
		// accessible by anonymous user
		[HttpGet]
		[AllowAnonymous]
		[Route("GetItems")]
		public IActionResult GetItems() { return StatusCode(200, storeService.getItems()); }
		[HttpGet]
		[Route("GetItemsByName/{itemName}")]
		[AllowAnonymous]
		public IActionResult GetItemsByName(string itemName) { return StatusCode(200, storeService.getItemsByName(itemName)); }
		[HttpGet]
		[Route("GetItemsById/{itemId}")]
		[AllowAnonymous]
		public IActionResult GetItemsById(int itemId) { return StatusCode(200, storeService.getItemsById(itemId)); }

		// accessible by all users
		[HttpPost]
		[Route("BuyItem")]
		[Authorize(Roles = "admin,manager,user")]
		public IActionResult BuyItem([FromBody] BuyItemRequest itemRequest) { return StatusCode(200, BuyItem(itemRequest)); }
		//[HttpPost]
		//[Route("BuyItems")]
		//[Authorize(Roles = "admin,manager,user")]
		//public IActionResult BuyItems([FromBody] List<BuyItemRequest> itemsRequest) { return Ok(); }

		// accessible by admin,manager
		[HttpGet]
		[Route("GetItemsStore")]
		[Authorize(Roles = "admin,manager")]
		public IActionResult GetItemsStore() { return Ok(storeService.getItemsStore()); }
		[HttpGet]
		[Route("GetItemsByNameStore/{itemName}")]
		[Authorize(Roles = "admin,manager")]
		public IActionResult GetItemsByNameStore(string itemName) { return Ok(storeService.getItemsByNameStore(itemName)); }
		[HttpGet]
		[Route("GetItemsByIdStore/{itemId}")]
		[Authorize(Roles = "admin,manager")]
		public IActionResult GetItemsByIdStore(int itemId) { return Ok(storeService.getItemsByIdStore(itemId)); }
		[HttpPost]
		[Route("AddItemStore")]
		[Authorize(Roles = "admin,manager")]
		public IActionResult AddItemStore([FromBody] Items item) { return Ok(storeService.addItemStore(item)); }
		[HttpPost]
		[Route("AddItemsStore")]
		[Authorize(Roles = "admin,manager")]
		public IActionResult AddItemsStore([FromBody] List<Items> items) { return Ok(storeService.addItemsStore(items)); }
		[HttpPut]
		[Route("KanageItemStore")]
		[Authorize(Roles = "admin,manager")]
		public IActionResult ManageItemStore([FromBody] Items item) { return Ok(storeService.manageItemStore(item)); }

		// accessible by admin
		[HttpDelete]
		[Route("DeleteItemStore/{itmeId}")]
		[Authorize(Roles = "admin")]
		public IActionResult DeleteItemStore(int itemId) { return Ok(storeService.deleteItemStore(itemId)); }

	}
}
