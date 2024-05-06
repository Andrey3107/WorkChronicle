namespace WebAPI.Controllers
{
    using System.Linq;
    
    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TableController : ApiControllerBase
    {
        public TableController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public IActionResult GetList()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var result = UnitOfWork.TestTableRepository.GetAsQueryable().ToList();

                return Ok(result);
            }
        } 
    }
}
