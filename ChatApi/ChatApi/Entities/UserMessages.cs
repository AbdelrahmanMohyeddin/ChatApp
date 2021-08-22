using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class UserMessages
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public AppUser FromUser { get; set; }
        public DateTime Timestamp { get; set; }
        public PrivateChat PrivateChat { get; set; }
    }
}
