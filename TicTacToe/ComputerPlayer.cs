using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Statistaics for the move - position and who's the winner if we make it
    /// </summary>
    class MoveAnalysis
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CrossesOrNoughts Winner { get; set; }

        public MoveAnalysis(int row, int column, CrossesOrNoughts winner)
        {
            Row = row;
            Column = column;
            Winner = winner;
        }
    }

    /// <summary>
    /// Static class used to calculate the optimal move
    /// </summary>
    static class MoveCalculator
    {
        private static Random random = new Random();
        /// <summary>
        /// Calculate the result to which leads outting playerSymbol at gven position on a board
        /// assuming that both players play optimally. 
        /// </summary>
        /// <param name="row">Row position</param>
        /// <param name="column">Column position</param>
        /// <param name="board">The game board</param>
        /// <param name="playerSymbol">Symbol - either naughts or crosses.</param>
        /// <returns></returns>
        private static CrossesOrNoughts EvaluateMove(int row, int column, GameBoard board, CrossesOrNoughts playerSymbol)
        {
            // Sanity check - checks whether the the position is legal
            if (playerSymbol == CrossesOrNoughts.Neither || !board.IsValidPosition(row, column))
                throw new ArgumentOutOfRangeException("Player can be either Crosses or Naughts.");
            if (board[row, column] != CrossesOrNoughts.Neither)
                throw new IndexOutOfRangeException("Square already occupied.");

            /* Calculates the score recursively. 
             * We put the current player's sign on the board and check the result.
             * If the game is over we return the result of the game.
             * Otherwise, we go over all available positions and pick return the best score 
             * achivable by the opponent
             */
            board[row, column] = playerSymbol;
            (GameLogic.GameState state, CrossesOrNoughts winner) = GameLogic.DetermineResult(board);

            if (state == GameLogic.GameState.Over)
            {
                board[row, column] = CrossesOrNoughts.Neither;
                return winner;
            }

            CrossesOrNoughts bestOponentResult = playerSymbol;
            CrossesOrNoughts opponentSymbol = OpponentSymbol(playerSymbol);

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (board[i, j] == CrossesOrNoughts.Neither)
                    {
                        CrossesOrNoughts result = EvaluateMove(i, j, board, opponentSymbol);
                        if (result == opponentSymbol)
                            bestOponentResult = opponentSymbol;
                        else if (result == CrossesOrNoughts.Neither && bestOponentResult == playerSymbol)
                            bestOponentResult = CrossesOrNoughts.Neither;
                    }
                }
            }

            board[row, column] = CrossesOrNoughts.Neither;
            return bestOponentResult;
        }

        /// <summary>
        /// Calculates the best move that can be maid by the player
        /// </summary>
        /// <param name="playerSymbol">Players symbol - either crosses or noughtss</param>
        /// <param name="board">the game board</param>
        /// <returns>Best move that player can make</returns>
        public static MoveAnalysis BestMove(CrossesOrNoughts playerSymbol, GameBoard board)
        {
            // list of possible moves
            List<MoveAnalysis> moves = new List<MoveAnalysis>();

            // go over all empty positions and them as possible moves
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (board[i, j] == CrossesOrNoughts.Neither)
                    {
                        CrossesOrNoughts winner = EvaluateMove(i, j, board, playerSymbol);
                        moves.Add(new MoveAnalysis(i, j, winner));
                    }
                }
            }

            // determine the best possible move and result
            MoveAnalysis bestMove = moves[0];

            for (int i = 1; i < moves.Count; ++i)
            {
                if (moves[i].Winner == playerSymbol)
                {
                    bestMove = moves[i];
                }

                else if (moves[i].Winner == CrossesOrNoughts.Neither && bestMove.Winner == OpponentSymbol(playerSymbol))
                {
                    bestMove = moves[i];
                }
            }

            // randomize - make a list of best moves and chose the best one
            List<MoveAnalysis> bestMoves = new List<MoveAnalysis>();
            for(int i = 0; i < moves.Count; ++i)
            {
                if (moves[i].Winner == bestMove.Winner)
                    bestMoves.Add(moves[i]);
            }

            return bestMoves[random.Next(bestMoves.Count)];
        }

        /// <summary>
        /// Calculates the symbol used by opponent player
        /// </summary>
        /// <param name="playerSymbol">Returns the symbol used by the opponent</param>
        /// <returns></returns>
        private static CrossesOrNoughts OpponentSymbol(CrossesOrNoughts playerSymbol)
        {
            if (playerSymbol == CrossesOrNoughts.Crosses) return CrossesOrNoughts.Naughts;
            if (playerSymbol == CrossesOrNoughts.Naughts) return CrossesOrNoughts.Crosses;
            else return CrossesOrNoughts.Neither;
        }
    }


    /// <summary>
    /// Represents computer controlled player - designed to always pick the best move
    /// </summary>
    public class ComputerPlayer : IPlayer
    {
        private string playerName;
        private CrossesOrNoughts playerSymbol;
        private GameManager gameManager;

        public ComputerPlayer(string name, CrossesOrNoughts symbol, GameManager gameManager)
        {
            this.playerName = name;
            this.playerSymbol = symbol;
            this.gameManager = gameManager;
        }

        /// <summary>
        /// Symbol used by the player  - crosses or noughts
        /// </summary>
        /// <returns>The symbol used by the player</returns>
        public CrossesOrNoughts Symbol() => playerSymbol;
        public string Name() => playerName;

        /// <summary>
        /// Calculates the best possible move and passes it to the IO
        /// </summary>
        public void MakeMove()
        {
            MoveAnalysis move = MoveCalculator.BestMove(playerSymbol, gameManager.Board);
            gameManager.IO.ProcessDigitalInput(move.Row, move.Column);
        }

        
        
    }
}
