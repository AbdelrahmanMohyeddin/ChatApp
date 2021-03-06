using AutoMapper;
using ChatApi.Dtos;
using ChatApi.Helpers;
using ChatApi.Hubs;
using ChatApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        
        private IHubContext<ConnectionService> _hub;
        private readonly IConnectionService _connectionService;
        public ChatController(IHubContext<ConnectionService> hub, IConnectionService connectionService)
        {
            _hub = hub;
            _connectionService = connectionService;
        }

        [HttpPost("send")]
        public IActionResult SendRequest([FromBody] MessageDto msg)
        {
            _hub.Clients.All.SendAsync("ReceiveOne", msg.user, msg.msgText);
            return Ok();
        }

        [HttpPost("sendToGroup")]
        public IActionResult SendToGroup(MessageToGroupDto msg)
        {
            _connectionService.MessageToGroup(HttpContext.User, msg);
            return Ok();
        }

        [HttpGet("SignedUser")]
        public IActionResult SignedUser()
        {
            var c = HttpContext.User;
            return Ok();
        }

        [HttpGet("addingConnection")]
        public IActionResult AddingConnection([FromQuery] string connectionId)
        {
            _connectionService.AddConnection(HttpContext.User, connectionId);
            return Ok();
        }
    }
}
