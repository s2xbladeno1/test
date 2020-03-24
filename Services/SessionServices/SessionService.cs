using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTool.Services.SessionServices
{
    public class SessionService: ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var accountIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(accountIdClaim?.Value))
                {
                    throw new Exception("Bạn chưa đăng nhập");
                }

                int userId;
                if (!int.TryParse(accountIdClaim.Value, out userId))
                {
                    throw new Exception("Bạn chưa đăng nhập");
                }

                return userId;
            }
        }
    }
}
