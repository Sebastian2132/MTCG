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
    class GetStatAndScoreBoardCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;


        public GetStatAndScoreBoardCommand(IRequestManager messageManager)
        {

            this.messageManager = messageManager;
        }



        public override Response Execute()
        {
            var response = new Response();
            var Score = messageManager.GetStat(User);
            if (response.Payload != "")
            {
                response.Payload ="ELO-Value: "+ Score;
                response.StatusCode = StatusCode.Ok;
            }


            return response;
        }
    }
}
