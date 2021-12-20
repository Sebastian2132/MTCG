using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Request
{

    public class RequestContext 
    {
       public HttpMethod method {get; set; }
       public string ResourcePath {get; set; }
       public string HttpVersion {get; set; }
       public Dictionary<string, string> Headers {get; set; }
       public string Payload {get; set; }
    }
}