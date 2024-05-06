namespace WebAPI.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    using Models.Dto;

    public class ProjectController : ApiControllerBase
    {
        public ProjectController(IUnitOfWork UnitOfWork)
            : base(UnitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool onlyActive = false)
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var projectQuery = UnitOfWork.ProjectRepository.GetAsQueryable();

                if (onlyActive)
                {
                    projectQuery = projectQuery.Where(x => x.ProjectStatusId == 1);
                }

                var result = await projectQuery.ToListAsync();

                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectsByUser(long userId)
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var projects = await UnitOfWork.ProjectRepository
                    .GetAsQueryable()
                    .Join(
                        UnitOfWork.UserToProjectRepository.GetAsQueryable(),
                        p => p.Id,
                        u => u.ProjectId,
                        (p, u) => new { p.Id, p.Name, p.ProjectStatusId, u.UserId } 
                    )
                    .Where(x => x.ProjectStatusId == 1 && x.UserId == userId)
                    .Select(x => new { x.Id, x.Name })
                    .ToListAsync();

                var result = projects.Select(x => new Project { Id = x.Id, Name = x.Name }).ToList();

                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectById(long id)
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var result = await UnitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);

                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeactivateProject(long id)
        {
            try
            {
                using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var existingProject = await UnitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);

                    if (existingProject == null) return Ok(false);

                    existingProject.ProjectStatusId = 2;

                    UnitOfWork.ProjectRepository.Update(existingProject);
                    await UnitOfWork.SaveAsync();

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
                using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var existingProject = await UnitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);

                    if (existingProject == null) return Ok(false);

                    existingProject.ProjectStatusId = 1;

                    UnitOfWork.ProjectRepository.Update(existingProject);
                    await UnitOfWork.SaveAsync();

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
                using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var userToProject = await UnitOfWork.UserToProjectRepository
                                            .GetAsQueryable()
                                            .Where(x => x.ProjectId == id)
                                            .ToListAsync();
                    var ticket = await UnitOfWork.TicketRepository
                                     .GetAsQueryable()
                                     .Where(x => x.ProjectId == id)
                                     .ToListAsync();

                    var tickeIds = ticket.Select(x => x.Id).ToList();

                    var timeTracks = await UnitOfWork.TimeTrackRepository.GetAsQueryable()
                                         .Where(x => tickeIds.Contains(x.TicketId)).ToListAsync();

                    UnitOfWork.TimeTrackRepository.Delete(timeTracks);
                    UnitOfWork.TicketRepository.Delete(ticket);
                    UnitOfWork.UserToProjectRepository.Delete(userToProject);
                    UnitOfWork.ProjectRepository.Delete(id);
                    await UnitOfWork.SaveAsync();

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

            using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                UnitOfWork.ProjectRepository.Add(newProject);
                await UnitOfWork.SaveAsync();

                transaction.Commit();

                return Ok(true);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            try
            {
                using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var existingProject = await UnitOfWork.ProjectRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == project.Id);

                    if (existingProject == null) return Ok(false);

                    existingProject.Name = project.Name;

                    UnitOfWork.ProjectRepository.Update(existingProject);
                    await UnitOfWork.SaveAsync();

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
