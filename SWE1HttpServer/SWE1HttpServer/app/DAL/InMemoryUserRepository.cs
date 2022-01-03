using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.DAL
{
    public class InMemoryUserRepository : IUserRepository
    {
         private readonly List<User> users = new();

        public User GetUserByAuthToken(string authToken)
        {
            return users.SingleOrDefault(u => u.Token == authToken);
        }

        public User GetUserByCredentials(string username, string password)
        {
            return users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }

        public bool InsertUser(User user)
        {
            var inserted = false;

            if (GetUserByName(user.Username) == null)
            {
                users.Add(user);
                inserted = true;
            }

            return inserted;
        }

        IEnumerable<Card> IUserRepository.ShowActiveDeck(User user)
        {
            return user.MainDeck;
        }

        public void UpdateDeck(User user, IEnumerable<Card> package)
        {
           user.AllCards.AddRange(package);
        }

        private User GetUserByName(string username)
        {
            return users.SingleOrDefault(u => u.Username == username);
        }

        IEnumerable<Card> IUserRepository.ShowWholeDeck(User user)
        {
            return user.AllCards;
        }

        public void UpdateActiveDeck(User user, List<Card> cards)
        {
            user.MainDeck.AddRange(cards);
        }

        public List<Card> allCards(User user)
        {
            return user.AllCards;
        }

        public string GetUserInfo(User user)
        {
            string userInfo = "UserInfo:\n"+user.Username + "\n"+user.Bio+"\n"+user.Picture;
            return userInfo;
        }

        public void SetUserInfo(User user,string userName, string Bio, string picture)
        {
            user.Name = userName;
            user.Bio = Bio;
            user.Picture = picture;
        }
    }
}
