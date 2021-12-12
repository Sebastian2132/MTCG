using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.Models;





namespace SWE1HttpServer.Models
{
    public class Spell : Card
    {
        public Spell(ElementType element, int damage)
        {
            this.element = element;
            this.Damage = damage;
            this.type = CardType.Spell;

        }

            public override string getName(){
            string name = "";
            name+=element.ToString()+type.ToString();
            return name;
        }
    }
}