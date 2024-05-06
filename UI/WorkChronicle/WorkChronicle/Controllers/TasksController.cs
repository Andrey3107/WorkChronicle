namespace WorkChronicle.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using ViewModels.Tasks;

    public class TasksController : BaseController
    {
        public TasksController(IConfiguration configuration)
            : base(configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectList()
        {
            try
            {
                var userIdExists = long.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId);

                var result = userIdExists ? await WebApiClient.GetProjectsByUser(userId) : new List<Project>();

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
                var assignees = await WebApiClient.GetAssigneesByProjectId(projectId);

                var result = assignees.Select(x => Mapper.Map(x)).ToList();

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

                return Json(new { Success = true, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskViewModel model)
        {
            if (model == null)
            {
                return Json(new { Success = false, ErrorMessage = "Input data cannot be null!" });
            }
            
            try
            {
                var ticket = Mapper.Map(model);

                var success = await WebApiClient.CreateTicket(ticket);

                if (success)
                {
                    return Json(new { Success = true });
                }

                return Json(new { Success = false, ErrorMessage = "Something went wrong!" });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, ErrorMessage = "Something went wrong!" });
            }
        }
    }
}
