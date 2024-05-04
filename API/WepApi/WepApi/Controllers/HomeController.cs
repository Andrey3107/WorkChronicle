namespace WepApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;

    using CodeFirst;

    public class HomeController : ApiControllerBase
    {
        [HttpGet]
        public HttpResponseMessage GetTables()
        {

            using (var context = new ApplicationDbContext())
            {
                var r1esult = context.TestTables.ToList();
            }

            var result = "result";

            return CreateOkResponse(result);
        }
    }
}