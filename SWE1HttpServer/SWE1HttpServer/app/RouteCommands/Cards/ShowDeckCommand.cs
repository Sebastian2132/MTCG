using SWE1HttpServer.core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.app.DAL;
using Newtonsoft.Json;
using SWE1HttpServer.app.Models;

namespace SWE1HttpServer.RouteCommands.Cards
{
    class ShowDeckCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;
        public ShowDeckCommand(IRequestManager messageManager)
        {
            this.messageManager = messageManager;
            
        }

        public override Response Execute()
        {
            var cards = messageManager.ShowActiveDeck(User);
            var response = new Response();
           
                response.Payload += "Deck:\n";
                foreach (var card in cards)
                {
                    response.Payload += card.toString() + "\n";
                }

                response.StatusCode = StatusCode.Ok;
            
            return response;


        }
    }
}
