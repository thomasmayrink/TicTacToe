using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class ScoreCalculator
    {
        public int WinScore { get; private set; }
        public int DrawScore { get; private set; }
        public int LoseScore { get; private set; }

        public ScoreCalculator(int win, int draw, int lose)
        {
            WinScore = win;
            DrawScore = draw;
            LoseScore = lose;
        }
    }
}
