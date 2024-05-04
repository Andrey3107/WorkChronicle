namespace WebAPI.Services
{
    using System;
    using System.Data.Entity;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;
    using CodeFirst.Models.Entities;

    using Helpers;

    using Interfaces;

    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using Models.Dto;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly JwtHelper jwtHelper;

        private string secretKey;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            jwtHelper = new JwtHelper();
        }

        public bool IsUniqueUser(string email)
        {
            using (unitOfWork.BeginTransaction(IsolationLevel.Snapshot))
            {
                var isUserNotUnique = unitOfWork.UserRepository.GetAsQueryable().Any(x => x.Email == email);

                return !isUserNotUnique;
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await unitOfWork.UserRoleRepository
            .GetAsQueryable()
            .Include(x => x.User)
            .Include(x => x.Role)
            .Select(x => new UserDto
            {
                Id = x.User.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                Password = x.User.Password,
                Salt = x.User.Salt,
                Role = x.Role.Name
            })
            .FirstOrDefaultAsync(x => x.Email == loginRequestDto.Email && x.Password == loginRequestDto.Password);

            if (user == null)
            {
                return new LoginResponseDto { Token = "", User = null };
            }

            var loginResponseDto = jwtHelper.GetResponseWithToken(user, secretKey);

            return loginResponseDto;
        }

        public async Task<User> Register(RegistrationRequestDto registrationRequestDto)
        {
            var newUser = new User
            {
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                Email = registrationRequestDto.Email,
                Password = registrationRequestDto.Password,
            };

            using (var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                var user = unitOfWork.UserRepository.Add(newUser);
                await unitOfWork.SaveAsync();

                var employeeRole = await unitOfWork.RoleRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Name == "Employee");

                if (employeeRole != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = employeeRole.Id
                    };

                    unitOfWork.UserRoleRepository.Add(userRole);
                    await unitOfWork.SaveAsync();
                }

                transaction.Commit();

                user.Password = "";

                return user;
            }
        }
    }
}
