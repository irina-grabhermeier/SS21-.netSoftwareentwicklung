using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class GameEndedEventArgs : EventArgs
    {
        public EActivePlayer _winner;

        public EActivePlayer Winner
        {
            get { return _winner; }
        }

        public GameEndedEventArgs(EActivePlayer winner)
        {
            _winner = winner;
        }
    }
}
