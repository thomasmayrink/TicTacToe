using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    /// <summary>
    /// A class to represent human controlled player in the game
    /// </summary>
    public class HumanPlayer : IPlayer
    {
        public string Name { get; private set; }
        private CrossesOrNoughts symbol;
        public CrossesOrNoughts Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                if (value == CrossesOrNoughts.Neither)
                    throw new ArgumentOutOfRangeException("Player must be either Nauths or crossees");
                symbol = value;

            }
        }

        private GameManager gameManager;
        
        public int Score { get; private set; }
        private ScoreCalculator scoreCalculator;

        /// <summary>
        /// Creats an instance of player
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="symbol">What player puts on the board - crosses or nauughts</param>
        /// <param name="gameManager">Interface to the game</param>
        public HumanPlayer(String name, CrossesOrNoughts symbol, GameManager gameManager, ScoreCalculator scoreCalculator) 
        {
            this.Name = name;
            this.symbol = symbol;
            this.gameManager = gameManager;
            this.Score = 0;
            this.scoreCalculator = scoreCalculator;
        }

        /// <summary>
        /// make a Move in the game
        /// </summary>
        public void MakeMove() => gameManager.IO.ProcessMouseInput();



        

        public void SetSymbol(CrossesOrNoughts symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Update the player's score
        /// </summary>
        /// <param name="winner">Current winner of the game</param>
        public void UpdateScore(CrossesOrNoughts winner)
        {
            if (winner == symbol) Score += scoreCalculator.WinScore;
            if (winner == GameLogic.OpponentSymbol(symbol)) Score += scoreCalculator.LoseScore;
            else Score += scoreCalculator.DrawScore;
        }

    }
}
