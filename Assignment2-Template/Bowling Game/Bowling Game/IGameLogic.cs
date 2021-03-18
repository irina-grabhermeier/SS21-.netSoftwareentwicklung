using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD___Bowling_Game_Demo
{
    /// <summary>
    /// This interface defines the interface for any round-based game. 
    /// </summary>
    public interface IGameLogic
    {
        void Roll(int pins);

        int GetFinalScore();
    }

}
