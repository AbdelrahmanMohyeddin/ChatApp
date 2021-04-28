using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Others.SendAsync("Connected", Context.ConnectionId, "Is Connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.SendAsync("DisConnected", Context.ConnectionId, "Is DisConnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
