namespace WebAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using CodeFirst;
    using CodeFirst.Interfaces;
    using CodeFirst.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiControllerBase
    {
        public HomeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetList()
        {
            List<TestTable> result;
            using (var dbContext = new ApplicationDbContext())
            {
                result = dbContext.TestTables.ToList();
            }

            return new JsonResult(result);
        }
    }
}
