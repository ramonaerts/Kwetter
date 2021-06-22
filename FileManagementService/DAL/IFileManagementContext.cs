using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Entities;
using MongoDB.Driver;

namespace FileManagementService.DAL
{
    public interface IFileManagementContext
    {
        IMongoCollection<ProfileImage> ProfileImages { get; }
    }
}
