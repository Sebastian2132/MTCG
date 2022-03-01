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
    class StartBattleCommand : ProtectedRouteCommand
    {
        private readonly IRequestManager _requestManager;

        public StartBattleCommand(IRequestManager requestManager)
        {
            this._requestManager = requestManager;

        }

        public override Response Execute()
        {
            Response response = new();
            _requestManager.StartBattle(User);
            System.Threading.SpinWait.SpinUntil(() => _requestManager.checkBattle());
            response.StatusCode = StatusCode.Ok;
            response.Payload = _requestManager.getLog();
            Console.WriteLine("Program battle...");

            return response;
        }
    }
}
