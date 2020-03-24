using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTool.Dtos;
using AppTool.Model;
using AppTool.Services.RequestServices;
using AppTool.Services.SessionServices;
using Microsoft.AspNetCore.Mvc;

namespace AppTool.Controllers
{
    public class RequestController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService,ISessionService sessionService)
        {
            _requestService = requestService;
            _sessionService = sessionService;
        }
        [HttpPost]
        [Route("api/request/submit")]
        public async Task<IActionResult> Submit(RequestDto dto)
        {
            var result = await _requestService.Add(dto);
            if (result)
            {
                return Ok("Yêu cầu của bạn đang chờ xét duyệt");
            }
            else
            {
                return Ok("Lỗi chưa xác định");
            }
        }

        [HttpGet]
        [Route("api/request/myrequest")]
        public async Task<List<Request>> MyRequest()
        {
           return await _requestService.MyRequest(_sessionService.UserId);
        }
    }
}