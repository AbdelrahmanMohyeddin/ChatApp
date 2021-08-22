using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Dtos
{
    public class MessageToGroupDto
    {
        public string GroupName { get; set; }
        public string content { get; set; }
    }
}
