using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// A class to help to determine the score obtained by the player as a result of the game
    /// </summary>
    public class ScoreCalculator
    {
        public int WinScore { get; private set; }
        public int DrawScore { get; private set; }
        public int LoseScore { get; private set; }
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="win">Score for the win</param>
        /// <param name="draw">Score for the draw</param>
        /// <param name="lose">Score for the loss</param>
        public ScoreCalculator(int win, int draw, int lose)
        {
            WinScore = win;
            DrawScore = draw;
            LoseScore = lose;
        }
    }
}
