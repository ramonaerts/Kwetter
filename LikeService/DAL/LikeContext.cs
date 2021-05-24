using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.DAL;

namespace LikeService.DAL
{
    public class LikeContext : ILikeContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
