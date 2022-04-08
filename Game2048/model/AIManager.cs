using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    enum AIStrategy
    {
        SNAKE,
        CORNER
    }
    class AIManager
    {
        private const int SearchDepth = 4;

        private Direction[] directions = { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT };

        private Dictionary<ulong, Transposition> transpositionTable;

        private int transpositionCapacity = 2000000;

        private AIStrategy strategy;

        public AIManager()
        { 
        }

        public Direction GetBestMove(BitBoard board)
        {
            Direction bestMove = Direction.NONE;
            double bestScore = double.MinValue;
            double currentScore;
            transpositionTable = new Dictionary<ulong, Transposition>(transpositionCapacity);
            foreach (Direction move in directions)
            {
                BitBoard cloned = (BitBoard)board.Clone();
                if (cloned.ShiftBoard(move))
                {
                    currentScore = GenerateScore(cloned, 0, adaptSearchDepth(cloned) - 1);
                    if (currentScore > bestScore)
                    {
                        bestScore = currentScore;
                        bestMove = move;
                    }
                }
            }
            return bestMove;
        }

        private double GenerateScore(BitBoard board, int currentDepth, int searchDepth)
        {
            Transposition value;
            if (transpositionTable.TryGetValue(board.BoardKey, out value) && value.Depth <= currentDepth)
            {
                return value.Score;
            }
            if (currentDepth == searchDepth || board.IsWon)
            {
                return BoardEvaluation.Evaluate(board, strategy);
            }
            double totalScore = 1;
            double score2, score4;
            board.EmptyCells--; // try spot 2/4 in every empty cell on the board
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row,col] == 0)
                    {
                        board[row, col] = 1;
                        score2 = CalculateMoveScore(board, currentDepth, searchDepth);
                        totalScore += 0.9 * score2;
                        board[row, col] = 2;
                        score4 = CalculateMoveScore(board, currentDepth, searchDepth);
                        totalScore += 0.1 * score4;
                        board[row, col] = 0;
                    }
                }
            }
            board.EmptyCells++;
            totalScore /= board.EmptyCells; // calculate the average score
            value = new Transposition(totalScore, (ushort)currentDepth);
            transpositionTable[board.BoardKey] = value;
            return totalScore;
        }

        private double CalculateMoveScore(BitBoard board, int currentDepth, int searchDepth)
        {
            double bestScore = double.MinValue;
            double currentScore;
            BitBoard clone;
            foreach (Direction move in directions)
            {
                clone = (BitBoard)board.Clone();
                if (clone.ShiftBoard(move))
                {
                    currentScore = GenerateScore(clone, currentDepth + 1, searchDepth);
                    bestScore = (currentScore > bestScore) ? currentScore : bestScore; 
                }
            }
            return bestScore;
        }

        private int adaptSearchDepth(BitBoard board)
        {
            if (board.EmptyCells > 4)
            {
                return SearchDepth;
            }
            return SearchDepth + 1;
        }
    }
}
