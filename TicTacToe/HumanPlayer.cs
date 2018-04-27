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
    class HumanPlayer : IPlayer
    {
        private string name;
        private CrossesOrNoughts symbol;
        private GameManager gameManager;
        private int score;

        /// <summary>
        /// Creats an instance of player
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="symbol">What player puts on the board - crosses or nauughts</param>
        /// <param name="gameManager">Interface to the game</param>
        public HumanPlayer(String name, CrossesOrNoughts symbol, GameManager gameManager) 
        {
            this.name = name;
            this.symbol = symbol;
            this.gameManager = gameManager;
            this.score = 0;
        }

        /// <summary>
        /// make a Move in the game
        /// </summary>
        public void MakeMove() => gameManager.IO.ProcessMouseInput();

        /// <summary>
        /// The symbol used by player
        /// </summary>
        /// <returns>Returns whether the player puts crosses or naughts</returns>
        public CrossesOrNoughts Symbol() => symbol;

        /// <summary>
        ///  Player's name
        /// </summary>
        /// <returns>Player's name</returns>
        public string Name() => name;

        /// <summary>
        /// Score of the player
        /// </summary>
        /// <returns>The score of the player</returns>
        public int Score() => score;

        public void SetSymbol(CrossesOrNoughts symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Update the player's score
        /// </summary>
        /// <param name="differense">The differense between the new and the old score</param>
        public void UpdateScore(int differense)
        {
            this.score += differense;
        }

    }
}
