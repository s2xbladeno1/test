using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Dtos
{
    public class RequestDto
    {
        public int Tool_id { get; set; }
        public int Requester_id { get; set; }
        public int Team_id { get; set; }
        public int Project_id { get; set; }
        public string Reason { get; set; }
    }
}
