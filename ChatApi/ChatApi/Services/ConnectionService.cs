using AutoMapper;
using ChatApi.Dtos;
using ChatApi.Entities;
using ChatApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public class ConnectionService : Hub,IConnectionService
    {
        public readonly static List<UserDto> _Connections = new List<UserDto>();
        public readonly static List<RoomDto> _Rooms = new List<RoomDto>();
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
       
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConnectionService(DataContext context,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task SendMessage1(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveOne", user, message);
        }

        public async Task MessageToGroup(ClaimsPrincipal user,MessageToGroupDto msg)
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            await Groups.AddToGroupAsync(Context.ConnectionId, msg.GroupName);
            await Clients.Group(msg.GroupName).SendAsync("MessageToGroup",email,msg.GroupName, msg.content);
        }

        public override async Task OnConnectedAsync()
        {
            getConnectionId();
            await Groups.AddToGroupAsync(getConnectionId(),"FirstGroup");
            await base.OnConnectedAsync();
        }

        public async Task JoinGroup(string connectionId)
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var _user = _userManager.Users.SingleOrDefault(x => x.Email == email);
            var groups = _context.groups.Where(x => x.Users.Where(x => x.AppUser == _user).Any());

            foreach (var group in groups)
            {
                await Groups.AddToGroupAsync(connectionId, group.Name);
            }
            
        }

        public string getConnectionId()
        {
            return Context.ConnectionId;
        }

        public async Task AddConnection(ClaimsPrincipal user, string connectionId)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var _user = _userManager.Users.SingleOrDefault(x => x.Email == email);

            var userDto = _mapper.Map<AppUser, UserDto>(_user);
            userDto.Device = "";
            userDto.CurrentRoom = "";


            if (!_Connections.Any(u => u.Username == _user.Email))
            {
                _Connections.Add(userDto);
                _ConnectionsMap.Add(_user.Email, connectionId);
            }
            else{
                _ConnectionsMap[_user.Email] = connectionId;
            }
            await JoinGroup(connectionId);
        }

    }
}
