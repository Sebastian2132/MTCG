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
        IEnumerable<Card> ShowWholeDeck(User user);
    }
}
