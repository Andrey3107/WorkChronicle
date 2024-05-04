namespace WebAPI.Helpers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.IdentityModel.Tokens;

    using Models.Dto;

    public class JwtHelper
    {
        public LoginResponseDto GetResponseWithToken(UserDto user, string secretKey)
        {
            if (user == null || string.IsNullOrEmpty(secretKey))
            {
                return new LoginResponseDto { Token = "", User = null };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var loginResponseDto = new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return loginResponseDto;
        }
    }
}
