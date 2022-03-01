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
            int elo =Convert.ToInt32(_requestManager.GetStat(User));
            
            _requestManager.StartBattle(User);
            System.Threading.SpinWait.SpinUntil(() => _requestManager.checkBattle());
            response.StatusCode = StatusCode.Ok;
            int elo2 =Convert.ToInt32(_requestManager.GetStat(User));
            if(elo>elo2){
                response.Payload ="You Lost!";
            }else if(elo2>elo){
                response.Payload ="You Won!";
            }else{
                response.Payload ="It was a Draw!";
            }
            response.Payload += _requestManager.getLog();
            Console.WriteLine("Program battle...");

            return response;
        }
    }
}
