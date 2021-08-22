using ChatApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IConnectionService
    {
        public Task AddConnection(ClaimsPrincipal user,string connectionId);
        public Task MessageToGroup(ClaimsPrincipal user, MessageToGroupDto msg);
    }
}
