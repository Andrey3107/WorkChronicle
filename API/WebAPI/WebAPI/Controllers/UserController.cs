namespace WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Models.Dto;

    using Services.Interfaces;

    public class UserController : ApiControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUnitOfWork unitOfWork, IUserService userService)
            : base(unitOfWork)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByProject(long projectId)
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var users = await UnitOfWork.ProjectRepository
                                   .GetAsQueryable()
                                   .Join(
                                       UnitOfWork.UserToProjectRepository.GetAsQueryable(),
                                       p => p.Id,
                                       u => u.ProjectId,
                                       (p, u) => new { ProjectId = p.Id, p.ProjectStatusId, u.UserId }
                                   )
                                   .Join(
                                       UnitOfWork.UserRepository.GetAsQueryable(),
                                       p => p.UserId,
                                       u => u.Id,
                                       (p, u) => new { p.ProjectId, p.ProjectStatusId, p.UserId, u.FirstName, u.LastName }
                                    )
                                   .Where(x => x.ProjectStatusId == 1 && x.ProjectId == projectId)
                                   .Select(x => new { x.UserId, x.FirstName, x.LastName })
                                   .ToListAsync();

                var result = users.Select(x => new User { Id = x.UserId, FirstName = x.FirstName, LastName = x.LastName }).ToList();

                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await userService.Login(loginRequestDto);

            ApiResponse<LoginResponseDto> response;

            if (user.User == null || string.IsNullOrEmpty(user.Token))
            {
                response = new ApiResponse<LoginResponseDto>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessage = "User or password is incorrect"
                };

                return BadRequest(response);
            }

            response = new ApiResponse<LoginResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = user
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            var isUserUniq = userService.IsUniqueUser(registrationRequestDto.Email);

            ApiResponse<UserDto> response;

            if (!isUserUniq)
            {
                response = new ApiResponse<UserDto>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessage = "User already exists"
                };

                return BadRequest(response);
            }

            var user = await userService.Register(registrationRequestDto);
            
            if (user == null)
            {
                response = new ApiResponse<UserDto>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessage = "Error while registering"
                };

                return BadRequest(response);
            }

            response = new ApiResponse<UserDto>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };

            return Ok(response);
        }
    }
}
