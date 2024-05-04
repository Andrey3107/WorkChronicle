namespace WebAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using CodeFirst;
    using CodeFirst.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiControllerBase
    {
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
