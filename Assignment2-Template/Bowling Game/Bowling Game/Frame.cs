using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD___Bowling_Game_Demo
{
    public class Frame
    {
        private readonly List<int> _pins = new List<int>();

        public bool IsFrameFinished
        {
            get { return false; }
        }

        public void AddRoll(int pins)
        {

        }

        public bool IsStrike
        {
            get { return false; }
        }

        public bool IsSpare
        {
            get { return false; }
        }

        public int NumRolls
        {
            get { return 0; }
        }
        public int FirstRoll
        {
            get { return 0; }
        }
        public int SecondRoll
        {
            get { return 0; }
        }

        public int Score
        {
            get
            {
                return 0;
            }
        }
    }
}