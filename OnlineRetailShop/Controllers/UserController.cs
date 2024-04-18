using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        public readonly CommonContext _context;

        public UserController(IConfiguration config, CommonContext context)
        {
            _context = context;
            _config = config;
        }
        [HttpPost]

        public async Task<IActionResult> Post(UserCredentials userCred)
        {
            if (userCred != null && userCred.username != null && userCred.password != null)
            {
                UserCredentials userData = GetUser(userCred.username, userCred.password);
                if (userData == null) { return BadRequest("Invalid Credentials"); }
                if (userCred.username == userData.username && userCred.password == userData.password)
                {

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[] {
                     new Claim(JwtRegisteredClaimNames.Sub, userCred.username),
                     new Claim("ID",userCred.UserId.ToString()),
                     new Claim("PassWord",userCred.password),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

                    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            return BadRequest("Inavalid Credentials");
        }

        [Authorize]
        [Route("Login")]
        [HttpGet]

        public UserCredentials GetUser(String Username, String Password)
        {
            UserCredentials user = null;
            user = _context.UserCredentials.FirstOrDefault(x => x.username == Username);
            if (user.password != Password) return null;
            return user;
        }

    }
}
