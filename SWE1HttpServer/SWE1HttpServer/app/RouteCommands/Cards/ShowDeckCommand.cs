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
        bool format;
        public ShowDeckCommand(IRequestManager messageManager, bool format)
        {
            this.messageManager = messageManager;
            this.format = format;


        }

        public override Response Execute()
        {
            var response = new Response();
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            var cards = messageManager.ShowActiveDeck(User);
            if (format == true)
            {
                response.Payload += "Deck:\n";
                foreach (var card in cards)
                {
                    response.Payload += card.toString() + "\n";
                }
            }
            else
            {
                response.Payload = JsonConvert.SerializeObject(cards, jsonSerializerSettings);
            }


            response.StatusCode = StatusCode.Ok;

            return response;


        }
    }
}
