using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    public class Transposition
    {
        private double score;
        private ushort depth;

        public Transposition(double score, ushort depth)
        {
            this.score = score;
            this.depth = depth;        
        }
        public double Score { get => score; set => score = value; }
        public ushort Depth { get => depth; set => depth = value; }
    }
}
