using AppTool.Dtos;
using AppTool.Model;
using AppTool.Services.SessionServices;
using AppTool.Services.UserServices;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppTool.Repository
{
    public class UserService: IUserService
    {
        private readonly ISessionService _sessionService;
        private readonly IConfiguration _config;
        public UserService(IConfiguration config,ISessionService sessionService)
        {
            _sessionService = sessionService;
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        
        public async Task<List<User>>  GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sql = @"select [User].ID, [User].Fullname, [User].UserName, [Role].Role  
                               from [User],[Role] 
                               where [User].Role = [Role].ID";
                conn.Open();
                var rs =  await conn.QueryAsync<User>(sql);
                return rs.ToList();
            }
        }
        public async Task<User> Login(LoginDto dto)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = @"select * from [User] where UserName = @user and Password = @pass";
                    conn.Open();
                    return await conn.QueryFirstOrDefaultAsync<User>(sql, new { user = dto.UserName, pass = dto.Password });
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task<LoginResultDto> GenerateTokenJwt(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey010203"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                claims: new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                },
                expires: DateTime.Now.AddDays(2),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new LoginResultDto
            {
                ID = user.ID,
                UserName = user.UserName,
                Role = user.Role,
                Token = tokenString
            };
        }

        public async Task<User> GetUser(int? id)
        {
            try
            {
                var userId = id.HasValue ? id.GetValueOrDefault() : _sessionService.UserId;
                using (IDbConnection conn = Connection)
                {
                    string sql = "select [User].ID, [User].Fullname, [User].UserName, [Role].Role from [User],[Role] where [User].Role = [Role].ID and [User].ID = @Id";
                    conn.Open();
                    return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
