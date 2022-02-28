using SWE1HttpServer.core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.app.DAL;
using Newtonsoft.Json;
using SWE1HttpServer.app.Models;

namespace SWE1HttpServer.RouteCommands.Cards
{
    class AddPackagesCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager messageManager;
        List<TestCard>  package;
        public AddPackagesCommand(IRequestManager messageManager, List<TestCard> package)
        {
            this.messageManager = messageManager;
            this.package = package;
        }

        public override Response Execute()
        {
            var response = new Response();
            //List<TestCard> tempPackage = JsonConvert.DeserializeObject<List<TestCard>>(package);
            List<Card> realPackage = messageManager.MakePackage(package);
            if (realPackage.Count() > 0) { 
                            messageManager.AddPackage(realPackage);
                            response.StatusCode = StatusCode.Ok;

            }
            else
            {
                response.StatusCode = StatusCode.Conflict;
            }
            


            return response;


        }
    }
}
