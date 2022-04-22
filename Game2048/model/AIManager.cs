using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    /// <summary>
    /// enum that represent the chosen strategy for the AI by the user
    /// </summary>
    public enum AIStrategy
    {
        SNAKE, // snake organize strategy
        CORNER // corner organize strategy
    }
    class AIManager
    {
        private const int TRANSPOSITION_CAPACITY = 2000000; // the capacity of the transposition table
        private int _depth = 4; // search depth
        private Direction[] _directions = { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT }; // array of the possible moves
        private Dictionary<ulong, Transposition> _transpositionTable; // dictionary that represent the transposition table     
        private AIStrategy _strategy; // the chosen strategy

        /// <summary>
        /// constractor that creat instance of this class
        /// with the chosen search depth and strategy
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="strategy"></param>
        public AIManager(int depth, AIStrategy strategy)
        {
            _depth = depth;
            _strategy = strategy;
        }

        /// <summary>
        /// function that receives a board and calculates the best move to make by the AI
        /// the function iterates over all the possible moves and make them on cloned boards
        /// and for each move calls to function that return the score of the board after thar move
        /// at the end the function returns the best move to make
        /// </summary>
        /// <param name="board">the current game board</param>
        /// <returns>the bost move to make on the given board</returns>
        public Direction GetBestMove(BitBoard board)
        {
            Direction bestMove = Direction.NONE;
            double bestScore = double.MinValue;
            double currentScore;
            _transpositionTable = new Dictionary<ulong, Transposition>(TRANSPOSITION_CAPACITY);
            foreach (Direction move in _directions)
            {
                BitBoard cloned = (BitBoard)board.Clone();
                if (cloned.ShiftBoard(move))
                {
                    currentScore = CreateAvgExpectation(cloned, 0, GetDepthByBoard(cloned) - 1);
                    if (currentScore > bestScore)
                    {
                        bestScore = currentScore;
                        bestMove = move;
                    }
                }
            }
            return bestMove;
        }

        /// <summary>
        /// the function receives a board and integers that represent the current search depth and the wanted search depth
        /// the function calculates the score of the current board by placing 2/4 in each empty cell
        /// and calls a function that calculates the scores of the next possible moves after the last one
        /// </summary>
        /// <param name="board"> the current board</param>
        /// <param name="currentDepth">the current search depth</param>
        /// <param name="searchDepth">the maximum search depth</param>
        /// <returns>the score of the current board base on the next moves</returns>
        private double CreateAvgExpectation(BitBoard board, int currentDepth, int searchDepth)
        {
            Transposition value;
            if (_transpositionTable.TryGetValue(board.BoardKey, out value) && value.Depth <= currentDepth)
                return value.Score;
            if (currentDepth == searchDepth || board.IsWon)
                return BoardEvaluation.Evaluate(board, _strategy);
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
            _transpositionTable[board.BoardKey] = value;
            return totalScore;
        }

        /// <summary>
        /// the function receives the current board and calculates the best score of a move to make
        /// by the current search depth and the wanted one
        /// </summary>
        /// <param name="board">the current board</param>
        /// <param name="currentDepth">the current search depth</param>
        /// <param name="searchDepth">the maximum search depth</param>
        /// <returns>the best score of a move</returns>
        private double CalculateMoveScore(BitBoard board, int currentDepth, int searchDepth)
        {
            double bestScore = double.MinValue;
            double currentScore;
            BitBoard clone;
            foreach (Direction move in _directions)
            {
                clone = (BitBoard)board.Clone();
                if (clone.ShiftBoard(move))
                {
                    currentScore = CreateAvgExpectation(clone, currentDepth + 1, searchDepth);
                    bestScore = (currentScore > bestScore) ? currentScore : bestScore; 
                }
            }
            return bestScore;
        }

        /// <summary>
        /// the function receives a board and returns the maximum depth to search on this board 
        /// by its empty cells
        /// </summary>
        /// <param name="board">the current board</param>
        /// <returns>the maximum search depth for this board</returns>
        private int GetDepthByBoard(BitBoard board)
        {
            if (board.EmptyCells > 4)
                return _depth;
            return _depth + 1;
        }
    }
}
