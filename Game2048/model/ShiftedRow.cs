﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    class ShiftedRow
    {
        private ushort _shifted;
        private int _emptyCells;
        private int _merges;
        private uint _score;
        private bool _isWin;

        public ShiftedRow(int emptyCells, ushort shifted, int merges, uint score, bool isWin)
        {
            _emptyCells = emptyCells;
            _shifted = shifted;
            _merges = merges;
            _score = score;
            _isWin = isWin;
        }

        public ushort Shifted
        {
            get { return _shifted; }
            set { _shifted = value; }
        }

        public int EmptyCells
        {
            get { return _emptyCells; }
            set { _emptyCells = value; }
        }

        public int Merges
        {
            get { return _merges; }
            set { _merges = value; }
        }

        public uint Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public bool IsWin
        {
            get { return _isWin; }
            set { _isWin = value; }
        }
    }
}
