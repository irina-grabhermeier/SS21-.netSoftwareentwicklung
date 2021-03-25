using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD___Bowling_Game;

namespace TDD___Bowling_Game_Demo
{
    /// <summary>
    /// Florian Eckhart, Irina Grabher Meier
    /// </summary>
    public class BowlingGameLogic : IGameLogic
    {
        private List<Frame> _frames = new List<Frame>();
        private Frame _currentFrame = new Frame();

        public void Roll(int pins)
        {
            _currentFrame.AddRoll(pins);
            if (_currentFrame.IsFrameFinished)
            {
                _frames.Add(_currentFrame);
                _currentFrame = new Frame();
            } 
        }

        public int GetFinalScore()
        {
            Frame prevFrame = _frames[0];
            bool isSecondRollDoubleMissing = false;
            int totalScore = prevFrame.Score;

            for (int i = 1; i < _frames.Count(); i++)
            {
                Frame curFrame = _frames[i];
                totalScore += curFrame.Score;
                if (prevFrame.IsStrike && i < 10)
                {
                    totalScore += curFrame.Score;
                    if (curFrame.IsStrike)
                    {
                        isSecondRollDoubleMissing = true;
                    }
                }
                else if (prevFrame.IsSpare && i < 10)
                {
                    totalScore += curFrame.FirstRoll;
                }

                if (isSecondRollDoubleMissing)
                {
                    totalScore += curFrame.FirstRoll;
                    isSecondRollDoubleMissing = false;
                }
            }

            return totalScore + _currentFrame.Score;
        }
    }
}
