using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual Group Group { get; set; }
        [JsonIgnore]
        public virtual AppUser User { get; set; }
        public Role Role { get; set; }
    }
}
