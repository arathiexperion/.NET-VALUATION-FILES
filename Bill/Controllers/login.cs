using Bill.viewmodel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class login : ControllerBase
    {
        private readonly IConfiguration _config;

        public login(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("token")]
        public IActionResult Login([FromBody] User user)
        {
            //checking Uauthorised
            IActionResult response = Unauthorized();
            //Authenticate the User
            var loginUser = AuthenticateUser(user);
            if (loginUser != null)
            {
                var tokenString = GenerateJWToken(loginUser);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        //5 Generate jwt Token
        private string GenerateJWToken(User loginUser)
        {
            //securityKey
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //signing credential
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claims-- Roles

            //generate token
            var token = new JwtSecurityToken(
               _config["Jwt:Issuer"],
               _config["Jwt:Issuer"],
               expires: DateTime.Now.AddMinutes(2),
               signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        //4Authenticate user
        private User AuthenticateUser(User user)
        {
            User loginUser = null;
            //validate the credentials 
            //Retrieve data from the db
            if (user.UserName == "Arathi")
            {
                loginUser = new User
                {
                    UserName = "Arathi",
                    UserEmail = "aswin@gmail.com",
                    DateOfJoining = new DateTime(2021, 12, 20),
                    Role = "Administrator"
                };


            }
            return loginUser;
        }


    }
}
