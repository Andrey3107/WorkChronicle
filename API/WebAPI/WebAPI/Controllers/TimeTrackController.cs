namespace WebAPI.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    public class TimeTrackController : ApiControllerBase
    {
        public TimeTrackController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeTrack(TimeTrack timerTrack)
        {
            if (timerTrack == null)
            {
                return BadRequest();
            }

            using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                var result = UnitOfWork.TimeTrackRepository.AddOrUpdate(timerTrack);
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
