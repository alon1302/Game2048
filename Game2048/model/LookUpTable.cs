using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    class LookupTable
    {
        public const int ROW_SIZE = 4;

        private static readonly object threadLock = new object();

        private static LookupTable instance = null;
        public ShiftedRow[] leftShifts = new ShiftedRow[0x10000];

        private LookupTable()
        {
            InitTable();
        }

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

        public ShiftedRow this[int i]
        {
            get { return leftShifts[i]; }
        }

        private ushort CalculateEmptyCells(byte[] line)
        {
            ushort emptyCells = 0;
            for (int i = 0; i < ROW_SIZE; i++)
            {
                if (line[i] == 0)
                {
                    emptyCells++;
                }
            }
            return emptyCells;
        }

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

        private ushort ArrangeNewLine(byte[] newLine)
        {
            return (ushort)((newLine[0] << 0) |
            (newLine[1] << 4) |
            (newLine[2] << 8) |
            (newLine[3] << 12));
        }

        private byte[] ArrangeOriginalLine(int originalLine)
        {
            byte[] line = new byte[4];
            line[0] = (byte)((originalLine >> 0) & 0xf);
            line[1] = (byte)((originalLine >> 4) & 0xf);
            line[2] = (byte)((originalLine >> 8) & 0xf);
            line[3] = (byte)((originalLine >> 12) & 0xf);
            return line;
        }

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
