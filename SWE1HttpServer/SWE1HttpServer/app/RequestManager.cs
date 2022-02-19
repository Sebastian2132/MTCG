using Newtonsoft.Json;
using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using SWE1HttpServer.core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWE1HttpServer
{
    public class RequestManager : IRequestManager
    {

        private readonly IUserRepository userRepository;
        private readonly IPackageRepository packageRepository;
        private readonly GameLogic gameLogic;
        private Queue<Tuple<User, List<Card>>> battleQueue;

        public RequestManager(IUserRepository userRepository, IPackageRepository packageRepository, GameLogic gamelogic)
        {
            this.userRepository = userRepository;
            this.packageRepository = packageRepository;
            this.gameLogic = gamelogic;
            this.battleQueue = new Queue<Tuple<User, List<Card>>>();
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
                AllCards = new List<Card>(),
                MainDeck = new List<Card>()

            };
            if (userRepository.InsertUser(user) == false)
            {
                throw new DuplicateUserException();
            }
        }


        public IEnumerable<Card> AquirePackages(User user)
        {
            //User coins gehen noch nicht
            List<Card> package = new List<Card>();

            if (user.Coins >= 5)
            {
                package = packageRepository.GetPackage();
                userRepository.UpdateDeck(user, package);
                userRepository.UpdateUserCoins(user.Username);
            }
            return package;
        }

        public IEnumerable<Card> ShowWholeDeck(User user)
        {
            return userRepository.ShowWholeDeck(user);

        }
        public void AddPackage(List<Card> package)
        {
            packageRepository.AddPackage(package);

        }
        public bool setDeck(string cards, User user)
        {
            List<Card> newActiveDeck = new List<Card>();
            Card card;
            //check somewhere where we check for authToken=username
            //apparentally not implemented
            List<Card> allCards = userRepository.allCards(user);
            string[] fullInfo = JsonConvert.DeserializeObject<string[]>(cards);
            if (fullInfo.Length <= 3)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < fullInfo.Length; i++)
                {
                    card = allCards.Find(x => x.Id == fullInfo[i]);
                    if (card == null)
                    {
                        return false;
                    }
                    newActiveDeck.Add(card);
                }
                userRepository.UpdateActiveDeck(user, newActiveDeck);
                return true;
            }



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
                        Spell card = new Spell(getElement(element), p.Damage, p.Id);
                        finalPackage.Add(card);

                    }
                    else
                    {
                        Monster card = new Monster(getElement(element), getMonsterType(type), p.Damage, p.Id);
                        finalPackage.Add(card);


                    }
                }
                else
                {
                    type = fullInfo[0];
                    Monster card = new Monster(ElementType.Normal, getMonsterType(type), p.Damage, p.Id);
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

        public List<Card> ShowActiveDeck(User user)
        {
            return userRepository.ShowActiveDeck(user);
        }



        public string GetUserInfo(User user, string userName)
        {
            if (user.Username != userName)
            {
                throw new RouteNotAuthorizedException();

            }
            else
            {
                return userRepository.GetUserInfo(user);
            }
        }

        public bool SetUserInfo(User user, string userName, Dictionary<string, string> info)
        {
            if (user.Username != userName)
            {
                return false;
            }
            else
            {
                userRepository.SetUserInfo(user, info["Name"], info["Bio"], info["Image"]);
                return true;
            }


        }
        
        public bool GetScoreboard(User user, string userName, Dictionary<string, string> info)
        {
            if (user.Username != userName)
            {
                return false;
            }
            else
            {
                userRepository.SetUserInfo(user, info["Name"], info["Bio"], info["Image"]);
                return true;
            }


        }

        public void StartBattle(User user)
        {
            var deck = userRepository.ShowActiveDeck(user);
            if (battleQueue.TryDequeue(out var playerOne) && (playerOne.Item1.Username == user.Username)) 
            {
                battleQueue.Enqueue(playerOne);
                throw new InvalidOperationException();

            }
            else if (playerOne != null)
            {
                gameLogic.playTheGame(playerOne.Item2, deck);
            }
            else
            {

                battleQueue.Enqueue(new Tuple< User, List<Card>>(user,deck));
            }
        }

        public bool checkBattle(){

            return gameLogic.comp;
        }

        public string GetStat(User user)
        {
         
                return userRepository.GetStat(user);

            
        }
    }
}
