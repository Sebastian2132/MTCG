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
    class RegisterCommand : IRouteCommand
    {
        private readonly IRequestManager messageManager;
        public Credentials Credentials { get; private set; }

        public RegisterCommand(IRequestManager messageManager, Credentials credentials)
        {
            Credentials = credentials;
            this.messageManager = messageManager;
        }

        public Response Execute()
        {
            var response = new Response();
            try
            {
                messageManager.RegisterUser(Credentials);
                response.StatusCode = StatusCode.Created;
            }
            catch (DuplicateUserException)
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;
        }
    }
}
