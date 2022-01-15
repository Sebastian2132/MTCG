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
    class GetUSerDataCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;

        private readonly string userName;

        public GetUSerDataCommand(IRequestManager messageManager, string  userName)
        {
           
            this.messageManager = messageManager;
            this.userName = userName;
        }

      

        public override Response Execute()
        {
            var response = new Response();
            var userInfo = messageManager.GetUserInfo(User,userName);
            response.Payload = userInfo;
            response.StatusCode = StatusCode.Ok;

            return response;
        }
    }
}
