namespace WebAPI.Services.Interfaces
{
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using Models.Dto;

    public interface IUserService
    {
        bool IsUniqueUser(string email);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<User> Register(RegistrationRequestDto registrationRequestDto);
    }
}
