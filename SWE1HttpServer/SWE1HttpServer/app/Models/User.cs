using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.core.Authentication;

namespace SWE1HttpServer.app.Models
{
    public class User:IIdentity
    {
       public string Username { get; set; }
        public string Password { get; set; }

        public string Token => $"{Username}-mtcgToken";

            public int Coins{ get; set;}
            public int Score { get;set;}
            public List<Card> MainDeck{get; set;}
            public List<Card> AllCards{get; set;}

    }
}
