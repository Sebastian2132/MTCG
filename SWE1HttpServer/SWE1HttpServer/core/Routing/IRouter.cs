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

    public interface IRouter
    {
       IRouteCommand Resolve(RequestContext request);
        
    }
    
    }