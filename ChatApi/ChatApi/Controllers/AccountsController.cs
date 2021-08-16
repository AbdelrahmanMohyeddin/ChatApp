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

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper map)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = map;
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
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerData)
        {
            var user = new AppUser
            {
                DisplayName = registerData.DisplayName,
                Email = registerData.Email,
                UserName = registerData.Email
            };

            var result = await _userManager.CreateAsync(user, registerData.Password);
            if (!result.Succeeded) return BadRequest("Not Registered,Please Repeate again!");
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }


        [HttpGet]
        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            var user = await _userManager.GetUserByClaimsPrincipalAsync(HttpContext.User);
            if (user == null) return Unauthorized("Sorry UnAuthorized");
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        
        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckExistEmail([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

    }
}
