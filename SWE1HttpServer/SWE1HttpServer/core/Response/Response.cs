using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Response
{

    public class Response 
    {
       public StatusCode StatusCode { get; set; }
       public string Payload {get; set; }
    }
}