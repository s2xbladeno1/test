using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppTool.Dtos;
using AppTool.Model;
using AppTool.Repository;
using AppTool.Services.SessionServices;
using AppTool.Services.UserServices;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AppTool.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
           
        }

        /// <summary>
        /// Danh sách user
        /// </summary>
        /// 
        //[Authorize]
        [Route("api/user/get")]
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _userService.GetAll();
        }

        /// <summary>
        /// Thông tin user
        /// </summary>
        //[Authorize]
        [Route("api/user/get/{id}")]
        [HttpGet]
        public async Task<User> Get(int id)
        {
            return await _userService.GetUser(id);
        }


        /// <summary>
        /// Đăng nhập
        /// </summary>
        [Route("api/user/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.Login(dto);
            if (user == null)
            {
                return BadRequest(new { message = "Your login was incorrect" });
            }
            else
            {
                var token = await _userService.GenerateTokenJwt(user);
                var userSession = new UserSessionDto();
                userSession.ID = user.ID;
                userSession.UserName = user.UserName;
                userSession.Password = user.Password;
                userSession.Role = user.Role;
                userSession.Token = token.Token;
                return Ok(token);
            }
        }
    }
}