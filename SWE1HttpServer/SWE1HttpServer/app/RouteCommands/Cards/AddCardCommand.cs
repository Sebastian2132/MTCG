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
    class AddCardCommand : ProtectedRouteCommand
    {
        private readonly IMessageManager messageManager;
        public AddCardCommand(IMessageManager messageManager)
        {
            this.messageManager = messageManager;
        }

        public override Response Execute()
        {
            var response = new Response();
            var package = messageManager.AquirePackages(User);
            
            if (package.Any())
            {
                response.Payload += "Package:\n";
                foreach (var card in package)
                {
                    response.Payload +=card.toString() + "\n";
                }

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
