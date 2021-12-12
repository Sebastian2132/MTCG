using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.Models;





namespace SWE1HttpServer.Models
{
    public class Monster : Card
    {
        public MonsterType monsterType{ get; set;}
        public Monster(ElementType element, MonsterType MonsterType, int damage)
        {
            this.element = element;
            this.monsterType = MonsterType;
            this.Damage = damage;
            this.type = CardType.Monster;

        }
        public override string getName(){
            string name = "";
            name+=element.ToString()+monsterType.ToString();
            return name;
        }

    }
}