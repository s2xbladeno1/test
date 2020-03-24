using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Dtos
{
    public class ToolDto
    {
        public int Id { get; set; }
        public int Create_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string File_url { get; set; }
    }
}
