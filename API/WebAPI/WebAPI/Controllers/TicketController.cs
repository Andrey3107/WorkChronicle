namespace WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    using Models.Ticket;

    public class TicketController : ApiControllerBase
    {
        public TicketController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketById(long id)
        {
            try
            {
                using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
                {
                    var result = await UnitOfWork.TicketRepository
                        .GetAsQueryable()
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketDetailsById(long id)
        {
            try
            {
                using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
                {
                    var ticket = await UnitOfWork.TicketRepository
                    .GetAsQueryable()
                    .Where(x => x.Id == id)
                    .Include(x => x.Priority)
                    .Include(x => x.Project)
                    .Include(x => x.Assignee)
                    .Include(x => x.TicketStatus)
                    .Include(x => x.TicketType)
                    .Include(x => x.TimeTracks)
                    .FirstOrDefaultAsync();

                    if (ticket == null)
                    {
                        return BadRequest();
                    }

                    var result = new TicketModalViewModel
                    {
                        ProjectName = ticket.Project?.Name,
                        Type = ticket.TicketType?.Description,
                        Status = ticket.TicketStatus?.Description,
                        Name = ticket.Name,
                        Description = ticket.Description,
                        AssigneeName = $"{ticket.Assignee?.FirstName} {ticket.Assignee?.LastName}",
                        Priority = ticket.Priority?.Description,
                        Completeness = ticket.Completeness?.ToString(),
                        Estimate = ticket.Estimate?.ToString(),
                        SpentTime = ticket.TimeTracks?.Sum(x => x.Duration).ToString(),
                        StartDate = ticket.Created.ToString("d MMM yyyy"),
                        EndDate = ticket.DueDate?.ToString("d MMM yyyy")
                    };

                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketsByUser(long userId)
        {
            try
            {
                using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
                {
                    var result = await UnitOfWork.TicketRepository
                        .GetAsQueryable()
                        .Where(x => x.AssigneeId == userId)
                        .Include(x => x.Priority)
                        .Include(x => x.Project)
                        .Where(x => x.Project.ProjectStatusId == 1)
                        .Select(x => new TicketViewModel
                        {
                            Id = x.Id,
                            ProjectName = x.Project.Name,
                            TicketName = x.Name,
                            Priority = x.Priority.Description,
                            TicketStatusId = x.TicketStatusId
                        })
                        .ToListAsync();

                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return Ok(new List<TicketViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (ticket.Id == 0)
                {
                    ticket.Created = DateTime.Now;
                }

                var result = UnitOfWork.TicketRepository.AddOrUpdate(ticket);
                await UnitOfWork.SaveAsync();

                transaction.Commit();

                if (result != null)
                {
                    return Ok(true);
                }

                return Ok(false);
            }
        }
    }
}
