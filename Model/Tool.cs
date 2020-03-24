using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Model
{
    public class Tool
    {
        public int ID { get; set; }
        public int Create_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string File_url { get; set; }
    }
}
