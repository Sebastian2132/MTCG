using SWE1HttpServer.DAL;
using SWE1HttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer
{
    public class PackageGenerator
    {
        public static List<Card> generatePackage(){
            var cards = new List<Card>();
            Random random = new Random();
            Card card;

            while(cards.Count() < 5){
            int attackDamage = random.Next(1,100);
            var elementType = (ElementType)(random.Next()%3);
            var cardType = random.Next()%2;
            if(cardType==0){
                var monsterType = (MonsterType)random.Next(1,3);
                card = new Monster(elementType,monsterType,attackDamage);
            
            }else{

                card = new Spell(elementType,attackDamage);
            }
            cards.Add(card);
            }



            return cards;

        }
            
    }
}
