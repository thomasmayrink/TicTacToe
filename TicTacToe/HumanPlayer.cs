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
    }
}
