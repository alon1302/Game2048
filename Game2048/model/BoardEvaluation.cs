namespace Game2048.model
{
    class BoardEvaluation
    {
        public const double EMPTY_CELL_SCORE = 128.0f;
        public const double MERGE_CELL_SCORE = 256.0f;
        public const double GAME_LOSE_SCORE = -5000000.0f;
        public const double GAME_WON_SCORE = 5000000.0f;

        private LookupTable lookupTable = LookupTable.Instance;

        public static int[,] snakeStrategy =
        {
            {1024, 512, 256, 128},
            {8, 16, 32, 64},
            {4, 2, 2, 0},
            {0, 0, 0, 0}
        };

        public static int[,] cornerStrategy =
        {
            {4096, 1024, 256, 64},
            {1024, 256, 64, 16},
            {256, 64, 16, 4},
            {64, 16, 4, 1}
        };

        private static double EvaluateEmptyCells(BitBoard board)
        {
            return EMPTY_CELL_SCORE * board.EmptyCells;
        }

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

        private static double EvaluateMergeableCells(BitBoard board)
        {
            return MERGE_CELL_SCORE * board.MergeableCells;
        }

        private static double EvaluateWon(BitBoard board)
        {
            return board.IsWon ? GAME_WON_SCORE : 0;
        }

        public static double Evaluate(BitBoard board, AIStrategy strategy)
        {
            return EvaluatePositionByStrategy(board, strategy)
             + EvaluateEmptyCells(board)
             + EvaluateMergeableCells(board)
             + EvaluateWon(board);
        }
    }
}
