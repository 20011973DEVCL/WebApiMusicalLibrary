using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        // private const string SecretKey = "P4DJU08CAv9DlBgxbmrZKpa9iAj12Yj6XUBSrpiFHIn78GUkro";
        // private static readonly string EncodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(SecretKey));

        public LoginController(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            var user = Autheticate(userLogin);

            if (user!=null)
            {
                //Creara Token
                var token = GenerateJwtToken(user);
                HttpContext.Response.Headers.Add("Authorization", $"Bearer {token}");
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private User Autheticate(LoginUser userLogin)
        {
            var currentUser = _db.User.FirstOrDefault(user => user.Username.ToLower() == userLogin.Username.ToLower()
                                && user.Password == userLogin.Password);

            if (currentUser!= null)
            {
                return currentUser;
            }
            return null;
        } 

        private string GenerateJwtToken(User user)
        {
            string SecretKey = _config["Jwt:Key"];
            string EncodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(SecretKey));

            var key = new SymmetricSecurityKey(Convert.FromBase64String(EncodedKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Crear los Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Role,user.Rol)
            };

            //Crear el Token
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}