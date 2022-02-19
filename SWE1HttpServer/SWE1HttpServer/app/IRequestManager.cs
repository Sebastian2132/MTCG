using SWE1HttpServer.app.Models;
using System.Collections.Generic;

namespace SWE1HttpServer
{
    public interface IRequestManager
    {
    
        User LoginUser(Credentials credentials);
        void RegisterUser(Credentials credentials);
        IEnumerable<Card> AquirePackages(User user);
        IEnumerable<Card> ShowWholeDeck(User user);
        List<Card> ShowActiveDeck(User user);
       
               
        void AddPackage(List<Card> package);
        bool setDeck(string cards,User user);
        List<Card> MakePackage(List<TestCard> package);
        string GetUserInfo(User user,string userName);

        bool SetUserInfo(User user,string userName,Dictionary<string, string> info);

        void StartBattle(User user);
        
        bool checkBattle();

        string GetStat(User user);
    }
}
