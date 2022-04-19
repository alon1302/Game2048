using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    /// <summary>
    /// singleton class that represent a lookup table
    /// array that the indicies are all the rows in the game and the values are object 
    /// that represent the row after shift left and some other info about it
    /// </summary>
    class LookupTable
    {
        private static readonly object threadLock = new object(); // object that make the singleton threadable
        private static LookupTable instance = null; // the single instance of this class
        public ShiftedRow[] leftShifts = new ShiftedRow[0x10000]; // array of 65563 possible rows in the game

        /// <summary>
        /// constructor that creates the singe instance of the lookup table 
        /// </summary>
        private LookupTable()
        {
            InitTable();
        }

        /// <summary>
        /// property that returns the single instance of this class
        /// creates it or returns it if exist
        /// </summary>
        public static LookupTable Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (instance == null)
                    {
                        instance = new LookupTable();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// indexer that returns the value of the array of rows by the index
        /// </summary>
        /// <param name="i">the index in te array</param>
        /// <returns>the shifted row value</returns>
        public ShiftedRow this[int i]
        {
            get { return leftShifts[i]; }
        }

        /// <summary>
        /// function that receives a byte array that represent one row/col
        /// the function returns the number of the empty cells in this line
        /// </summary>
        /// <param name="line">byte array that represent one row/col</param>
        /// <returns>number of the empty cells</returns>
        private ushort CalculateEmptyCells(byte[] line)
        {
            ushort emptyCells = 0;
            for (int i = 0; i < BitBoard.ROW_SIZE; i++)
            {
                if (line[i] == 0)
                {
                    emptyCells++;
                }
            }
            return emptyCells;
        }

        /// <summary>
        /// function that receives a byte array that represent one row/col
        /// the function returns the score of this line
        /// </summary>
        /// <param name="line">byte array that represent one row/col</param>
        /// <returns>number of the empty cells</returns>
        private uint CalculateScore(byte[] line)
        {
            uint score = 0;
            for (int i = 0; i < 4; ++i)
            {
                int currValue = line[i];
                if (currValue >= 2)
                {
                    score += (uint)((currValue - 1) * (1 << currValue));
                }
            }
            return score;
        }

        /// <summary>
        /// function that receives a byte array that represent one row/col
        /// sets this line into type of ushort using bitwise operation
        /// </summary>
        /// <param name="newLine">byte array that represent one row/col</param>
        /// <returns>ushort that represent the line</returns>
        private ushort ArrangeNewLine(byte[] newLine)
        {
            return (ushort)((newLine[0] << 0) |
            (newLine[1] << 4) |
            (newLine[2] << 8) |
            (newLine[3] << 12));
        }

        /// <summary>
        /// function that receives a an integer that represent one row/col
        /// sets this int into a byte array and returns it
        /// </summary>
        /// <param name="originalLine">integer that represent one line</param>
        /// <returns>byte array that represent one line</returns>
        private byte[] ArrangeOriginalLine(int originalLine)
        {
            byte[] line = new byte[4];
            line[0] = (byte)((originalLine >> 0) & 0xf);
            line[1] = (byte)((originalLine >> 4) & 0xf);
            line[2] = (byte)((originalLine >> 8) & 0xf);
            line[3] = (byte)((originalLine >> 12) & 0xf);
            return line;
        }

        /// <summary>
        /// function that initialize the lookup table
        /// iterate over all the possible rows in the board and make them shift left using bitwise operations
        /// save the result in the array at the index of the original with some more info about the original row
        /// </summary>
        private void InitTable()
        {
            byte[] line;
            ushort numberOfMergeableTile;
            ushort emptyCells;
            bool isWon;
            uint score;
            for (int row = 0; row < leftShifts.Length; ++row)
            {
                numberOfMergeableTile = 0;                
                isWon = false;
                line = ArrangeOriginalLine(row);
                emptyCells = CalculateEmptyCells(line);
                score = CalculateScore(line);
                for (int i = 0; i < 3; ++i)
                {
                    int j;
                    for (j = i + 1; j < 4; ++j)
                        if (line[j] != 0) break;
                    if (j == 4) break; // no more tiles to the right
                    if (line[i] == 0)
                    {
                        line[i] = line[j];
                        line[j] = 0;
                        i--; // retry this entry
                    }
                    else if (line[i] == line[j])
                    {
                        if (line[i] != 0xf) //marge two tiles
                        {
                            numberOfMergeableTile++;
                            line[i]++;
                        }
                        else
                        {
                            isWon = true;
                        }
                        line[j] = 0;
                    }
                }
                ushort result = ArrangeNewLine(line);
                leftShifts[row] = new ShiftedRow(emptyCells, (ushort)(row ^ result), numberOfMergeableTile, score, isWon); // save line after shift left
            }
        }
    }
}
