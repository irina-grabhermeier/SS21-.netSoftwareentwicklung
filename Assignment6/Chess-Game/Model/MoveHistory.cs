using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise_2.Annotations;

namespace Exercise_2
{
    public class Move {

        public CellId Source { get; }
        public CellId Target { get; }

        public Move(CellId source, CellId target)
        {
            Source = source;
            Target = target;
        }
    }

    public class MoveHistory : IEnumerable<Move>
    {
        public event EventHandler HistoryUpdated = delegate { };

        public List<Move> _history = new List<Move>();
        public List<Move> History
        {
            get { return _history; }
        }

        public void AddMove(Move move)
        {
            _history.Add(move);
            HistoryUpdated(this, EventArgs.Empty);
        }

        public IEnumerator<Move> GetEnumerator()
        {
            for (int index = 0; index < _history.Count; index++)
            {
                yield return _history[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
