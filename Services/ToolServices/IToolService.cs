using AppTool.Dtos;
using AppTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Services.ToolServices
{
    public interface IToolService
    {
        Task<List<Tool>> GetAll();
        Task<List<Tool>> Search(FilterDto dto);
        Task<bool> Register(ToolDto dto);
        Task<bool> Update(ToolDto dto, int id);
        Task<List<Tool>> MyTool(int id);
    }
}
