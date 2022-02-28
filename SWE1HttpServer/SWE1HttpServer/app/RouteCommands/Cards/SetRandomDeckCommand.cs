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
    class SetRandomDeckCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;
        public SetRandomDeckCommand(IRequestManager messageManager)
        {
            this.messageManager = messageManager;
        }

        public override Response Execute()
        {
            var response = new Response();
            bool isSet = messageManager.setRandomDeck(User);
            
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
