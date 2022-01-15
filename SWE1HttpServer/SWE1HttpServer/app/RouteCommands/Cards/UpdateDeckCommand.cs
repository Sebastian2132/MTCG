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
    class UpdateDeckCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;
        string cards;
        public UpdateDeckCommand(IRequestManager messageManager, string cards)
        {
            this.messageManager = messageManager;
            this.cards =cards;
        }

        public override Response Execute()
        {
            var response = new Response();
            //string realCards=JsonConvert.DeserializeObject<string>(cards);
            bool isSet = messageManager.setDeck(cards,User);
            
            if (isSet==true)
            {
                response.StatusCode = StatusCode.Ok;
            }
            else
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;


        }
    }
}
