using EShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] Login_model login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model is't currect");
            }
            if (login.UserName.ToLower() != "ali" || login.PassWord.ToLower() != "1234")
            {
                return Unauthorized();
            }
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyTopLearn"));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var tokenoption=new JwtSecurityToken(
                issuer: "http://localhost:40116",
                claims : new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,login.UserName),
                        new Claim(ClaimTypes.Role,"Admin")
                    },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
                );
            var tokenstring = new JwtSecurityTokenHandler().WriteToken(tokenoption);
            return Ok(new {token=tokenstring});  
        }
    }
}
