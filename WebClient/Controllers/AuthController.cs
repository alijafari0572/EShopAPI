using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AuthController : Controller
    {
        private IHttpClientFactory _client;

        public AuthController(IHttpClientFactory client)
        {
            _client = client;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login_Viewmodel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var client = _client.CreateClient("EShopclient");
            var Jsonbody = JsonConvert.SerializeObject(login);
            var content = new StringContent(Jsonbody, Encoding.UTF8, "application/json");
            var Respons = client.PostAsync("/Api/Auth", content).Result;
            if (Respons.IsSuccessStatusCode)
            {
                var token = Respons.Content.ReadAsAsync<Token_model>().Result;
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,login.UserName),
                    new Claim(ClaimTypes.Name,login.UserName),
                    new Claim("AccessToken",token.Token)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var prenciple = new ClaimsPrincipal(identity);
                var Properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true
                };
                HttpContext.SignInAsync(prenciple, Properties);
                return Redirect("/Home");
            }
            else
            {
                ModelState.AddModelError("Username", "User Not Valid");
                return View(login);
            }
        }
    }
}
