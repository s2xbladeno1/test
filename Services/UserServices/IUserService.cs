using AppTool.Dtos;
using AppTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Services.UserServices
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetUser(int? id);
        Task<LoginResultDto> GenerateTokenJwt(User user);
        Task<User> Login(LoginDto dto);
    }
}
