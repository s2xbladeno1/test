using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Dtos
{
    public class FilterDto
    {
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
    }
}
