using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Store_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		IUsers users;
		public UsersController(IUsers users)
		{
			this.users = users;
		}

		[HttpPost]
		[Route("AddUser")]
		[Authorize(Roles = "admin")]
		public IActionResult addUser([FromBody] User user)
		{
			bool result = users.addUser(user);
			if (result)
			{
				return StatusCode(201, "User Added Successfully");
			}
			else
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpDelete]
		[Route("DeleteUser/{userId}")]
		[Authorize(Roles = "admin")]
		public IActionResult deleteUser([FromBody] int userId)
		{
			bool result = users.deleteUser(userId);
			if (result)
			{
				return StatusCode(201, "User Deleted Successfully");
			}
			else
			{
				return StatusCode(500, "Internal Server Error");
			}

		}

		[HttpPut]
		[Route("ManageUser/{userId}")]
		[Authorize(Roles = "admin")]
		public IActionResult manageUser(int userId, [FromBody] User user)
		{
			bool result = users.manageUser(userId, user);
			if (result)
			{
				return StatusCode(201, "User Modified Successfully");
			}
			else
			{
				return StatusCode(500, "Internal Server Error");
			}

		}
	}
}
