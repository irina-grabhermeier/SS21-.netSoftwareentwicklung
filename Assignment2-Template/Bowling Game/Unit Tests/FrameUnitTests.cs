using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD___Bowling_Game_Demo;

namespace TDD___Bowling_Game_Unit_Tests
{
    [TestClass]
    public class FrameUnitTests
    {
        [TestMethod]
        [TestCategory("Frame")]
        public void TestFrameFinished()
        {
            var frame = new Frame();
            Assert.IsFalse(frame.IsFrameFinished);
            frame.AddRoll(5);
            Assert.IsFalse(frame.IsFrameFinished);
            frame.AddRoll(1);
            Assert.IsTrue(frame.IsFrameFinished);
        }

        [TestMethod]
        [TestCategory("Frame")]
        public void TestFrameFinishedAfterStrike()
        {
            // A frame is finished after a strike in the first roll. 
            //
            var frame = new Frame();
            Assert.IsFalse(frame.IsFrameFinished);
            frame.AddRoll(10);
            Assert.IsTrue(frame.IsFrameFinished);
        }

        [TestMethod]
        [TestCategory("Frame")]
        public void TestIsStrike()
        {
            // A frame is finished after a strike in the first roll. 
            //
            var frame = new Frame();
            frame.AddRoll(10);
            Assert.IsTrue(frame.IsStrike);
            Assert.IsFalse(frame.IsSpare);
        }

        [TestMethod]
        [TestCategory("Frame")]
        public void TestIsSpare()
        {
            // A frame is finished after a strike in the first roll. 
            //
            var frame = new Frame();
            frame.AddRoll(5);
            frame.AddRoll(5);
            Assert.IsFalse(frame.IsStrike);
            Assert.IsTrue(frame.IsSpare);
        }

        [TestMethod]
        [TestCategory("Frame")]
        public void TestScore()
        {
            var frame1 = new Frame();
            frame1.AddRoll(10);
            Assert.IsTrue(frame1.Score == 10);

            var frame2 = new Frame();
            frame2.AddRoll(3);
            frame2.AddRoll(4);
            Assert.IsTrue(frame2.Score == 7);
        }
    }
}
