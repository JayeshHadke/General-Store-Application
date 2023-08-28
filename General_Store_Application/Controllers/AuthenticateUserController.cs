using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Store_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateUserController : ControllerBase
	{
		IConfiguration configuration;
		IAuthenticateUser authenticateUser;
		AuthenticateUserController(IConfiguration configuration, IAuthenticateUser authenticateUser)
		{
			this.configuration = configuration;
			this.authenticateUser = authenticateUser;
		}
		[AllowAnonymous]
		[HttpPost]
		public IActionResult AuthenticateUser([FromBody] Users user)
		{
			Users? user1 = authenticateUser.AuthenticateUser(user.userName, user.password);
			if (user1 != null)
			{
				string jwtToken = getToken(user);

			}
		}
	}
}
