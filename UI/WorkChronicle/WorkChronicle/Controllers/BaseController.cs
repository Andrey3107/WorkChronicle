namespace WorkChronicle.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Utilities.Mapper;

    using WebApiClients;

    [Authorize]
    public class BaseController : Controller
    {
        protected readonly WebApiClient WebApiClient;

        protected readonly IMapper Mapper;

        public BaseController(IConfiguration configuration)
        {
            WebApiClient = new WebApiClient(configuration.GetValue<string>("BaseApiUrl"));
            Mapper = new Mapper();
        }
    }
}
