namespace WorkChronicle.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using WebApiClients;

    [Authorize]
    public class BaseController : Controller
    {
        protected readonly WebApiClient webApiClient;

        public BaseController(IConfiguration configuration)
        {
            webApiClient = new WebApiClient(configuration.GetValue<string>("BaseApiUrl"));
        }
    }
}
