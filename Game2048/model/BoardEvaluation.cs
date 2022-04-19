namespace Game2048.model
{
    class BoardEvaluation
    {
        public const double EMPTY_CELL_SCORE = 128.0f; // score of an empty cell
        public const double MERGE_CELL_SCORE = 256.0f; // score of a mergeable cell
        public const double GAME_LOSE_SCORE = -5000000.0f; // score of a lose
        public const double GAME_WON_SCORE = 5000000.0f; // score of a win

        private LookupTable lookupTable = LookupTable.Instance; // reference to the global lookup table

        public static int[,] snakeStrategy =
        {
            {1024, 512, 256, 128},
            {8, 16, 32, 64},
            {4, 2, 2, 0},
            {0, 0, 0, 0}
        }; // weight matrix to the snake strategy

        public static int[,] cornerStrategy =
        {
            {4096, 1024, 256, 64},
            {1024, 256, 64, 16},
            {256, 64, 16, 4},
            {64, 16, 4, 1}
        }; // weight matrix to the corner strategy

        /// <summary>
        /// function that returns the score of the empty cells in the board
        /// </summary>
        /// <param name="board">the current board</param>
        /// <returns>the score of the empty cells</returns>
        private static double EvaluateEmptyCells(BitBoard board)
        {
            return EMPTY_CELL_SCORE * board.EmptyCells;
        }

        /// <summary>
        /// function that returns the score of the board according to the chosen strategy 
        /// </summary>
        /// <param name="board">the current board</param>
        /// <param name="strategy">the chosen strategy</param>
        /// <returns>the score of the board according to the chosen strategy</returns>
        private static double EvaluatePositionByStrategy(BitBoard board, AIStrategy strategy)
        {
            int[,] matrix = GetMatrixByStrategy(strategy);
            double score = 0;
            for (int row = 0; row < BitBoard.ROW_SIZE; row++)
            {
                for (int col = 0; col < BitBoard.ROW_SIZE; col++)
                {
                    score += matrix[row, col] * (1 << board[row, col]);
                }
            }
            return score;
        }

        /// <summary>
        /// function that returns the score of the board according to the mergeable cell
        /// </summary>
        /// <param name="board">the current board</param>
        /// <returns>the score of the board according to the mergeable cell</returns>
        private static double EvaluateMergeableCells(BitBoard board)
        {
            return MERGE_CELL_SCORE * board.MergeableCells;
        }

        /// <summary>
        /// function that returns the score of won board or 0
        /// </summary>
        /// <param name="board">the current board</param>
        /// <returns>the score of won board or 0</returns>
        private static double EvaluateWon(BitBoard board)
        {
            return board.IsWon ? GAME_WON_SCORE : 0;
        }

        /// <summary>
        /// function that receives a board and the chosen strategy 
        /// evaluates the score of the current board and returns it
        /// </summary>
        /// <param name="board">the current board</param>
        /// <param name="strategy">the chosen strategy</param>
        /// <returns>the evaluation of the current board</returns>
        public static double Evaluate(BitBoard board, AIStrategy strategy)
        {
            return EvaluatePositionByStrategy(board, strategy)
             + EvaluateEmptyCells(board)
             + EvaluateMergeableCells(board)
             + EvaluateWon(board);
        }

        /// <summary>
        /// function that returns the weight matrix by the wanted strategy 
        /// </summary>
        /// <param name="strategy">the wanted strategy</param>
        /// <returns>the weight matrix</returns>
        private static int[,] GetMatrixByStrategy(AIStrategy strategy)
        {
            switch (strategy)
            {
                case AIStrategy.SNAKE:
                    return snakeStrategy;
                case AIStrategy.CORNER:
                    return cornerStrategy;
            }
            return null;
        }
    }
}
