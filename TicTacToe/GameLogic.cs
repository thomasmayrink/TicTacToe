using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    ///  A class respsonsible to the logic of the game. Used to determine winner and control the turns.
    /// </summary>
    public class GameLogic
    {
        /// <summary>
        /// The state of the game - whether it is over or not.
        /// </summary>
        public enum GameState { Continue, Over};
        private GameManager gameManager;
        GameBoard board => gameManager.Board;
        public IPlayer player1 { get; private set; }
        public IPlayer player2 { get; private set; }
        public IPlayer CurrentPlayer { get;  private set; }
        public CrossesOrNoughts Winner { get; private set; }
        public GameState State { get; private set; }
        
        /// <summary>
        /// Creates a new game logic object and associates it with the gameManager object
        /// </summary>
        /// <param name="gameManager">Game manager object to associate with</param>
        public GameLogic(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.State = GameState.Continue;
            player1 = null;
            player2 = null;
            CurrentPlayer = player1;
        }

        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void AddPlayers(IPlayer player1, IPlayer player2)
        {
            if (this.player1 == null) this.player1 = player1;
            if (this.player2 == null) this.player2 = player2;
            CurrentPlayer = player1;
        }

        /// <summary>
        /// Determines result of the game state determined by internal board object
        /// </summary>
        /// <returns>Whehter the game is over and the winner symbol</returns>
        private (GameState, CrossesOrNoughts) DetermineResult() => DetermineResult(this.board);

        /// <summary>
        /// Calculates the state and the result of the game at the moment represented by the board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>Wheher the game is over and who is the winner in case it i. I
        /// f it is not over - Niether is retunred.</returns>
        public static (GameState state, CrossesOrNoughts winner) DetermineResult(GameBoard board)
        {
            // go over rows colums and diagonals to che k whether the game is over and we have a winner.
            // After that - check if the board is full
            for(int i = 0; i < 3; ++i)
            {
                if (board.IsRowTaken(i))
                    return (GameState.Over, board[0, i]);
                if (board.IsColumnTaken(i))
                    return (GameState.Over, board[i, 0]);
            }

            if (board.IsMainDiagonalTaken())
                return (GameState.Over, board[0, 0]);
            if (board.IsSecondaryDiagonalTaken())
                return (GameState.Over, board[2, 0]);
            if (board.IsFull())
                return (GameState.Over, CrossesOrNoughts.Neither);

            return (GameState.Continue, CrossesOrNoughts.Neither);
        }

        /// <summary>
        /// Change the player
        /// </summary>
        void UpdatePlayer()
        {
            CurrentPlayer = (CurrentPlayer == player1) ? player2 : player1;
        }

        /// <summary>
        /// Checks whether position is legal or if it is taken and puts appropriate player sign on it if the game is not over.
        /// After performing player move, updates the player if it the game is not over.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void Update(int row, int column)
        {
            if (board.ShouldUpdate(row, column) && State == GameState.Continue)
            {
                board[row, column] = CurrentPlayer.Symbol;
                (State, Winner) = DetermineResult();

                if (State == GameState.Continue) UpdatePlayer();
                else
                {
                    player1.UpdateScore(Winner);
                    player2.UpdateScore(Winner);
                }
            }
        }

        /// <summary>
        /// Calculates the symbol used by opponent player
        /// </summary>
        /// <param name="playerSymbol">Returns the symbol used by the opponent</param>
        /// <returns></returns>
        public static CrossesOrNoughts OpponentSymbol(CrossesOrNoughts playerSymbol)
        {
            if (playerSymbol == CrossesOrNoughts.Crosses) return CrossesOrNoughts.Naughts;
            if (playerSymbol == CrossesOrNoughts.Naughts) return CrossesOrNoughts.Crosses;
            else return CrossesOrNoughts.Neither;
        }
    }
}
