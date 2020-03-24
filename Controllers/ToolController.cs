using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTool.Dtos;
using AppTool.Model;
using AppTool.Services;
using AppTool.Services.SessionServices;
using AppTool.Services.ToolServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppTool.Controllers
{
    //[Authorize]
    public class ToolController : Controller
    {
        private readonly IToolService _toolService;
        private readonly ISessionService _sessionService;
        public ToolController(IToolService toolService, ISessionService sessionService)
        {
            _toolService = toolService;
            _sessionService = sessionService;
        }

        /// <summary>
        /// Danh sách tool - Trang Home
        /// </summary>
        [HttpGet]
        [Route("api/tool/get")]
        public async Task<IEnumerable<Tool>> GetAll()
        {
            return await _toolService.GetAll();
        }

        /// <summary>
        /// Tìm kiếm tool - Trang Home
        /// </summary>
        [HttpGet]
        [Route("api/tool/search")]
        public async Task<List<Tool>> Search(FilterDto dto)
        {
            var result = await _toolService.Search(dto);
            return result;
        }

        /// <summary>
        /// Đăng ký tool - Trang My Tool
        /// </summary>
        [HttpPost]
        [Route("api/tool/register")]
        public async Task<string> Register([FromBody] ToolDto dto)
        {
            var result = await _toolService.Register(dto);
            if (result)
            {
                return "Chờ duyệt tool";
            }
            else
            {
                return "Đăng ký tool thất bại";
            }
        }

        /// <summary>
        /// Cập nhật tool - Trang My Tool
        /// </summary>
        [HttpPost]
        [Route("api/tool/update/{id}")]
        public async Task<string> Update([FromBody] ToolDto dto, int id)
        {
            var result = await _toolService.Update(dto,id);
            if (result)
            {
                return "Cập nhật thành công";
            }
            else
            {
                return "Cập nhật thất bại";
            }
        }

        /// <summary>
        /// Danh sách tool - Trang My Tool
        /// </summary>
        [HttpGet]
        [Route("api/tool/mytool")]
        public async Task<List<Tool>> MyTool()
        {
            return await _toolService.MyTool(_sessionService.UserId);
        }
    }
}