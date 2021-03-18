using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD___Bowling_Game_Demo; // Need to import the namespace of our tested classes.

namespace TDD___Bowling_Game_Unit_Tests
{
    [TestClass]
    public class GameLogicUnitTests
    {
        private void RollMany(int nrolls, int pins, IGameLogic gameLogic)
        {
            for (int i = 0; i < nrolls; i++)
            {
                gameLogic.Roll(pins);
            }
        }

        [TestMethod]
        public void TestAllZeros()
        {
            var game = new BowlingGameLogic();
            RollMany(20, 0, game);
            Assert.IsTrue(game.GetFinalScore() == 0);
        }

        [TestMethod]
        public void TestAllOnes()
        {
            var game = new BowlingGameLogic();
            RollMany(20, 1, game);
            Assert.IsTrue(game.GetFinalScore() == 20);
        }

        [TestMethod]
        public void TestSpareBonus()
        {
            var game = new BowlingGameLogic();
            game.Roll(5);
            game.Roll(5); // spare bonus is active for next roll
            game.Roll(7);
            game.Roll(3); // spare bonus is active for next roll
            game.Roll(1);
            RollMany(15, 0, game);
            var score = game.GetFinalScore();
            Assert.IsTrue(score == 21 + 7 + 1);
        }

        [TestMethod]
        public void TestStrikeBonus()
        {
            var game = new BowlingGameLogic();
            game.Roll(10); // strike bonus active for next two rolls
            game.Roll(3);
            game.Roll(4);
            RollMany(16, 0, game);
            var score = game.GetFinalScore();

            Assert.IsTrue(score == 17 + 3 + 4);
        }

        [TestMethod]
        public void TestBonusFrameAfterStrike()
        {
            var game = new BowlingGameLogic();
            RollMany(18, 0, game);
            game.Roll(10);
            // A strike in the last frame gives two bonus rolls. 
            //
            game.Roll(5);
            game.Roll(10);

            var score = game.GetFinalScore();
            Assert.IsTrue(score == 10 + 5 + 10);
        }

        [TestMethod]
        public void TestBonusFrameAfterSpear()
        {
            var game = new BowlingGameLogic();
            RollMany(18, 0, game);
            game.Roll(5);
            game.Roll(5);
            // A spear in the last frame gives one bonus roll.
            //
            game.Roll(3);
            
            var score = game.GetFinalScore();
            Assert.IsTrue(score == 10 + 3);
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            var game = new BowlingGameLogic();
            RollMany(12, 10, game);
            
            var score = game.GetFinalScore();
            Assert.IsTrue(score == 300);
        }


       
    }
}
