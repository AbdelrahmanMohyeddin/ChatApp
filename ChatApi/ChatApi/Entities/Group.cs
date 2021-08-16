using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        [JsonIgnore]
        public ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual ICollection<GroupUser> GroupUsers { get; set; }

    }
}
