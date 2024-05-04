namespace WebAPI.Controllers
{
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Models.Dto;

    using Services.Interfaces;

    public class UserController : ApiControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
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
