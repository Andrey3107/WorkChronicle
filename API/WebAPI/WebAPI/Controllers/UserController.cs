namespace WebAPI.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Models.AccessManagement;
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
        public async Task<IActionResult> GetAllUsers()
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var users = await UnitOfWork.UserRepository.GetAsQueryable().ToListAsync();

                return Ok(users);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPermissions(long userId)
        {
            using (UnitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var allRoles = await UnitOfWork.RoleRepository.GetAsQueryable().ToListAsync();

                var user = await UnitOfWork.UserRepository
                                .GetAsQueryable()
                                .Where(x => x.Id == userId)
                                .FirstOrDefaultAsync();

                var userRoles = await UnitOfWork.UserRoleRepository
                                    .GetAsQueryable()
                                    .Where(x => x.UserId == userId)
                                    .Include(x => x.Role)
                                    .ToListAsync();

                var userViewModel = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    AllRoles = allRoles,
                    UserRoles = userRoles.Select(y => y.Role.Name).ToList()
                };

                return Ok(userViewModel);
            }
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
        public async Task<IActionResult> ChangeUserPermissions(ChangeRoleViewModel filter)
        {
            try
            {
                using (var transaction = UnitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var userRoles = await UnitOfWork.UserRoleRepository
                                        .GetAsQueryable()
                                        .Where(x => x.UserId == filter.UserId)
                                        .Select(x => x.RoleId)
                                        .ToListAsync();

                    var selectedUserRoles = await UnitOfWork.RoleRepository
                                                .GetAsQueryable()
                                                .Where(x => filter.UserRoles.Contains(x.Name))
                                                .Select(x => x.Id)
                                                .ToListAsync();

                    var addedRolesId = selectedUserRoles.Except(userRoles).ToList();
                    var removedRolesId = userRoles.Except(selectedUserRoles).ToList();

                    var newRoles = addedRolesId.Select(x => new UserRole { UserId = filter.UserId, RoleId = x }).ToList();

                    UnitOfWork.UserRoleRepository.Add(newRoles);
                    UnitOfWork.UserRoleRepository.Delete(x => removedRolesId.Contains(x.RoleId) && x.UserId == filter.UserId);

                    await UnitOfWork.SaveAsync();

                    transaction.Commit();

                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return Ok(false);
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

                return Ok(response);
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
