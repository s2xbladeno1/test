using AppTool.Dtos;
using AppTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Services.RequestServices
{
    public interface IRequestService
    {
        Task<bool> Add(RequestDto dto);
        Task<List<Request>> MyRequest(int id);
    }
}
