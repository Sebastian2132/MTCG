using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWE1HttpServer
{
    public class MessageManager : IMessageManager
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;
        private readonly IPackageRepository packageRepository;

        public MessageManager(IMessageRepository messageRepository, IUserRepository userRepository, IPackageRepository packageRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.packageRepository = packageRepository;
        }

        public User LoginUser(Credentials credentials)
        {
            var user = userRepository.GetUserByCredentials(credentials.Username, credentials.Password);
            return user ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User()
            {
                Username = credentials.Username,
                Password = credentials.Password,
                Coins = 20,
                Score = 100,
                AllCards = new List<Card>()

            };
            if (userRepository.InsertUser(user) == false)
            {
                throw new DuplicateUserException();
            }
        }

        public Message AddMessage(User user, string content)
        {
            var message = new Message() { Content = content };
            messageRepository.InsertMessage(user.Username, message);

            return message;
        }

        public IEnumerable<Message> ListMessages(User user)
        {
            return messageRepository.GetMessages(user.Username);
        }

        public void RemoveMessage(User user, int messageId)
        {
            if (messageRepository.GetMessageById(user.Username, messageId) != null)
            {
                messageRepository.DeleteMessage(user.Username, messageId);
            }
            else
            {
                throw new MessageNotFoundException();
            }
        }

        public Message ShowMessage(User user, int messageId)
        {
            Message message;
            return (message = messageRepository.GetMessageById(user.Username, messageId)) != null
                ? message
                : throw new MessageNotFoundException();
        }

        public void UpdateMessage(User user, int messageId, string content)
        {
            Message message;
            if ((message = messageRepository.GetMessageById(user.Username, messageId)) != null)
            {
                message.Content = content;
                messageRepository.UpdateMessage(user.Username, message);
            }
            else
            {
                throw new MessageNotFoundException();
            }
        }
        public IEnumerable<Card> AquirePackages(User user)
        {
            List<Card> package = new List<Card>();
            if (user.Coins >= 5)
            {
                package = packageRepository.getPackageFromDb();
                userRepository.UpdateDeck(user, package);
                user.Coins -= 5;
            }
            return package;
        }

        public IEnumerable<Card> ShowWholeDeck(User user)
        {
            return userRepository.ShowWholeDeck(user);

        }
        public void AddPackage(List<Card> package)
        {
            packageRepository.addPackageToDb(package);

        }

        public List<Card> MakePackage(List<TestCard> package)
        {
            List<Card> finalPackage = new List<Card>();
            foreach (var p in package)
            {
                string element, type;
                string[] fullInfo = Regex.Split(p.Name, @"(?<!^)(?=[A-Z])");
                if (fullInfo.Length > 1)
                {
                    element = fullInfo[0];
                    type = fullInfo[1];
                    if (type == "Spell")
                    {
                        Spell card = new Spell(getElement(element), p.Damage);
                        finalPackage.Add(card);

                    }
                    else
                    {
                        Monster card = new Monster(getElement(element), getMonsterType(type), p.Damage);
                        finalPackage.Add(card);


                    }
                }else{
                    type = fullInfo[0];
                    Monster card = new Monster(ElementType.Normal, getMonsterType(type), p.Damage);
                    finalPackage.Add(card);
                }


            }
            return finalPackage;
        }

        private MonsterType getMonsterType(string type)
        {
            switch (type)
            {
                case "Goblin": return MonsterType.Goblin;
                case "Dragon": return MonsterType.Dragon;
                case "Wizzard": return MonsterType.Wizzard;
                case "Ork": return MonsterType.Ork;
                case "Knight": return MonsterType.Knight;
                case "Kraken": return MonsterType.Kraken;
                case "Elve": return MonsterType.Elve;
                case "Troll": return MonsterType.Troll;
                default: return MonsterType.Goblin;
            }
        }

        private ElementType getElement(string element)
        {
            switch (element)
            {
                case "Water": return ElementType.Water;
                case "Fire": return ElementType.Fire;
                default: return ElementType.Normal;
            }
        }
    }
}
