namespace WorkChronicle.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Models.Analytics;
    using Models.Tasks;

    using WebApiClients;

    public class AnalyticsController : BaseController
    {
        public AnalyticsController(IConfiguration configuration)
            : base(configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAnalyticsHighchartData(HighchartFilter filter)
        {
            try
            {
                var result = await WebApiClient.GetAnalyticsHighchartData(filter);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectList()
        {
            try
            {
                var userIdExists = long.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId);

                var result = userIdExists ? await WebApiClient.GetProjectsByUser(userId) : new List<Project>();

                var allOption = new Project { Id = 0, Name = "All" };

                result.Insert(0, allOption);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketTypes()
        {
            try
            {
                var result = await WebApiClient.GetTicketTypes();

                var allOption = new TicketType
                {
                    Id = 0,
                    Description = "All"
                };

                result.Insert(0, allOption);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketStatuses()
        {
            try
            {
                var result = await WebApiClient.GetTicketStatuses();

                var allOption = new TicketStatus
                {
                    Id = 0,
                    Description = "All"
                };

                result.Insert(0, allOption);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignees(long projectId)
        {
            try
            {
                List<User> assignees;

                if (projectId == 0)
                {
                    assignees = await WebApiClient.GetAllUsers();
                }
                else
                {
                    assignees = await WebApiClient.GetAssigneesByProjectId(projectId);
                }

                var result = assignees.Select(x => Mapper.Map(x)).ToList();

                var allOption = new AssigneeModel { Id = 0, FullName = "All" };

                result.Insert(0, allOption);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPriorities()
        {
            try
            {
                var result = await WebApiClient.GetPriorities();

                var allOption = new Priority
                {
                    Id = 0,
                    Description = "All"
                };

                result.Insert(0, allOption);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            try
            {
                var result = await WebApiClient.GetPlaces();

                var allOption = new Place
                {
                    Id = 0,
                    Description = "All"
                };

                result.Insert(0, allOption);

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }
    }
}
