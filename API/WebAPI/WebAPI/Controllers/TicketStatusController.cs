namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class TicketStatusController : ApiControllerBase
    {
        public TicketStatusController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketStatuses()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var ticketStatuses = await UnitOfWork.TicketStatusRepository.GetAsQueryable().ToListAsync();

                return Ok(ticketStatuses);
            }
        }
    }
}
