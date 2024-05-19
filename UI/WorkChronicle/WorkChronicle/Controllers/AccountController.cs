namespace WorkChronicle.Controllers
{
    using System;
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
    using Models.Profile;

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

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById()
        {
            var userIdExists = long.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId);

            var userInfo = userIdExists ? await WebApiClient.GetUserById(userId) : null;

            return Json(new { Success = true, Data = userInfo });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            try
            {
                var userIdExists = long.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId);

                model.UserId = userId;

                if (!userIdExists)
                {
                    return Json(new { Success = false, ErrorMessage = "Cannot find user!" });
                }

                var result = await WebApiClient.ChangePassword(model);

                if (result)
                {
                    return Json(new { Success = true });
                }

                return Json(new { Success = false, ErrorMessage = "Wrong password!" });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, ErrorMessage = "Something went wrong!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfile model)
        {
            try
            {
                var userIdExists = long.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId);

                model.UserId = userId;

                if (!userIdExists)
                {
                    return Json(new { Success = false, ErrorMessage = "Cannot find user!" });
                }

                var result = await WebApiClient.UpdateUserInfo(model);

                return Json(new { Success = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, ErrorMessage = "Something went wrong!" });
            }
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
