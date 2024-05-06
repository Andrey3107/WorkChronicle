namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class TicketTypeController : ApiControllerBase
    {
        public TicketTypeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketTypes()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var ticketTypes = await UnitOfWork.TicketTypeRepository.GetAsQueryable().ToListAsync();

                return Ok(ticketTypes);
            }
        }
    }
}
