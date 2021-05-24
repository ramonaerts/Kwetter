using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeService.Entities
{
    public class UserLike
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<string> UserLikes { get; set; }
    }
}
