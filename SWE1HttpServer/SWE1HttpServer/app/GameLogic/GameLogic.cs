using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.Models
{

    public class GameLogic
    {
        public bool finished = false;
        public string winner = "";
        private readonly List<(ElementType, ElementType)> _elementRules = new List<(ElementType, ElementType)>{
             (ElementType.Fire,ElementType.Normal),
             (ElementType.Water,ElementType.Fire),
             (ElementType.Normal,ElementType.Water),

        };
        public void playTheGame(List<Card> deckOne, List<Card> deckTwo)
        {
            int rounds = 0;
           
            (float, float) damageValues;
            Card card, card2;
            Random random = new Random();
            Logger log = new Logger();
            while (rounds < 101 && deckOne.Any() && deckTwo.Any())
            {
                int index = random.Next(deckOne.Count() - 1);
                int index2 = random.Next(deckTwo.Count() - 1);
                card = deckOne[index];
                card2 = deckTwo[index2];
                if (card.type == CardType.Monster && card2.type == CardType.Monster)
                {
                    damageValues = checkMonsterRules((Monster)card, (Monster)card2);

                }
                else
                {

                    damageValues = checkMixedRules(card, card2);

                }

                if (damageValues.Item1 < damageValues.Item2)
                {


                    deckTwo.Add(card);
                    deckTwo.Add(card2);
                    deckOne.RemoveAt(index);
                    deckTwo.RemoveAt(index2);
                    log.GameLog(card, card2, damageValues.Item1, damageValues.Item2);




                }
                else if (damageValues.Item1 > damageValues.Item2)
                {

                    deckOne.Add(card);
                    deckOne.Add(card2);
                    deckOne.RemoveAt(index);
                    deckTwo.RemoveAt(index2);
                    log.GameLog(card, card2, damageValues.Item1, damageValues.Item2);



                }
                else
                {
                    log.GameLog(card, card2, damageValues.Item1, damageValues.Item2);
                }

                rounds++;
            }
            if (deckOne.Any() && !deckTwo.Any())
            {
                finished = true;
                winner = "A";

                //Give the log back to client here!!
                Console.WriteLine(log.getGameLog("A"));


            }
            else if (!deckOne.Any() && deckTwo.Any())
            {
                finished = true;
                winner = "B";

                //Give the log back to client here!!
                Console.WriteLine(log.getGameLog("B"));

            }
            else 
            {
                finished = true;
                winner = "D";
                //Give the log back to client here!!

                Console.WriteLine(log.getGameLog("D"));

            }
            //log.clearLog();


        }

        public (float, float) checkMonsterRules(Monster card1, Monster card2)
        {
            if ((card1.monsterType == MonsterType.Goblin && card2.monsterType == MonsterType.Dragon))
            {
                return (0, card2.Damage);

            }
            else if ((card1.monsterType == MonsterType.Dragon && card2.monsterType == MonsterType.Goblin))
            {

                return (card1.Damage, 0);

            }
            else if ((card1.monsterType == MonsterType.Wizzard && card2.monsterType == MonsterType.Ork))
            {
                return (card1.Damage, 0);

            }
            else if ((card1.monsterType == MonsterType.Ork && card2.monsterType == MonsterType.Wizzard))
            {
                return (0, card2.Damage);

            }
            else if ((card1.monsterType == MonsterType.Elve) && (card1.element == ElementType.Fire) && (card2.monsterType == MonsterType.Dragon))
            {
                return (card1.Damage, 0);

            }
            else if ((card1.monsterType == MonsterType.Dragon) && (card2.element == ElementType.Fire) && (card2.monsterType == MonsterType.Elve))
            {
                return (0, card2.Damage);
            }
            else
            {
                return (card1.Damage, card2.Damage);
            }

        }

        public (float, float) checkMixedRules(Card card1, Card card2)
        {
            //Special Fighting Rules
            if ((card1.type == CardType.Monster) && (((Monster)card1).monsterType == MonsterType.Knight) && (card2.element == ElementType.Water))
            {

                return (0, card2.Damage);

            }
            else if ((card2.type == CardType.Monster) && (((Monster)card2).monsterType == MonsterType.Knight) && (card1.element == ElementType.Water))
            {

                return (card1.Damage, 0);

            }
            else if (card1.type == CardType.Monster && ((Monster)card1).monsterType == MonsterType.Kraken)
            {
                return (card1.Damage, 0);

            }
            else if (card2.type == CardType.Monster && ((Monster)card2).monsterType == MonsterType.Kraken)
            {
                return (0, card2.Damage);

            }
            else
            {
                //Generall elemental rules 

                if (_elementRules.Contains((card1.element, card2.element)))
                {
                    return (card1.Damage * 2, card2.Damage / 2);


                }
                else if (_elementRules.Contains((card2.element, card1.element)))
                {
                    return (card1.Damage / 2, card2.Damage * 2);

                }
                else
                {
                    return (card1.Damage, card2.Damage);
                }

            }

        }

    }
}
