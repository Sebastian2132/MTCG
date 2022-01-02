using SWE1HttpServer.app.Models;
using System.Collections.Generic;

namespace SWE1HttpServer
{
    public interface IMessageManager
    {
        Message AddMessage(User user, string content);
        IEnumerable<Message> ListMessages(User user);
        User LoginUser(Credentials credentials);
        void RegisterUser(Credentials credentials);
        IEnumerable<Card> AquirePackages(User user);
        IEnumerable<Card> ShowWholeDeck(User user);
        IEnumerable<Card> ShowActiveDeck(User user);
        void RemoveMessage(User user, int messageId);
        Message ShowMessage(User user, int messageId);
        void UpdateMessage(User user, int messageId, string content);
        void AddPackage(List<Card> package);
        bool setDeck(string cards,User user);
        List<Card> MakePackage(List<TestCard> package);
    }
}
