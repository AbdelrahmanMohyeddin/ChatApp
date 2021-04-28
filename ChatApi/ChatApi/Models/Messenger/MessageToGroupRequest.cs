using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models.Messenger
{
    public class MessageToGroupRequest
    {
       //public string GroupName { get; set; }
       public string UserName { get; set; }
       public string MessageText { get; set; }
    }
}
