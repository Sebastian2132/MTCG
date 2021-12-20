using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Client
{

    public interface IClient
    {
        RequestContext ReceiveRequest();
        void SendResponse(Response.Response response);
        
    }
    
    }