using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bill.viewmodel
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Role { get; set; }
    }
}
