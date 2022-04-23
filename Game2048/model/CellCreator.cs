using System;

namespace Game2048.model
{
    class CellCreator
    {
        private static Random rnd = new Random(); // randomizer

        /// <summary>
        /// function that returns the value 2 or 4 by the randomly by the probability of the game 
        /// for a new cell in the board
        /// </summary>
        /// <returns>the value 2 or 4</returns>
        public static int getValue()
        {
            int randomPercentage = rnd.Next(1, 101);
            return randomPercentage > 10 ? 1 : 2; 
        }
    }
}
