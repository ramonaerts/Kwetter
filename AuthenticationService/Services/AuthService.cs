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
using Microsoft.IdentityModel.Tokens;
using Shared.Messaging;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        //Change directory when database is working.
        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
            { { "test1@gmail.com", "password1" },{ "test2@gmail.com", "password2" } };
        private readonly string _key;
        private readonly IAuthenticationContext _authenticationContext;

        public AuthService(IAuthenticationContext authenticationContext)
        {
            _key = "eBCatxoffIIq6ESdrDZ8LKI3zpxhYkYM";
            _authenticationContext = authenticationContext;
        }

        public User LoginUser(LoginMessage message)
        {
            return _authenticationContext.LoginUser(message.Email, message.Password);
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
