using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vinance.Web.Controllers
{
    using Contracts.Interfaces;
    using Contracts.Models.Identity;
    using Models.Identity;

    [Authorize]
    [Route("")]
    public class UserController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public UserController(IUserApi userApi, IMapper mapper, IEmailSender emailSender)
        {
            _userApi = userApi;
            _mapper = mapper;
            _emailSender = emailSender;
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
        public async Task<IActionResult> Login(LoginViewmodel login, string returnUrl)
        {
            var loginModel = _mapper.Map<LoginModel>(login);
            var tokenResult = await _userApi.GetToken(loginModel);

            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ReadJwtToken(tokenResult.Token);
            var claimsIdentity = new ClaimsIdentity(result.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            DateTime? validTo;
            if (login.RememberMe)
                validTo = result.ValidTo;
            else
                validTo = null;

            var opt = new CookieOptions
            {
                Expires = validTo,
                HttpOnly = true
            };
            HttpContext.Response.Cookies.Append("token", tokenResult.Token, opt);

            var authOpt = new AuthenticationProperties
            {
                ExpiresUtc = validTo,
                IsPersistent = login.RememberMe
            };
            await HttpContext.SignInAsync(principal, authOpt);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewmodel registerViewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewmodel);
            }
            var model = _mapper.Map<RegisterModel>(registerViewmodel);
            var token = await _userApi.Register(model);

            if (token == null)
            {
                return View(registerViewmodel);
            }
            await _emailSender.SendEmail(registerViewmodel.Email, token.Token);

            return View("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            await _userApi.ConfirmEmail(email, token);
            return View();
        }
    }
}