using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.Models
{
     public enum ElementType
    {
        Water,
        Fire,
        Normal

    };
    public enum MonsterType
    {
        Goblin,
        Dragon,
        Wizzard,
        Ork,
        Knight,
        Kraken,
        Elve,
        Troll
    };
    public enum CardType
    {
        Monster,
        Spell
    };
   
    public abstract class Card
    {
        public int Damage ;
        public ElementType element;
        public CardType type;
        public abstract string getName();
        public abstract string getCardNameWithoutElement();

        public abstract string toString();


    }
}
