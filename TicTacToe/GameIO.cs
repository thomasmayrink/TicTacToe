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
    /// The class repsonsible for the graphics part of the game
    /// </summary>
    public class GameIO
    {
        GameManager gameManager;
        TicTacToeGame TheGame => gameManager.TheGame;
        GameLogic Logic => gameManager.Logic;
        GameBoard Board => gameManager.Board;
        Vector2 TopLeft => WindowSize * 0.05f;    // top left position of the board
        Vector2 SquareSize => WindowSize / 5;
        Vector2 BoardSize => SquareSize * 3f;
        Vector2 WindowSize => new Vector2(TheGame.GraphicsDevice.Viewport.Width, TheGame.GraphicsDevice.Viewport.Height);
        Texture2D background;                                // background texture
        Texture2D tableBorders;                              // borders between the squares
        Texture2D xImage;                                    // Crosses image
        Texture2D oImage;                                    // Naughts imaage
        Texture2D horizontalLine;                            // Horizontal line image
        Texture2D verticalLine;                              // vertical line image    
        Texture2D westEastDiagonal;                          // an image of diagonal from topleft to buttom right
        Texture2D eastWestDiagonal;                          // an image of diagonal from topright to butttom left
        GameMessage gameMessage;

        private GameIO() { }

        public GameIO(GameManager gameManager)
        {
            this.gameManager = gameManager;
            background = TheGame.Content.Load<Texture2D>("Background");
            tableBorders = TheGame.Content.Load<Texture2D>("TableBorders");
            xImage = TheGame.Content.Load<Texture2D>("X");
            oImage = TheGame.Content.Load<Texture2D>("O");
            horizontalLine = TheGame.Content.Load<Texture2D>("HorizontalLine");
            verticalLine = TheGame.Content.Load<Texture2D>("VerticalLine");
            westEastDiagonal = TheGame.Content.Load<Texture2D>("WestEastDiagonal");
            eastWestDiagonal = TheGame.Content.Load<Texture2D>("EastWestDiagonal");
            gameMessage = new GameMessage(gameManager);
        }


        /// <summary>
        /// Draws a square image on the screen
        /// </summary>
        /// <param name="image">Texture name</param>
        /// <param name="topPosition">Upper border of the image position</param>
        /// <param name="leftPosition">Left border of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="width">Widht of the image</param>
        void DrawSquare(Texture2D image, float topPosition, float leftPosition, float width, float height)
        {
            Rectangle destination = new Rectangle((int)topPosition, (int)leftPosition, (int)width, (int)height);
            TheGame.SpriteBatch.Draw(image, destination, Color.White);
        }

        /// <summary>
        /// Draws the back ground of the table
        /// </summary>
        void DrawBackground()
        {
            DrawSquare(background, TopLeft.X, TopLeft.Y, BoardSize.X, BoardSize.Y);
            DrawSquare(tableBorders, TopLeft.X, TopLeft.Y, BoardSize.X, BoardSize.Y);
        }

        /// <summary>
        /// Fills the squares in the game table
        /// </summary>
        void DrawSquares()
        {
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    Texture2D filling;
                    if (Board[i, j] == CrossesOrNoughts.Crosses)
                        filling = TheGame.Content.Load<Texture2D>("X");
                    else if (Board[i, j] == CrossesOrNoughts.Naughts)
                        filling = TheGame.Content.Load<Texture2D>("O");
                    else filling = null;

                    if (filling != null)
                        DrawSquare(filling, TopLeft.X + i * SquareSize.X, TopLeft.Y + j * SquareSize.Y,
                            SquareSize.X, SquareSize.Y);
                }
        }

        /// <summary>
        /// Marks with a line the rows that are all either noughts or crosses
        /// </summary>
        void MarkRows()
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Board.IsRowTaken(i))
                {
                    DrawSquare(horizontalLine, TopLeft.X, TopLeft.Y + SquareSize.Y * i, BoardSize.X, SquareSize.Y);
                }
            }
        }

        /// <summary>
        /// Marks the collumns that are all either noughts or crosses
        /// </summary>
        void MarkColumns()
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Board.IsColumnTaken(i))
                {
                    DrawSquare(verticalLine, TopLeft.X + SquareSize.X * i, TopLeft.Y, SquareSize.X, BoardSize.Y);
                }
            }
        }

        /// <summary>
        /// Marks the main if it contains all noughts or crosses
        /// </summary>
        void MarkDiagonals()
        {
            if (Board.IsMainDiagonalTaken())
                DrawSquare(westEastDiagonal, TopLeft.X, TopLeft.Y, BoardSize.X, BoardSize.Y);
            if (Board.IsSecondaryDiagonalTaken())
                DrawSquare(eastWestDiagonal, TopLeft.X, TopLeft.Y, BoardSize.X, BoardSize.Y);
        }


        /// <summary>
        /// Draws the game board
        /// </summary>
        public void Draw()
        {
            DrawBackground();
            DrawSquares();
            MarkRows();
            MarkColumns();
            MarkDiagonals();
            PrintScores();

            if (Logic.State == GameLogic.GameState.Over)
            {
                DeclareWinner();
                RestartMessage();
            }
        }

        /// <summary>
        /// Translates 2 dimensional vector to position on the board
        /// </summary>
        /// <param name="clickPosition"></param>
        /// <returns></returns>
        public (int row, int column) PositionOnBoard(Vector2 clickPosition)
        {
            return ((int)((clickPosition.X - TopLeft.X) / SquareSize.X),
                    (int)((clickPosition.Y - TopLeft.Y) / SquareSize.Y));
        }

        /// <summary>
        /// Processes mouse input from the user
        /// </summary>
        public void ProcessMouseInput()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                (int row, int column) = PositionOnBoard(new Vector2(mouseState.X, mouseState.Y));
                Logic.Update(row, column);
            }
        }

        /// <summary>
        /// Processes move that was entered as a pair of numbers
        /// </summary>
        /// <param name="row">Row number</param>
        /// <param name="column">Column number</param>
        public void ProcessDigitalInput(int row, int column)
        {
            Logic.Update(row, column);
        }

        /// <summary>
        /// Get input from player and update the state of the game
        /// </summary>
        public void Update()
        {
            if (Logic.State == GameLogic.GameState.Continue) Logic.CurrentPlayer.MakeMove();
            if (Logic.State == GameLogic.GameState.Over)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    gameManager.Reset();
            }
        }

        /// <summary>
        /// Print player scores
        /// </summary>
        private void PrintScores()
        {
            gameMessage.PrintMessageAt(new Vector2(TopLeft.X, TopLeft.Y + BoardSize.Y + 20),
                                       $"{Logic.player1.Name}: {Logic.player1.Score}");
            gameMessage.PrintMessageAt(new Vector2(TopLeft.X, TopLeft.Y + BoardSize.Y + 70),
                                       $"{Logic.player2.Name}: {Logic.player2.Score}");
        }

        private void DeclareWinner()
        {
            string winnerName = (Logic.Winner == CrossesOrNoughts.Neither) ? "Draw" : Logic.CurrentPlayer.Name;
            gameMessage.PrintMessageAt(new Vector2(TopLeft.X, TopLeft.Y + BoardSize.Y + 120),
                                       $"The winner is {winnerName}");
                                      
        }

        private void RestartMessage()
        {
            gameMessage.PrintMessageAt(new Vector2(TopLeft.X, TopLeft.Y + BoardSize.Y + 170),
                                       "Press space to continue");
        }

    }
}
