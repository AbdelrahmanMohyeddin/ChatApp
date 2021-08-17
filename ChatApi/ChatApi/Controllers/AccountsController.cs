using AutoMapper;
using ChatApi.Dtos;
using ChatApi.Entities;
using ChatApi.Extensions;
using ChatApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConnectionService _connectionService;
        private readonly IHttpContextAccessor _httpContext;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper map,
            IConnectionService connectionService,
            IHttpContextAccessor HttpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = map;
            _connectionService = connectionService;
            _httpContext = HttpContext;
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginData)
        {
            var user = await _userManager.FindByEmailAsync(loginData?.Email);
            if (user == null) return BadRequest("Email Or Password Not Correct");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);
            if (!result.Succeeded) return Unauthorized("Not Authorized");
            return new UserDto
            {
                Username = user.UserName,
                Avatar = user.Avatar,
                FullName = user.FullName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerData)
        {
            var user = new AppUser
            {
                FullName = registerData.FullName,
                UserName = registerData.Email,
                Email = registerData.Email,
               
            };

            var result = await _userManager.CreateAsync(user, registerData.Password);
            if (!result.Succeeded) return BadRequest("Not Registered,Please Repeate again!");
            return new UserDto
            {
                FullName = user.FullName,
                Token = _tokenService.CreateToken(user),
                Avatar = user.Avatar,
                Username = user.UserName
            };
        }

        [HttpGet("addingConnection")]
        public IActionResult AddingConnection([FromQuery] string connectionId)
        {
            _connectionService.AddConnection(HttpContext.User, connectionId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            var user = await _userManager.GetUserByClaimsPrincipalAsync(HttpContext.User);
            if (user == null) return Unauthorized("Sorry UnAuthorized");
            return new UserDto
            {
                FullName = user.FullName,
                Token = _tokenService.CreateToken(user),
                Avatar = user.Avatar,
                Username = user.UserName
            };
        }

        
        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckExistEmail([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

    }
}
