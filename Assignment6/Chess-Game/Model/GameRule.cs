using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class GameRule
    {
        private List<Predicate<ChessCellModel>> _cellFilters = 
            new List<Predicate<ChessCellModel>>();
        
        private List<Func<ChessCellModel, ChessCellModel, IChessBoard, bool>> _validMoves = 
            new List<Func<ChessCellModel, ChessCellModel, IChessBoard, bool>>();

        private List<Func<ChessCellModel, ChessCellModel, IChessBoard, bool>> _invalidMoves =
            new List<Func<ChessCellModel, ChessCellModel, IChessBoard, bool>>();

        public static GameRule Create()
        {
            return new GameRule();
        }

        public GameRule Filter(Predicate<ChessCellModel> filterExpression)
        {
            _cellFilters.Add(filterExpression);
            return this;
        }

        public GameRule ValidMove(Func<ChessCellModel, ChessCellModel, IChessBoard, bool> move)
        {
            _validMoves.Add(move);
            return this;
        }

        public GameRule InvalidMove(Func<ChessCellModel, ChessCellModel, IChessBoard, bool> move)
        {
            _invalidMoves.Add(move);
            return this;
        }


        public bool AppliesTo(ChessCellModel source)
        {
            foreach(var filter in _cellFilters)
            {
                if (filter(source))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInvalidMove(ChessCellModel source, ChessCellModel target, IChessBoard chessboard)
        {
            foreach (var invalidMove in _invalidMoves)
            {
                if (invalidMove(source, target, chessboard))
                {
                    return true;
                }
            }

            // We can not infer if it is valid, this means we miss a rule. 
            //
            //throw new ArgumentException("Rule is missing.");
            return false;
        }

        public bool IsValidMove(ChessCellModel source, ChessCellModel target, IChessBoard chessboard)
        {

            foreach(var validMove in _validMoves)
            {
                if (validMove(source, target, chessboard))
                {
                    return true;
                }
            }

            // We can not infer if it is valid, this means we miss a rule. 
            //
            //throw new ArgumentException("Rule is missing.");
            return false;
        }
    }
}
