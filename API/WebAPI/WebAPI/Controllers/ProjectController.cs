namespace WebAPI.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    using Models.Dto;

    public class ProjectController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using (unitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var result = await unitOfWork.ProjectRepository.GetAsQueryable().ToListAsync();

                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto model)
        {
            var newProject = new Project
            {
                Name = model.ProjectName,
                Created = DateTime.Now,
                ProjectStatusId = 1
            };

            using (var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                unitOfWork.ProjectRepository.Add(newProject);
                await unitOfWork.SaveAsync();

                transaction.Commit();

                return Ok(true);
            }
        }
    }
}
