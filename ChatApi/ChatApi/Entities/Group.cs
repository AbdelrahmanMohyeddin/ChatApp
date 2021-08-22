using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppUser Admin { get; set; }
        public ICollection<GroupMessages> Messages { get; set; }
        public ICollection<UserGroup> Users { get; set; }
        public DateTime Created { get; set; }
    }
}
