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
        TicTacToeGame game => gameManager.TheGame;
        GameLogic gameLogic => gameManager.Logic;
        GameBoard board => gameManager.Board;
        Vector2 topLeft;                                     // top left position of the board
        float SquareHeight => TableHeight / 3;               // height of the the square on the board
        float SquareWidth => TableWidth / 3;                 // wdith of the suare on the board
        float TableHeight;                                   // heigiht of the table
        float TableWidth;                                    // width of the table
        Texture2D background;                                // background texture
        Texture2D tableBorders;                              // borders between the squares
        Texture2D xImage;                                    // Crosses image
        Texture2D oImage;                                    // Naughts imaage
        Texture2D horizontalLine;                            // Horizontal line image
        Texture2D verticalLine;                              // vertical line image    
        Texture2D westEastDiagonal;                          // an image of diagonal from topleft to buttom right
        Texture2D eastWestDiagonal;                          // an image of diagonal from topright to butttom left

        public GameIO(GameManager gameManager, float topLeftX = 0f, float topLeftY = 0f, float height = 640f, float width = 640f)
        {
            this.gameManager = gameManager;
            background = game.Content.Load<Texture2D>("Background");
            tableBorders = game.Content.Load<Texture2D>("TableBorders");
            xImage = game.Content.Load<Texture2D>("X");
            oImage = game.Content.Load<Texture2D>("O");
            horizontalLine = game.Content.Load<Texture2D>("HorizontalLine");
            verticalLine = game.Content.Load<Texture2D>("VerticalLine");
            westEastDiagonal = game.Content.Load<Texture2D>("WestEastDiagonal");
            eastWestDiagonal = game.Content.Load<Texture2D>("EastWestDiagonal");
            TableHeight = height;
            TableWidth = width;
            topLeft = new Vector2(topLeftX, topLeftY);
        }

        
        /// <summary>
        /// Draws a square image on the screen
        /// </summary>
        /// <param name="image">Texture name</param>
        /// <param name="topPosition">Upper border of the image position</param>
        /// <param name="leftPosition">Left border of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="width">Widht of the image</param>
        void DrawSquare(Texture2D image, float topPosition, float leftPosition, float height, float width)
        {
            Rectangle destination = new Rectangle((int)topPosition, (int)leftPosition, (int)height, (int)width);
            game.SpriteBatch.Draw(image, destination, Color.White);
        }

        /// <summary>
        /// Draws the back ground of the table
        /// </summary>
        void DrawBackground()
        {
            DrawSquare(background, topLeft.X, topLeft.Y,  TableHeight,  TableWidth);
            DrawSquare(tableBorders, topLeft.X, topLeft.Y, TableHeight, TableWidth);
        }

        /// <summary>
        /// Fills the squares in the game table
        /// </summary>
        void DrawSquares()
        {
            for(int i = 0; i < 3; ++i)
                for(int j = 0; j < 3; ++j)
                {
                    Texture2D filling;
                    if (board[i, j] == CrossesOrNoughts.Crosses)
                        filling = game.Content.Load<Texture2D>("X");
                    else if (board[i, j] == CrossesOrNoughts.Naughts)
                        filling = game.Content.Load<Texture2D>("O");
                    else filling = null;

                    if (filling != null)
                        DrawSquare(filling, topLeft.X + i * SquareWidth, topLeft.Y + j * SquareHeight,
                            SquareWidth, SquareHeight);
                }
        }

        /// <summary>
        /// Marks with a line the rows that are all either noughts or crosses
        /// </summary>
        void MarkRows()
        {
            for(int i = 0; i < 3; ++i)
            {
                if(board.IsRowTaken(i))
                {
                    DrawSquare(horizontalLine, topLeft.X, topLeft.Y + SquareHeight * i, TableWidth, SquareHeight);
                }
            }
        }

        /// <summary>
        /// Marks the collumns that are all either noughts or crosses
        /// </summary>
        void MarkColumns()
        {
            for(int i = 0; i < 3; ++i)
            {
                if(board.IsColumnTaken(i))
                {
                    DrawSquare(verticalLine, topLeft.X + SquareWidth * i, topLeft.Y, SquareWidth, TableHeight);
                }
            }
        }

        /// <summary>
        /// Marks the main if it contains all noughts or crosses
        /// </summary>
        void MarkDiagonals()
        {
            if (board.IsMainDiagonalTaken())
                DrawSquare(westEastDiagonal, topLeft.X, topLeft.Y, TableWidth, TableHeight);
            if (board.IsSecondaryDiagonalTaken())
                DrawSquare(eastWestDiagonal, topLeft.X, topLeft.Y, TableWidth, TableHeight);
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
        }

        /// <summary>
        /// Translates 2 dimensional vector to position on the board
        /// </summary>
        /// <param name="clickPosition"></param>
        /// <returns></returns>
        public (int row, int column) BoardPosition(Vector2 clickPosition)
        {
            return ((int)((clickPosition.X - topLeft.X) / SquareWidth),
                    (int)((clickPosition.Y - topLeft.Y) / SquareHeight));
        }

        /// <summary>
        /// Processes mouse input from the user
        /// </summary>
        public void ProcessMouseInput()
        {
            MouseState mouseState = Mouse.GetState();
            
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                (int row, int column) = BoardPosition(new Vector2(mouseState.X, mouseState.Y));
                gameLogic.Update(row, column);
            }
        }

        /// <summary>
        /// Processes move that was entered as a pair of numbers
        /// </summary>
        /// <param name="row">Row number</param>
        /// <param name="column">Column number</param>
        public void ProcessDigitalInput(int row, int column)
        {
            gameLogic.Update(row, column);
        }

        /// <summary>
        /// Get input from player and update the state of the game
        /// </summary>
        public void Update()
        {
            gameLogic.CurrentPlayer.MakeMove();
        }

    }
}
