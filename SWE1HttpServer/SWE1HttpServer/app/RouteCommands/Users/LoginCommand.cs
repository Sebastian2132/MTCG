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
    class LoginCommand : IRouteCommand
    {
        private readonly IRequestManager messageManager;

        public Credentials Credentials { get; private set; }

        public LoginCommand(IRequestManager messageManager, Credentials credentials)
        {
            Credentials = credentials;
            this.messageManager = messageManager;
        }

        public Response Execute()
        {
            User user;
            try
            {
                user = messageManager.LoginUser(Credentials);
            }
            catch (UserNotFoundException)
            {
                user = null;
            }

            var response = new Response();
            if (user == null)
            {
                response.StatusCode = StatusCode.Unauthorized;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = user.Token;
            }

            return response;
        }
    }
}
