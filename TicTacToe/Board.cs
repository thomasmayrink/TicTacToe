using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A class responsible for representing the board in the game
namespace TicTacToe
{
    /// <summary>
    /// Board to represent board in Noughts and crosses game
    /// </summary>
    public class GameBoard
    {    
        private CrossesOrNoughts[,]  entries;

        /// <summary>
        /// Checks whether the position is within bounds
        /// </summary>
        /// <param name="row">Position row</param>
        /// <param name="column">Position column</param>
        /// <returns>True if position is within bounds, false otherwise</returns>
        public bool IsValidPosition(int row, int column) => !(row < 0 || row >= 3 || column < 0 || column >= 3);
        public bool IsFree(int row, int column) => !(entries[row, column] == CrossesOrNoughts.Neither);
        public bool ShouldUpdate(int row, int column) => 
            IsValidPosition(row, column) && (entries[row, column] == CrossesOrNoughts.Neither);

        /// <summary>
        /// The construtor - accepts no arguments and creates 3 on 3 empty board
        /// </summary>
        public GameBoard()
        {
            entries = new CrossesOrNoughts[3, 3];
        }

        /// <summary>
        /// Indexer - returns the square at the given position
        /// </summary>
        /// <param name="row">Position row</param>
        /// <param name="column">Position column</param>
        /// <returns>Position entry</returns>
        public CrossesOrNoughts this[int row, int column]
        {
            get
            {
                if (IsValidPosition(row, column))
                    return entries[row, column];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (IsValidPosition(row, column))
                    entries[row, column] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Returns whether the entries in the array are same are either noughts or crosses
        /// </summary>
        /// <param name="crossesOrNoughts">The array os crosses or nouhgts</param>
        /// <returns>True if they are all same or false otherwise</returns>
        private bool HaveSameSign(params CrossesOrNoughts[] crossesOrNoughts)
        {
            for (int i = 0; i < crossesOrNoughts.Length - 1; ++i)
            {
                if (crossesOrNoughts[i] == CrossesOrNoughts.Neither) return false;
                if (crossesOrNoughts[i] != crossesOrNoughts[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the entries in the given row.
        /// </summary>
        /// <param name="row">Row numbers</param>
        /// <returns>The row entries in array form</returns>
        private CrossesOrNoughts[] TableRow(int row)
        {
            CrossesOrNoughts[] result = new CrossesOrNoughts[entries.GetLength(1)];
            for(int i = 0; i < result.Length; ++i)
            {
                result[i] = entries[i, row];
            }

            return result;
        }

        /// <summary>
        /// Returns the entries in the given column
        /// </summary>
        /// <param name="column">Column number</param>
        /// <returns>The column entries in array form</returns>
        private CrossesOrNoughts[] TableColumn(int column)
        {
            CrossesOrNoughts[] result = new CrossesOrNoughts[entries.GetLength(0)];

            for(int i = 0; i < result.Length; ++i)
            {
                result[i] = entries[column, i];
            }

            return result;
        }

        /// <summary>
        /// Returns the entries in diagonal from top left corner to buttom right corner
        /// </summary>
        /// <returns>Entries of the main diagonal in array form</returns>
        private CrossesOrNoughts[] MainDiagonal()
        {
            CrossesOrNoughts[] result = new CrossesOrNoughts[entries.GetLength(0)];
            for (int i = 0; i < result.Length; ++i)
                result[i] = entries[i, i];

            return result;
        }

        /// <summary>
        /// Return  the entries in the diagonal from buttom left to upper right corner
        /// </summary>
        /// <returns>The entries of the secondary diagonal in array form</returns>
        private CrossesOrNoughts[] SecondaryDiagonal()
        {
            CrossesOrNoughts[] result = new CrossesOrNoughts[entries.GetLength(0)];
            for (int i = 0; i < result.Length; ++i)
                result[i] = entries[result.Length - 1 - i, i];

            return result;
        }

        /// <summary>
        /// Checks whether the board is full
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            for(int i = 0; i < 3; ++i)
            {
                for(int j = 0; j < 3; ++j)
                {
                    if (entries[i, j] == CrossesOrNoughts.Neither) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether the given row is full and contains same signs
        /// </summary>
        /// <param name="row"></param>
        /// <returns>True if the column entries are either all noughts or all rosses, false otherwise</returns>
        public bool IsRowTaken(int row) => HaveSameSign(TableRow(row));

        /// <summary>
        /// Checks whether the given column is marked and contains same signs
        /// </summary>
        /// <param name="column"></param>
        /// <returns>True if the column entries are either all noughts or all crosses, false otherwise</returns>
        public bool IsColumnTaken(int column) => HaveSameSign(TableColumn(column));

        /// <summary>
        /// Checks whether the main diagonal is marked and contains same signs
        /// </summary>
        /// <returns>True if all the entries in the main diagonal are either all noughts or all crosses, false otherwise</returns>
        public bool IsMainDiagonalTaken() => HaveSameSign(MainDiagonal());

        /// <summary>
        /// Checks whther the secondary diagonal is marked and contains same signs
        /// </summary>
        /// <returns>True if all the entries in the main diagonal are either all noughts or all crosses, false otherwise</returns>
        public bool IsSecondaryDiagonalTaken() => HaveSameSign(SecondaryDiagonal());
    }
}
