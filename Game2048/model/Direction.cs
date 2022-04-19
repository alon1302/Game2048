using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    /// <summary>
    /// enum that represent the dorection of a move in the game
    /// </summary>
    public enum Direction
    {
        NONE, // no move
        UP, // move up
        RIGHT, // move right
        DOWN, // move down
        LEFT // move left
    }
}
