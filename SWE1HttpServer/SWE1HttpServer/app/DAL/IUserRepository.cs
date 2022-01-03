using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.DAL
{
    public interface IUserRepository
    {
         User GetUserByCredentials(string username, string password);

        User GetUserByAuthToken(string authToken);

        bool InsertUser(User user);
        void UpdateDeck(User user,IEnumerable<Card> package);
        void UpdateActiveDeck(User user,List<Card> cards);
        IEnumerable<Card> ShowWholeDeck(User user);
        IEnumerable<Card> ShowActiveDeck(User user);
        string GetUserInfo(User user);
        void SetUserInfo(User user,string userName,string Bio,string picture);

        List<Card> allCards(User user);
    }
}
