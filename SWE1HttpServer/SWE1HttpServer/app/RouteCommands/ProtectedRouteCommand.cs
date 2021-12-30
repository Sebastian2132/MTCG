using SWE1HttpServer.core.Authentication;
using SWE1HttpServer.core.Response;
using SWE1HttpServer.core.Routing;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.RouteCommands
{
    abstract class ProtectedRouteCommand : IProtectedRouteCommand
    {
        public IIdentity Identity { get; set; }

        public User User => (User)Identity;

        public abstract Response Execute();
    }
}
