using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class PrivateChat
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string FirstUser { get; set; }
        public string SecondUser { get; set; }
        public ICollection<UserMessages> Messages { get; set; }
    }
}
