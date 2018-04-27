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
        void SetSymbol(CrossesOrNoughts symbol);
        void MakeMove();
        string Name();
        int Score();
        void UpdateScore(int differense);
    }
}
