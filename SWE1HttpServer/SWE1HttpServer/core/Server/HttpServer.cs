using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Client;
using SWE1HttpServer.core.Listener;
using SWE1HttpServer.core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.core.Server
{

    public class HttpServer : IServer
    {
        private readonly IListener listener;
        private readonly IRouter router;
        private bool isListening;

        public HttpServer(IPAddress address, int port, IRouter router){
            listener = new Listener.HttpListener(address, port);
            this.router=router;
        }
        public void Start()
        {
            listener.Start();
            isListening = true;
        
            while (isListening){
                var client = listener.AcceptClient();
                HandleCLient(client);
            }
        }

        public void Stop()
        {
            isListening=false;
            listener.Stop();
        }

        private void HandleCLient(IClient client){
            var request = client.ReceiveRequest();

            Response.Response response;
            try{
                var command = router.Resolve(request);
                if(command != null){
                    response = command.Execute();
                }else{
                    response = new Response.Response(){ 
                        StatusCode = Response.StatusCode.BadRequest
                    };
                }

            }catch{
                response = new Response.Response(){
                    StatusCode = Response.StatusCode.Unauthorized
                };
            }
            client.SendResponse(response);
        }
    }

}