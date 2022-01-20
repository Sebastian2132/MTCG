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
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,10,"dsf");
            Card MonsterCard2 = new Monster(ElementType.Normal,MonsterType.Goblin,15,"dsfs");
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
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,100,"dsf");
            Card MonsterCard2 = new Monster(ElementType.Normal,MonsterType.Dragon,10,"dsfs");
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
        public void DoDrawWithElement()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Water,MonsterType.Goblin,10,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Fire,40,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(1,deckOne.Count);
        }
          [Test]
        public void DoWinWithElement()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Water,MonsterType.Goblin,30,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Fire,40,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(2,deckOne.Count);
        }
                [Test]
        public void DoBattleSameElementDraw()
        {
              // arrange
            Card MonsterCardA1 = new Monster(ElementType.Normal,MonsterType.Dragon,150,"dsfs");
            Card MonsterCardA2 = new Monster(ElementType.Normal,MonsterType.Dragon,150,"dsfss");
            Card MonsterCardA3 = new Monster(ElementType.Normal,MonsterType.Wizzard,150,"dsfsa");
            Card SpellCardA4 = new Spell(ElementType.Normal,150,"dsfsw");
            Card SpellCardA5 = new Spell(ElementType.Normal,150,"dsfsd");
            Card SpellCardB1 = new Spell(ElementType.Normal,150,"dsfsasd");
            Card SpellCardB2 = new Spell(ElementType.Normal,150,"dsasdfs");
            Card SpellCardB3 = new Spell(ElementType.Normal,150,"dssadfs");
            Card MonsterCardB4 = new Monster(ElementType.Normal,MonsterType.Knight,150,"dsadsfs");
            Card MonsterCardB5 = new Monster(ElementType.Normal,MonsterType.Knight,150,"dsfsa");
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
            Assert.AreEqual(5,deckOne.Count);
        }
        [Test]
        public void DoWaterAgainstFire()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Water,MonsterType.Goblin,40,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Fire,40,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(2,deckOne.Count);
        }
        
        [Test]
        public void DoMixedFight()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Water,MonsterType.Goblin,40,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Fire,40,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(2,deckOne.Count);
        }
        [Test]
        public void DoNormalVsNormal()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,40,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Normal,40,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(1,deckOne.Count);
        }
          [Test]
        public void DoNormalVsWater()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Goblin,50,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Water,50,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(2,deckOne.Count);
        }  [Test]
        public void DoFirevsNormal()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Fire,MonsterType.Goblin,40,"dsf");
            Card MonsterCard2 = new Spell(ElementType.Normal,50,"dsfs");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard);
            deckTwo.Add(MonsterCard2);
            var GameLogic= new GameLogic();

            // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(2,deckOne.Count);
        }
          [Test]
        public void DoKnightAgainstWaterSpell()
        {
            // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Knight,100,"dsf");
            Card SpellCard = new Spell(ElementType.Water,10,"dsfs");
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
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Knight,50,"dsf");
            Card SpellCard = new Spell(ElementType.Fire,25,"dsfa");
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
        public void DoWizzardVsOrkBattle()
        {
             // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Wizzard,50,"dsf");
            Card MonsterCard1 = new Monster(ElementType.Normal,MonsterType.Ork,50,"dsf");
            var deckOne= new List<Card>();
            var deckTwo= new List<Card>();
            deckOne.Add(MonsterCard1);
            deckTwo.Add(MonsterCard);
            var GameLogic= new GameLogic();

             // act
            GameLogic.playTheGame(deckOne, deckTwo);
            // assert
            Assert.AreEqual(0,deckOne.Count);
        }
        [Test]
        public void DoKrakenVsSpell()
        {
             // arrange
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Kraken,50,"dsf");
            Card SpellCard = new Spell(ElementType.Fire,100,"dsfs");
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
            Card MonsterCard = new Monster(ElementType.Normal,MonsterType.Kraken,50,"dsf");
            Card MonsterCard2 = new Monster(ElementType.Fire,MonsterType.Goblin,50,"dsfasd");
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
        public void DoWHoleFightDeckAWin()
        {
             // arrange
            Card MonsterCardA1 = new Monster(ElementType.Normal,MonsterType.Kraken,150,"dsfs");
            Card MonsterCardA2 = new Monster(ElementType.Fire,MonsterType.Dragon,150,"dsfss");
            Card MonsterCardA3 = new Monster(ElementType.Water,MonsterType.Wizzard,150,"dsfsa");
            Card SpellCardA4 = new Spell(ElementType.Water,100,"dsfsw");
            Card SpellCardA5 = new Spell(ElementType.Fire,80,"dsfsd");
            Card SpellCardB1 = new Spell(ElementType.Normal,0,"dsfsasd");
            Card SpellCardB2 = new Spell(ElementType.Fire,0,"dsasdfs");
            Card SpellCardB3 = new Spell(ElementType.Normal,0,"dssadfs");
            Card MonsterCardB4 = new Monster(ElementType.Normal,MonsterType.Ork,0,"dsadsfs");
            Card MonsterCardB5 = new Monster(ElementType.Fire,MonsterType.Knight,0,"dsfsa");
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
          [Test]
        public void DoWHoleFightDeckBWin()
        {
             // arrange
            Card MonsterCardA1 = new Monster(ElementType.Normal,MonsterType.Knight,0,"dsfs");
            Card MonsterCardA2 = new Monster(ElementType.Fire,MonsterType.Dragon,0,"dsfss");
            Card MonsterCardA3 = new Monster(ElementType.Water,MonsterType.Wizzard,0,"dsfsa");
            Card SpellCardA4 = new Spell(ElementType.Water,0,"dsfsw");
            Card SpellCardA5 = new Spell(ElementType.Fire,0,"dsfsd");
            Card SpellCardB1 = new Spell(ElementType.Normal,30,"dsfsasd");
            Card SpellCardB2 = new Spell(ElementType.Fire,40,"dsasdfs");
            Card SpellCardB3 = new Spell(ElementType.Normal,50,"dssadfs");
            Card MonsterCardB4 = new Monster(ElementType.Normal,MonsterType.Ork,0,"dsadsfs");
            Card MonsterCardB5 = new Monster(ElementType.Fire,MonsterType.Knight,0,"dsfsa");
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
            Assert.AreEqual(10,deckTwo.Count);
        }
       
      
    }
}
 