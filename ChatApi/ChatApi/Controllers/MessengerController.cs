using ChatApi.Hubs;
using ChatApi.Models.Messenger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessengerController : ControllerBase
    {
        public IHubContext<ChatHub> _hubContext;

        public MessengerController(IHubContext<ChatHub> _hub)
        {
            _hubContext = _hub;
        }

        [HttpPost]
        [Authorize]
        public IActionResult SendToGroup(MessageToGroupRequest msg)
        {
            _hubContext.Clients.Group(msg.GroupName).SendAsync("ReceiveOnGroup", msg.UserName, msg.MessageText);
            return Ok();
        }
    }
}
