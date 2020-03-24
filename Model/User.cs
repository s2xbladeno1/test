using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
