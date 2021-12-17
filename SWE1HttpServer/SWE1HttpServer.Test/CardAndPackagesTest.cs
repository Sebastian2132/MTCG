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
            Monster MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,10);
            string result = "";

            // act
            result=MonsterCard.getName();
            // assert
            Assert.AreEqual("NormalGoblin",result);
        }
        [Test]
        public void CreateOneNewSpellCardAndReturnValues()
        {
            // arrange
            Spell MonsterCard = new Spell(ElementType.Fire,10);
            string result = "";

            // act
            result=MonsterCard.getName();
            // assert
            Assert.AreEqual("FireSpell",result );
        }
        [Test]
        public void CreateOnePackageAndReturnIt()
        {
            // arrange
           var package= PackageGenerator.generatePackage();

            // act
            
            // assert
            Assert.AreEqual(5,package.Count() );
        }

        
    }
}
