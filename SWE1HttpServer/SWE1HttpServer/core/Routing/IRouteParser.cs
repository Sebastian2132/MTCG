using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Routing
{

    public interface IRouteParser
    {
       bool isMatch(RequestContext request,HttpMethod method, string routePattern);
       Dictionary<string,string> ParseParameters(RequestContext request, string routePattern);
        
    }
    
    }