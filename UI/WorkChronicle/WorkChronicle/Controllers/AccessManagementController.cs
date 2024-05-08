namespace WorkChronicle.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using ViewModels.AccessManagement;

    public class AccessManagementController : BaseController
    {
        public AccessManagementController(IConfiguration configuration)
            : base(configuration)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await WebApiClient.GetAllUsers();

            var mappedUsers = users.Select(x => Mapper.MapUser(x)).ToList();

            return View(mappedUsers);
        }

        [HttpGet]
        public async Task<IActionResult> EditPermissions(long id)
        {
            //var isValid = long.TryParse(userId, out var userIdParced);
            var userPermissions = await WebApiClient.GetUserPermissions(id);

            return View(userPermissions);
        }

        [HttpPost]
        public async Task<IActionResult> EditPermissions(long userId, List<string> roles)
        {
            var filter = new ChangeRoleViewModel { UserId = userId, UserRoles = roles };

            var success = await WebApiClient.ChangeUserPermissions(filter);

            return RedirectToAction("Index");
        }
    }
}
