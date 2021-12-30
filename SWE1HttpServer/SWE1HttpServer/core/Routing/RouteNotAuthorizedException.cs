using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Routing
{

    public class RouteNotAuthorizedException : Exception
    {
        public RouteNotAuthorizedException()
        {

        }

        public RouteNotAuthorizedException(string message): base(message){

        }
        public RouteNotAuthorizedException(string message, Exception innerException): base(message, innerException){

        }


    }

}