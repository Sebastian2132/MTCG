using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Elo { get; set; }

        public string Token => $"{Username}-msgToken";
    }
}
