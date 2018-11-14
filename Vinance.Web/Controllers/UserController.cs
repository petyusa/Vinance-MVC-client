using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Interfaces;
using Vinance.Contracts.Models.Identity;
using Vinance.Web.Models.Identity;

namespace Vinance.Web.Controllers
{
    [Authorize]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;

        public UserController(IUserApi userApi, IMapper mapper)
        {
            _userApi = userApi;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewmodel login)
        {
            var loginModel = _mapper.Map<LoginModel>(login);
            var tokenResult = await _userApi.GetToken(loginModel);

            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ReadJwtToken(tokenResult.Token);
            var claimsIdentity = new ClaimsIdentity(result.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.Response.Cookies.Append("token", tokenResult.Token);
            await HttpContext.SignInAsync(principal);

            return View("LoggedIn");
        }

        [HttpGet]
        [Route("loggedin")]
        public IActionResult LoggedIn()
        {
            return View();
        }
    }
}