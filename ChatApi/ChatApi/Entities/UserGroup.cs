using ChatApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class UserGroup
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public Role Role { get; set; }
    }
}
