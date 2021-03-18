using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD___Bowling_Game;

namespace TDD___Bowling_Game_Demo
{
    public class BowlingGameLogic : IGameLogic
    {
        private List<Frame> _frames = new List<Frame>();
        private Frame _currentFrame = new Frame();

        public void Roll(int pins)
        {
        }

        public int GetFinalScore()
        {
            return 0;
        }
    }
}
