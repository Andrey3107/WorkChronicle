namespace WorkChronicle.WebApiClients
{
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using Models;
    using Models.Profile;

    public partial class WebApiClient
    {
        public Task<User> GetUserById(long userId)
        {
            return GetAsync<User>($"/User/GetUserById?userId={userId}");
        }

        public Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return PostAsync<LoginRequestDto, ApiResponse<LoginResponseDto>>("/User/Login", loginRequestDto);
        }

        public Task<ApiResponse<UserDto>> RegisterAsync(RegistrationRequestDto registerRequestDto)
        {
            return PostAsync<RegistrationRequestDto, ApiResponse<UserDto>>("/User/Register", registerRequestDto);
        }

        public Task<bool> UpdateUserInfo(UpdateProfile model)
        {
            return PostAsync<UpdateProfile, bool>("/User/UpdateUserInfo", model);
        }

        public Task<bool> ChangePassword(ChangePassword model)
        {
            return PostAsync<ChangePassword, bool>("/User/ChangePassword", model);
        }
    }
}
