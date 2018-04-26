using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TicTacToe
{
    /// <summary>
    /// An interface representing the player in the game.
    /// </summary>
    public interface IPlayer
    {
        CrossesOrNoughts Symbol();
        void MakeMove();
        string Name();
    }
}
