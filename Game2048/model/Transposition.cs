using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    /// <summary>
    /// class that represent one value of the transposition table for the AI efficiency improvement
    /// </summary>
    public class Transposition
    {
        private double score; // the evaluation of the board
        private ushort depth; // the depth it is found on

        /// <summary>
        /// constructor that creates new instance of this class
        /// and sets its fields to the parameters that received
        /// </summary>
        /// <param name="score">evaluation of the board</param>
        /// <param name="depth">depth it is found on</param>
        public Transposition(double score, ushort depth)
        {
            this.score = score;
            this.depth = depth;        
        }

        /// <summary>
        /// property that returns instance to the score of the board 
        /// to get or set its value
        /// </summary>
        public double Score { get => score; set => score = value; }

        /// <summary>
        /// property that returns instance to the depth 
        /// to get or set its value
        /// </summary>
        public ushort Depth { get => depth; set => depth = value; }
    }
}
