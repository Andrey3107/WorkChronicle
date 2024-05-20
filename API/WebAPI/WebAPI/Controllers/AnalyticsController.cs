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

    using Models.Analytics;

    public class AnalyticsController : ApiControllerBase
    {
        public AnalyticsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAnalyticsData(HighchartFilter filter)
        {
            if (filter == null)
            {
                return BadRequest();
            }

            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var query = GetFilteredData(filter);

                var timeTracks = await query.ToListAsync();

                var result = timeTracks
                    .GroupBy(x => new { x.Assignee.Id, x.Assignee.FirstName, x.Assignee.LastName })
                    .Select(x => new AnalyticsChartViewModel
                    {
                        Name = $"{x.Key.FirstName} {x.Key.LastName}",
                        Data = x.GroupBy(y => new { y.Created.Value.Year, y.Created.Value.Month }).Select(z => new AnalyticsDataModel
                        {
                            CreateDate = new DateTime(z.Key.Year, z.Key.Month, 1),
                            Value = z.Sum(p => p.Duration)
                        })
                        .ToList()
                    })
                    .ToList();

                return Ok(result);
            }
        }

        private IQueryable<TimeTrack> GetFilteredData(HighchartFilter filter)
        {
            var query = UnitOfWork.TimeTrackRepository.GetAsQueryable().Include(x => x.Ticket).Include(x => x.Assignee);

            if (filter.ProjectId != 0)
            {
                query = query.Where(x => x.Ticket.ProjectId == filter.ProjectId);
            }

            if (filter.AssigneeId != 0)
            {
                query = query.Where(x => x.AssigneeId == filter.AssigneeId);
            }

            if (filter.PlaceId != 0)
            {
                query = query.Where(x => x.PlaceId == filter.PlaceId);
            }

            if (filter.TypeId != 0)
            {
                query = query.Where(x => x.Ticket.TypeId == filter.TypeId);
            }

            if (filter.StatusId != 0)
            {
                query = query.Where(x => x.Ticket.TicketStatusId == filter.StatusId);
            }

            if (filter.PriorityId != 0)
            {
                query = query.Where(x => x.Ticket.PriorityId == filter.PriorityId);
            }

            return query;
        }
    }
}
