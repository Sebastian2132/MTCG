using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.app.Models;




namespace SWE1HttpServer.app.Models
{
    public class Spell : Card
    {
        public Spell(ElementType element, float damage,string Id)
        {
            this.element = element;
            this.Damage = damage;
            this.type = CardType.Spell;
            this.Id=Id;

        }

            public override string getName(){
            string name = "";
            name+=element.ToString()+type.ToString();
            return name;
        }
        public override string getCardNameWithoutElement(){
            string name = "";
            name+=type.ToString();
            return name;
        }
        public override string toString(){ 
            return "Name: "+ this.getName()+", Damage: "+ this.Damage+"\n";
        }
    }
}