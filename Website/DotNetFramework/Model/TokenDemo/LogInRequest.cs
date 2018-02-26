using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TokenDemo
{
    public class LogInRequest
    {
        public string UsernameOrEmailAddress { get; set; }
        public string Password { get; set; }
    }
}
