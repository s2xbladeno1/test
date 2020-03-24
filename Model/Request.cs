using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Model
{
    public class Request
    {
        public int Id { get; set; }
        public int Tool_id { get; set; }
        public int Requester_id { get; set; }
        public int Team_id { get; set; }
        public int Project_id { get; set; }
        public string Reason { get; set; }
    }
}
