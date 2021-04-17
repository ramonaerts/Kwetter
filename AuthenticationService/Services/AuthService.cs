using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.DAL;
using AuthenticationService.Entities;
using AuthenticationService.Messages.Api;
using AuthenticationService.Messages.Broker;
using Microsoft.IdentityModel.Tokens;
using Shared.Messaging;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _key;
        private readonly AuthenticationContext _authenticationContext;

        public AuthService(AuthenticationContext authenticationContext)
        {
            _key = "eBCatxoffIIq6ESdrDZ8LKI3zpxhYkYM";
            _authenticationContext = authenticationContext;
        }

        public User LoginUser(LoginMessage message)
        {
            return _authenticationContext.Users.FirstOrDefault(u =>
                u.Email == message.Email && u.Password == message.Password);
        }

        public void AddUser(NewUserMessage message)
        {
            var user = new User
            {
                Id = message.Id,
                Email = message.Email,
                Password = message.Password
            };

            _authenticationContext.Add(user);
            _authenticationContext.SaveChanges();
        }

        public string CreateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userId)
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
