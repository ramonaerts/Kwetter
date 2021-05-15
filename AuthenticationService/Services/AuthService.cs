using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.DAL;
using AuthenticationService.Entities;
using AuthenticationService.Messages.Api;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Models;
using Konscious.Security.Cryptography;
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
            var user = _authenticationContext.Users.FirstOrDefault(u => u.Email == message.Email);

            if (user == null) return null;

            return !VerifyHash(message.Password, user.Salt, user.Hash) ? null : user;
        }

        public void AddUser(NewUserMessage message)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(message.Password, salt);

            var user = new User
            {
                Id = message.Id,
                Email = message.Email,
                Hash = hash,
                Salt = salt,
                Role = UserRole.User
            };

            _authenticationContext.Add(user);
            _authenticationContext.SaveChanges();
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 16,
                MemorySize = 8192,
                Iterations = 40
        };

            return argon2.GetBytes(128);
        }

        private byte[] GenerateSalt()
        {
            var buffer = new byte[128];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);

            return buffer;
        }

        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }

        public void UpdateEmail(EmailChangedMessage message)
        {
            var user = _authenticationContext.Users.FirstOrDefault(u => u.Id == message.Id);

            if (user == null) return;

            user.Email = message.Email;

            _authenticationContext.Update(user);
            _authenticationContext.SaveChanges();
        }

        public string CreateToken(string userId, UserRole role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
