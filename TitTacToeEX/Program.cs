using System;

namespace TitTacToeEX
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            while (board.IsPlayable)
            {
                board.Draw();
                board.Move();
            }

            board.Results();
        }
    }
}
