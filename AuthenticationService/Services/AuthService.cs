using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Entities;
using AuthenticationService.Messages.Api;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        //Change directory when database is working.
        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
            { { "test1@gmail.com", "password1" },{ "test2@gmail.com", "password2" } };
        private readonly string _key;

        public AuthService(string key)
        {
            _key = key;
        }

        public User LoginUser(LoginMessage message)
        {
            if (!_users.Any(u => u.Key == message.Email && u.Value == message.Password)) return null;

            return new User
            {
                Email = "test1@gmail.com",
                Id = 1,
                Password = "password1",
                ProfileName = "Ramon",
                Username = "ra15"
            };
        }

        public string CreateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
