namespace WorkChronicle.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using Models;

    using ViewModels;

    using WebApiClients;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration) 
            : base(configuration)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await webApiClient.GetAllProjects();
            var viewModel = new ProjectListViewModel { Projects = projects };

            return View(viewModel);
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectViewMode model)
        {
            if (ModelState.IsValid)
            {
                var result = await webApiClient.CreateProjectAsync(model);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Wrong password or login");
            }

            return View(model);
        }
    }
}
