namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    public class TicketController : ApiControllerBase
    {
        public TicketController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
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
