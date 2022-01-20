using NUnit.Framework;
using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SWE1HttpServer.Test
{
    [TestFixture]
    class CardAndPackagesTest
    {
        [Test]
        public void CreateOneNewMonsterCardAndReturnValues()
        {
            // arrange
            Monster MonsterCard = new Monster(ElementType.Normal, MonsterType.Goblin, 10, "dsfs");
            string result = "";

            // act
            result = MonsterCard.getName();
            // assert
            Assert.AreEqual("NormalGoblin", result);
        }
        [Test]
        public void CreateOneNewSpellCardAndReturnValues()
        {
            // arrange
            Spell MonsterCard = new Spell(ElementType.Fire, 10, "dsfs");
            string result = "";

            // act
            result = MonsterCard.getName();
            // assert
            Assert.AreEqual("FireSpell", result);
        }
            [Test]
            public void CreateDeck()
            {
                // arrange
                List<Card> deck= new();
                Spell MonsterCard = new Spell(ElementType.Fire, 10, "dsfs");
                Spell MonsterCard2 = new Spell(ElementType.Fire, 10, "dsfs");
                Spell MonsterCard3 = new Spell(ElementType.Fire, 10, "dsfs");
                Spell MonsterCard4 = new Spell(ElementType.Fire, 10, "dsfs");
                deck.Add(MonsterCard);
                deck.Add(MonsterCard2);
                deck.Add(MonsterCard3);
                deck.Add(MonsterCard4);

     
                // assert
                Assert.AreEqual(4, deck.Count());
            }
        }



    }

