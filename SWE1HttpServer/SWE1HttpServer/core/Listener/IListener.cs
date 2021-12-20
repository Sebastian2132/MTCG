using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Listener
{

    public interface IListener
    {
        public IClient AcceptClient();
        void Start();
        void Stop();
    }
    
    }