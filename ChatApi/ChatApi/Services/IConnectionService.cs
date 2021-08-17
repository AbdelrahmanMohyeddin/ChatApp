using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IConnectionService
    {
        public void AddConnection(ClaimsPrincipal user,string connectionId);

    }
}
