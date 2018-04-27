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
    /// A class for displaying messages in the game.
    /// </summary>
    public class GameMessage
    {
        private GameManager gameManager;
        private SpriteFont spriteFont;
        private Color defaultColor;

        /// <summary>
        /// Consructor - initializes default color to white and associates the Message with a gameManager
        /// </summary>
        /// <param name="gameManager">The game manager to associate with</param>
        public GameMessage(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.spriteFont = gameManager.TheGame.Content.Load<SpriteFont>("Arial");
            defaultColor = Color.White;
        }

        public void PrintMessageAt(Vector2 position, String message, Color drawColor)
        {
            gameManager.TheGame.SpriteBatch.DrawString(spriteFont, message, position, drawColor);
        }

        public void PrintMessageAt(Vector2 position, String message) => PrintMessageAt(position, message, defaultColor);
    }
}
