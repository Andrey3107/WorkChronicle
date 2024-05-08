namespace WorkChronicle.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Models;

    using ViewModels;

    public class AccountController : BaseController
    {
        public AccountController(IConfiguration configuration) 
            : base(configuration)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginRequestDto = new LoginRequestDto { Email = model.Email, Password = model.Password };

                var result = await WebApiClient.LoginAsync(loginRequestDto);
                
                if (result.IsSuccess)
                {
                    await HttpContext.SignOutAsync();

                    var claims = new List<Claim>
                    {
                        new Claim("UserId", $"{result.Result.User.Id}"),
                        new Claim(ClaimTypes.Name, $"{result.Result.User.FirstName} {result.Result.User.LastName}"),
                        new Claim(ClaimTypes.Role, result.Result.User.Role),
                        new Claim("Token", result.Result.Token)
                    };

                    var claimIsentity = new ClaimsIdentity(claims, "MyAppCookie");
                    var claimsPrincipal = new ClaimsPrincipal(claimIsentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Wrong password or login");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registrationRequestDto = new RegistrationRequestDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };

                var result = await WebApiClient.RegisterAsync(registrationRequestDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Login", "Account");
                }
                
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
