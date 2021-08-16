using ChatApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApi.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
