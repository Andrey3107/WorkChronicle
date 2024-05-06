namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class PriorityController : ApiControllerBase
    {
        public PriorityController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPriorities()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var priorities = await UnitOfWork.PriorityRepository.GetAsQueryable().ToListAsync();

                return Ok(priorities);
            }
        }
    }
}
