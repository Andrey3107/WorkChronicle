﻿namespace WorkChronicle.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication.OAuth.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using ViewModels;
    using ViewModels.AccessManagement;
    using ViewModels.Project;

    public class ProjectsController : BaseController
    {
        public ProjectsController(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IActionResult> Index()
        {
            var projects = await WebApiClient.GetAllProjects(false);
            var viewModel = new ProjectListViewModel { Projects = projects };

            return View(viewModel);
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        public async Task<IActionResult> EditParticipants(long id)
        {
            var users = await WebApiClient.GetProjectUsers(id);

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditProject(long id)
        {
            var project = await WebApiClient.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            var projectViewModel = new EditProjectViewModel { Id = project.Id, ProjectName = project.Name };

            return View(projectViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeactivateProject(long id)
        {
            var result = await WebApiClient.DeactivateProject(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ActivateProject(long id)
        {
            var result = await WebApiClient.ActivateProject(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var result = await WebApiClient.DeleteProject(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(EditProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var updatedProject = Mapper.Map(project);
                var success = await WebApiClient.UpdateProject(updatedProject);

                if (!success) { }

                return RedirectToAction("Index");
            }

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectViewMode model)
        {
            var result = await WebApiClient.CreateProjectAsync(model);

            return Json(new { Success = result });
        }

        [HttpPost]
        public async Task<IActionResult> EditParticipants(long projectId, List<long> users)
        {
            var filter = new ChangeParticipantsViewModel { ProjectId = projectId, ProjectUsers = users };

            var success = await WebApiClient.ChangeProjectUsers(filter);

            return RedirectToAction("Index");
        }
    }
}
