using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Services
{
    public interface IProfileService
    {
        Task<User> getUserByUsername(string username);
    }
}
