using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    class LookUpTable
    {
        public const int ROW_SIZE = 4;
        public static ShiftedRow[] leftShifts = new ShiftedRow[0x10000];

        static LookUpTable()
        {

        }


        public void init_tables()
        {
            byte[] line = new Byte[4];
            ushort numberOfMergeableTile;
            ushort numberOfEmptyTile = 0;
            bool isWon;
            uint score;
            for (int row = 0; row < 65536; row++)
            {
                numberOfMergeableTile = 0;
                numberOfEmptyTile = 0;
                isWon = false;
                line[0] = (byte)((row >> 0) & 0xf);
                line[1] = (byte)((row >> 4) & 0xf);
                line[2] = (byte)((row >> 8) & 0xf);
                line[3] = (byte)((row >> 12) & 0xf);
                for (int i = 0; i < ROW_SIZE; i++)
                {
                    if (line[i] == 0)
                    {
                        numberOfEmptyTile++;
                    }
                }
                score = 0;
                for (int i = 0; i < 4; ++i)
                {
                    int rank = line[i];
                    if (rank >= 2)
                    {
                        score += (uint)((rank - 1) * (1 << rank));
                    }
                }
                for (int i = 0; i < 3; ++i)
                {
                    int j;
                    for (j = i + 1; j < 4; ++j)
                    {
                        if (line[j] != 0) break;
                    }
                    if (j == 4) break; // no more tiles to the right
                    if (line[i] == 0)
                    {
                        line[i] = line[j];
                        line[j] = 0;
                        i--; // retry this entry
                    }
                    else if (line[i] == line[j])
                    {
                        if (line[i] != 0xf)
                        {
                            //marge two tiles
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
                ushort result = (ushort)(line[0] << 0);
                result |= (ushort)(line[1] << 4);
                result |= (ushort)(line[2] << 8);
                result |= (ushort)(line[3] << 12);
                leftShifts[row] = new ShiftedRow(numberOfEmptyTile, (ushort)(row ^ result), numberOfMergeableTile,
                score, isWon);
            }
        }
    }
}
