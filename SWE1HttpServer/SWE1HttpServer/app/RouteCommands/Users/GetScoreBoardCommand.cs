using SWE1HttpServer.core.Response;
using SWE1HttpServer.core.Routing;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.RouteCommands.Users
{
    class GetScoreBoardCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;


        public GetScoreBoardCommand(IRequestManager messageManager)
        {

            this.messageManager = messageManager;
        }



        public override Response Execute()
        {
            var response = new Response();
            var Score = messageManager.GetScoreboard();
            if (response.Payload != "")
            {
                response.Payload ="Scoreboard: \n"+ Score;
                response.StatusCode = StatusCode.Ok;
            }


            return response;
        }
    }
}
