using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Dtos
{
    public class LoginResultDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
