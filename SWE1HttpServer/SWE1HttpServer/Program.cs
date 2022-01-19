using System;
using System.Net;
using Newtonsoft.Json;
using SWE1HttpServer.core.Request;
using SWE1HttpServer.core.Routing;
using SWE1HttpServer.core.Server;
using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.RouteCommands.Users;
using SWE1HttpServer.RouteCommands.Cards;
using System.Collections.Generic;

namespace SWE1HttpServer.SWE1HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database("Host=localhost;Port=5431;Username=postgres;Password=postgres;Database=mtcgdb");
            var messageManager = new RequestManager(db.UserRepository,db.PackageRepository);

            var identityProvider = new MessageIdentityProvider(db.UserRepository);
            var routeParser = new IdRouteParser();

            var router = new Router(routeParser, identityProvider);
            RegisterRoutes(router, messageManager);

            var httpServer = new HttpServer(IPAddress.Any, 10001, router);
            httpServer.Start();
        }

        private static void RegisterRoutes(Router router, IRequestManager messageManager)
        {
            // public routes
            router.AddRoute(HttpMethod.Post, "/sessions", (r, p) => new LoginCommand(messageManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Post, "/users", (r, p) => new RegisterCommand(messageManager, Deserialize<Credentials>(r.Payload)));

            // protected routes
       
            router.AddProtectedRoute(HttpMethod.Post,"/transactions/packages", (r, p)=>new BuyPackageCommand(messageManager));
            router.AddProtectedRoute(HttpMethod.Get,"/cards", (r, p)=>new ShowWholeDeck(messageManager));
            router.AddProtectedRoute(HttpMethod.Post,"/packages", (r, p)=>new AddPackagesCommand(messageManager,r.Payload));
            router.AddProtectedRoute(HttpMethod.Get,"/deck", (r, p)=>new ShowDeckCommand(messageManager));
            router.AddProtectedRoute(HttpMethod.Put,"/deck", (r, p)=>new UpdateDeckCommand(messageManager,r.Payload));
            router.AddProtectedRoute(HttpMethod.Get,"/users/{id}", (r, p)=>new GetUSerDataCommand(messageManager,p["id"]));
            router.AddProtectedRoute(HttpMethod.Put,"/users/{id}", (r, p)=>new SetUserDataCommand(messageManager,p["id"],Deserialize<Dictionary<string,string>>(r.Payload)));
            //router.AddProtectedRoute(HttpMethod.Get,"/score", (r, p)=>new SetUserDataCommand(messageManager);

        }

        private static T Deserialize<T>(string payload) where T : class
        {
            var deserializedData = JsonConvert.DeserializeObject<T>(payload);
            return deserializedData;
        }
    }
}

