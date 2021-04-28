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
    public class MessengerController : BaseController
    {
        public IHubContext<ChatHub> _hubContext;

        public MessengerController(IHubContext<ChatHub> _hub)
        {
            _hubContext = _hub;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendToGroup(MessageToGroupRequest msg)
        {
            //await _hubContext.Groups.AddToGroupAsync(, msg.UserName);
            //await _hubContext.Clients.Group(msg.UserName).SendAsync("ReceiveOnGroup", msg.UserName, msg.MessageText);
            await _hubContext.Clients.All.SendAsync("ReceiveOnGroup",msg.UserName,msg.MessageText);
            //_hubContext.Clients.Client().SendAsync("ReceiveOnGroup", msg.UserName, msg.MessageText);
            return Ok();
        }



    }
}
