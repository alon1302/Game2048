using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    class NewCellGenerator
    {
        private static Random rnd = new Random();

        public static int getValue()
        {
            int randomPercentage = rnd.Next(1, 101);
            return randomPercentage > 10 ? 1 : 2; 
        }
    }
}
