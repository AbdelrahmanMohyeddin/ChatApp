using ChatApi.Controllers;
using ChatApi.Entities;
using ChatApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected",Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.SendAsync("DisConnected", Context.ConnectionId, "Is DisConnected");
            return base.OnDisconnectedAsync(exception);
        }

    }
}
