using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    [Owned]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        [JsonIgnore]
        public AppUser Sender { get; set; }
        [JsonIgnore]
        public Group Group { get;set; }
    }
}
