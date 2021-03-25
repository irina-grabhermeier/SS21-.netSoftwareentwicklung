using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD___Bowling_Game_Demo
{
    /// <summary>
    /// Florian Eckhart, Irina Grabher Meier
    /// </summary>
    public class Frame
    {
        private readonly List<int> _pins = new List<int>();

        private bool _isFinished = false;
        public bool IsFrameFinished
        {
            get {
                return _isFinished;
            }
        }

        public void AddRoll(int pins)
        {
            _pins.Add(pins);
            if (pins == 10)
            {
                _isStrike = true;
            }
            if (_pins.Count() >= 2 || IsStrike)
            {
                _isFinished = true;
            }
        }

        private bool _isStrike = false;
        public bool IsStrike
        {
            get { return _isStrike; }
        }

        public bool IsSpare
        {
            get {
                return _pins.Count() == 2 && _pins.Sum() == 10;
            }
        }

        public int NumRolls
        {
            get { return _pins.Count(); }
        }
        public int FirstRoll
        {
            get { return _pins.Any() ? _pins[0] : 0; }
        }
        public int SecondRoll
        {
            get { return _pins.Count() >= 2 ? _pins[1] : 0; }
        }

        public int Score
        {
            get
            {
                return _pins.Sum();
            }
        }
    }
}