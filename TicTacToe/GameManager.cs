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
        private IPlayer player1;
        private IPlayer player2;

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
            player1 = new HumanPlayer("Player", CrossesOrNoughts.Crosses, this, new ScoreCalculator(10, 1, -10));
            player2 = new ComputerPlayer("Computer", CrossesOrNoughts.Naughts, this, new ScoreCalculator(10, 0, -10));
            Logic.AddPlayers(player1, player2);
        }

        public void Reset()
        {
            Board = new GameBoard();
            Logic = new GameLogic(this);

            CrossesOrNoughts tempSymbol = player1.Symbol;
            player1.Symbol = player2.Symbol;
            player2.Symbol = tempSymbol;

            IPlayer tempPlayer = player1;
            player1 = player2;
            player2 = tempPlayer;

            Logic.AddPlayers(player1, player2);
        }

        /// <summary>
        /// Update the game state
        /// </summary>
        public void Update()
        {
            IO.Update();
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
