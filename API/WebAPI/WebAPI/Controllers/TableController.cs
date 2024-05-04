namespace WebAPI.Controllers
{
    using System.Linq;
    
    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TableController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public TableController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            using (unitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var result = unitOfWork.TestTableRepository.GetAsQueryable().ToList();

                return Ok(result);
            }
        } 
    }
}
