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
    class BattleLogicTest
    {
        [Test]
        public void DoOneMonsterBattleRound()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,10);
            Card MonsterCard2 = new Monster(ElementType.Normal,MonsterType.Goblin,15);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(0,deckOne.Count);
            
        }
        [Test]
        public void DoGoblinAgainstDragonRound()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,100);
            Card MonsterCard2 = new Monster(ElementType.Normal,MonsterType.Dragon,10);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(0,deckOne.Count);
        }
          [Test]
        public void DoKnightAgainstWaterSpell()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Knight,100);
            Card SpellCard = new Spell(ElementType.Water,10);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(SpellCard);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(0,deckOne.Count);
        }
        [Test]
        public void DoSpellAgainstMonsterFireNormal()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Knight,50);
            Card SpellCard = new Spell(ElementType.Fire,25);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(SpellCard);
            var GameLogic= new GameLogic();

             // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(0,deckOne.Count);
        }
        [Test]
        public void DoKrakenAgainstNormalSpell()
        {
             // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Kraken,50);
            Card SpellCard = new Spell(ElementType.Fire,100);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(SpellCard);
            deckTwo.Add(MonsterCard);
            var GameLogic= new GameLogic();

             // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(0,deckOne.Count);
        }
          [Test]
        public void DoOneDraw()
        {
             // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Kraken,50);
            Card MonsterCard2 = new Monster(ElementType.Fire,MonsterType.Goblin,50);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard2);
            deckTwo.Add(MonsterCard);
            var GameLogic= new GameLogic();

             // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(1,deckTwo.Count);
        }  
          [Test]
        public void PlayGameWithNewDeckFromPackageGenerator(){
            var GameLogic= new GameLogic();
            var deckOne = PackageGenerator.generatePackage();
            var deckTwo = PackageGenerator.generatePackage();

            var sizeA = deckOne.Count();
            var sizeB = deckTwo.Count();

            GameLogic.playTheGame(deckOne, deckTwo);

            Assert.AreNotEqual(sizeA,deckOne.Count());
            Assert.AreNotEqual(sizeB,deckTwo.Count());



        }
        [Test]
        public void DoWHoleFightDeckAWin()
        {
             // arrange
            Card MonsterCardA1 = new Monster(ElementType.Normal,MonsterType.Kraken,150);
            Card MonsterCardA2 = new Monster(ElementType.Fire,MonsterType.Dragon,150);
            Card MonsterCardA3 = new Monster(ElementType.Water,MonsterType.Wizzard,150);
            Card SpellCardA4 = new Spell(ElementType.Water,100);
            Card SpellCardA5 = new Spell(ElementType.Fire,80);
            Card SpellCardB1 = new Spell(ElementType.Normal,0);
            Card SpellCardB2 = new Spell(ElementType.Fire,0);
            Card SpellCardB3 = new Spell(ElementType.Normal,0);
            Card MonsterCardB4 = new Monster(ElementType.Normal,MonsterType.Ork,0);
            Card MonsterCardB5 = new Monster(ElementType.Fire,MonsterType.Knight,0);
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCardA1);
            deckOne.Add(MonsterCardA2);
            deckOne.Add(MonsterCardA3);
            deckOne.Add(SpellCardA4);
            deckOne.Add(SpellCardA5);
            deckTwo.Add(SpellCardB1);
            deckTwo.Add(SpellCardB2);
            deckTwo.Add(SpellCardB3);
            deckTwo.Add(MonsterCardB4);
            deckTwo.Add(MonsterCardB5);
            var GameLogic= new GameLogic();

             // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(10,deckOne.Count);
        }
       
      
    }
}
