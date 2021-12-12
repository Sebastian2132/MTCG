using SWE1HttpServer.DAL;
using SWE1HttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer
{

    public class GameLogic
    {
        private readonly List<(ElementType, ElementType)> _elementRules = new List<(ElementType, ElementType)>{
             (ElementType.Fire,ElementType.Normal),
             (ElementType.Water,ElementType.Fire),
             (ElementType.Normal,ElementType.Water),

        };
        public void playTheGame(List<Card> deckOne, List<Card> deckTwo)
        {
            int rounds = 0;
            int index=0;
            string winDetermination="";
            (int, int) damageValues;
            Card card, card2;
            while (rounds < 101 && deckOne.Any() && deckTwo.Any())
            {
                card = deckOne[index];
                card2 = deckTwo[index];
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
                    deckTwo.RemoveAt(index);
                    winDetermination="a";



                }
                else if (damageValues.Item1 > damageValues.Item2)
                {
                    deckOne.Add(card);
                    deckOne.Add(card2);
                    deckOne.RemoveAt(index);
                    deckTwo.RemoveAt(index);
                    winDetermination="b";

                }
                switch(winDetermination){ 
                    case "a":
                    Console.WriteLine("DeckA won!\n Battle:"+card.getName()+" Damage: "+card.Damage+" against "+card2.getName()+" Damage: "+card2.Damage+"\n");break;
                    case "b":
                    Console.WriteLine("DeckB won!\n Battle:"+card.getName()+" Damage: "+card.Damage+" against "+card2.getName()+" Damage: "+card2.Damage+"\n");break;
                    default:
                    Console.WriteLine("It was a Draw!\n Battle:"+card.getName()+" Damage: "+card.Damage+" against "+card2.getName()+" Damage: "+card2.Damage+"\n");break;




                }
                rounds++;
            }


        }

        public (int, int) checkMonsterRules(Monster card1, Monster card2)
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

        public (int, int) checkMixedRules(Card card1, Card card2)
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
