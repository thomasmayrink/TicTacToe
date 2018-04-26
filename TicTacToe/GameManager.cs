using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// An object repsonsible for controlling the game
    /// </summary>
    public class GameManager
    {
        public TicTacToeGame TheGame { get; private set; }
        public GameLogic Logic { get; private set; }
        public GameIO IO { get; private set; }
        public GameBoard Board { get; private set; }

        /// <summary>
        /// Allocates memory for object - used to avoid null reference errors
        /// while initializing the fields of the the object
        /// </summary>
        private GameManager()
        {
            TheGame = null;
            Logic = null;
            IO = null;
            Board = null;
        }

        /// <summary>
        /// The game to which the given Manager belongs to.
        /// </summary>
        /// <param name="ticTacToeGame"></param>
        public GameManager(TicTacToeGame ticTacToeGame) : this()
        {
            this.TheGame = ticTacToeGame;
            Board = new GameBoard();
            Logic = new GameLogic(this);
            IO = new GameIO(this);
            Logic.AddPlayers(new HumanPlayer("Player1", CrossesOrNoughts.Crosses, this),
                             new ComputerPlayer("Player2", CrossesOrNoughts.Naughts, this));
        }

        /// <summary>
        /// Update the game state
        /// </summary>
        public void Update()
        {
            if(Logic.State == GameLogic.GameState.Continue) IO.Update();
        }

        /// <summary>
        /// Display the board on the screen.
        /// </summary>
        public void Draw()
        {
            IO.Draw();
        }

    }
}
