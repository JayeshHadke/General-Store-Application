using General_Store_Application.Interfaces;
using General_Store_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_Store_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateUserController : ControllerBase
	{
		IConfiguration configuration;
		IAuthenticateUser authenticateUser;
		public AuthenticateUserController(IConfiguration configuration, IAuthenticateUser authenticateUser)
		{
			this.configuration = configuration;
			this.authenticateUser = authenticateUser;
		}
		[HttpPost]
		[AllowAnonymous]
		public IActionResult AuthenticateUser([FromBody] ARequest request)
		{
			User? user = authenticateUser.AuthenticateUser(request.userName, request.password);
			if (user != null)
			{
				string jwtToken = getToken(user);
				AResponse respons = new AResponse() { userName = user.userName, token = jwtToken, userRole = user.userRole };
				return Ok(respons);
			}
			else
			{
				return StatusCode(401, "Invalid Credentails");
			}
		}
		private string getToken(User user)
		{
			string issuer = configuration["Jwt:Issuer"];
			string audience = configuration["Jwt:Audience"];
			byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
			SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
			ClaimsIdentity subjet = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.userName), new Claim(ClaimTypes.Role, user.userRole) });
			DateTime expires = DateTime.UtcNow.AddMinutes(5);
			SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor() { Subject = subjet, Expires = expires, Issuer = issuer, Audience = audience, SigningCredentials = signingCredentials };
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);
			string jwtToken = tokenHandler.WriteToken(token);
			return jwtToken;
		}
	}
}
