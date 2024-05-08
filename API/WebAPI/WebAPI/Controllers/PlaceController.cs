namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class PlaceController : ApiControllerBase
    {
        public PlaceController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var places = await UnitOfWork.PlaceRepository.GetAsQueryable().ToListAsync();

                return Ok(places);
            }
        }
    }
}
