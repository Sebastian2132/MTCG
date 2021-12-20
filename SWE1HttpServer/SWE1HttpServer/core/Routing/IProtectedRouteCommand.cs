using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Routing
{

    public interface IProtectedRouteCommand : IRouteCommand
    {
        IIdentity Identity{get; set; }
    }

}