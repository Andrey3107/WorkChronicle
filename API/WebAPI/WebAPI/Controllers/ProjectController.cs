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

        [HttpGet]
        public async Task<IActionResult> GetProjectById(long id)
        {
            using (unitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var result = await unitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);

                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeactivateProject(long id)
        {
            try
            {
                using (var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var existingProject = await unitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);

                    if (existingProject == null) return Ok(false);

                    existingProject.ProjectStatusId = 2;

                    unitOfWork.ProjectRepository.Update(existingProject);
                    await unitOfWork.SaveAsync();

                    transaction.Commit();

                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActivateProject(long id)
        {
            try
            {
                using (var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var existingProject = await unitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);

                    if (existingProject == null) return Ok(false);

                    existingProject.ProjectStatusId = 1;

                    unitOfWork.ProjectRepository.Update(existingProject);
                    await unitOfWork.SaveAsync();

                    transaction.Commit();

                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject(long id)
        {
            try
            {
                using (var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    unitOfWork.ProjectRepository.Delete(id);
                    await unitOfWork.SaveAsync();

                    transaction.Commit();

                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
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

        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            try
            {
                using (var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var existingProject = await unitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == project.Id);

                    if (existingProject == null) return Ok(false);

                    existingProject.Name = project.Name;

                    unitOfWork.ProjectRepository.Update(existingProject);
                    await unitOfWork.SaveAsync();

                    transaction.Commit();

                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }
    }
}
