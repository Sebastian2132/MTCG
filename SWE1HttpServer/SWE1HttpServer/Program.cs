using System;
using SWE1HttpServer.DAL;
using SWE1HttpServer.Models;

namespace SWE1HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var userRepository = new InMemoryUserRepository();
            var user = new User();
            var messageRepository = new InMemoryMessageRepository();
            var messageManager = new MessageManager(messageRepository,userRepository);

            user.Username=("skrt");
            user.Password=("skrt");
            messageManager.AddMessage(user,"hello world");
            messageManager.AddMessage(user,"test"); */
            
            var messages = PackageGenerator.generatePackage();

            foreach (var message in messages)
            {
                Console.WriteLine(message.getName());
            }
        }
    }
}
