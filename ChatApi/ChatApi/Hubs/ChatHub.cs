using AutoMapper;
using ChatApi.Controllers;
using ChatApi.Dtos;
using ChatApi.Entities;
using ChatApi.Helpers;
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
    //public class ChatHub : Hub
    //{
    //    public Task SendMessage1(string user, string message)
    //    {
    //        return Clients.All.SendAsync("ReceiveOne", user, message);
    //    }

    //}
}
