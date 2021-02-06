namespace TitTacToeEX
{
    internal class Program
    {
        private static void Main(string[] args)
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
