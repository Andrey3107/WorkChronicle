namespace WebAPI.Controllers
{
    using CodeFirst.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]/[Action]")]
    [ApiController]
    public class ApiControllerBase : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;

        public ApiControllerBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
