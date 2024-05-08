namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class RoleController : ApiControllerBase
    {
        public RoleController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var roles = await UnitOfWork.RoleRepository.GetAsQueryable().ToListAsync();

                return Ok(roles);
            }
        }
    }
}
