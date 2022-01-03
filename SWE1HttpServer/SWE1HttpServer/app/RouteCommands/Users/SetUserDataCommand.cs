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
    class SetUserDataCommand : ProtectedRouteCommand
    {
        private readonly IMessageManager messageManager;

        public readonly string userName;
        private readonly Dictionary<string, string> info;

        public SetUserDataCommand(IMessageManager messageManager, string  userName,Dictionary<string, string> info)
        {
            this.userName = userName;
            this.messageManager = messageManager;
            this.info=info;
        }

        public override Response Execute()
        {
            var response = new Response();
            if(messageManager.SetUserInfo(User,userName,info)){
                response.StatusCode = StatusCode.Ok;
            }else{
                response.StatusCode = StatusCode.Conflict;
            }


            return response;
        }
    }
}
