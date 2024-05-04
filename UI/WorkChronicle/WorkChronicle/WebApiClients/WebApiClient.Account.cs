namespace WorkChronicle.WebApiClients
{
    using System.Threading.Tasks;

    using Models;

    public partial class WebApiClient
    {
        public Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return PostAsync<LoginRequestDto, ApiResponse<LoginResponseDto>>("/User/Login", loginRequestDto);
        }

        public Task<ApiResponse<UserDto>> RegisterAsync(RegistrationRequestDto registerRequestDto)
        {
            return PostAsync<RegistrationRequestDto, ApiResponse<UserDto>>("/User/Register", registerRequestDto);
        }
    }
}
