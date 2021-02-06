using System;
using System.Collections.Generic;
using System.Linq;

namespace TitTacToeEX
{
    public class Board
    {
        public bool IsPlayable { get; private set; }
        public bool CurrentPlayer { get; private set; }
        public string PlayerName { get { return CurrentPlayer ? "Player 1" : "Player 2"; } }
        public string WinState { get; private set; }
        private string[] squares = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private readonly string row = "  {0}  |  {1}  |  {2}  ";
        private readonly string div = "_____|_____|_____";
        private List<int> playerOneMoves = new List<int>();
        private List<int> playerTwoMoves = new List<int>();

        public Board()
        {
            CurrentPlayer = true;
            IsPlayable = true;
            WinState = "The game is being played.";
        }

        /// <summary>
        /// Draw the game board
        /// </summary>
        public void Draw()
        {
            Console.Clear();
            DrawLine(1);
            DrawLine(2);
            DrawLine(3);
            Console.WriteLine();
        }

        /// <summary>
        /// Capture the player move and update board
        /// </summary>
        public void Move()
        {
            bool isValid = false;
            int choice = 0;
            while (!isValid)
            {
                // get the move from player
                Console.WriteLine("{0}: Choose your Field!", PlayerName);
                Int32.TryParse(Console.ReadLine(), out choice);
                // validate move
                isValid = ValidateChoice(choice);
            }

            // Update game board
            SetSquare(choice);

            // Check for game state
            CheckGameState();
            // Toggle player
            if (IsPlayable)
            {
                CurrentPlayer = !CurrentPlayer;
            }
        }

        /// <summary>
        /// Get the state of the game
        /// </summary>
        public void Results()
        {
            Draw();
            Console.WriteLine(WinState);
            Console.ReadLine();
        }

        /// <summary>
        /// Determine the current state of the game
        /// </summary>
        private void CheckGameState()
        {
            // does either player have over three moves
            if (playerOneMoves.Count < 3 && playerTwoMoves.Count < 3)
            {
                return;
            }
            // does player 1 or 2 have a win state
            if (IsWinner(playerOneMoves) || IsWinner(playerTwoMoves))
            {
                WinState = PlayerName + " has won!";
                IsPlayable = false;
                return;
            }
            // does players moves add up to 9
            if (playerOneMoves.Count + playerTwoMoves.Count >= 9)
            {
                WinState = "The game was a draw.";
                IsPlayable = false;
            }
        }

        /// <summary>
        /// Determine if any player has made winning move.
        /// </summary>
        /// <param name="moves"></param>
        /// <returns>bool</returns>
        private bool IsWinner(IEnumerable<int> moves)
        {
            int[,] winningCombos =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 3, 6, 9 },
                { 1, 5, 9 },
                { 3, 5, 7 }
            };

            for (int col = 0; col < winningCombos.GetLength(0); col++)
            {
                int[] combo = Enumerable.Range(0, winningCombos.GetLength(1))
                    .Select(x => winningCombos[col, x])
                    .ToArray();

                if (CheckMoves(moves, combo))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Perform a check on the moves list to see if it contains the winning combo
        /// </summary>
        /// <param name="moves"></param>
        /// <param name="wc"></param>
        /// <returns>bool</returns>
        private bool CheckMoves(IEnumerable<int> moves, int[] wc)
        {
            bool hasAll = true;
            foreach (int value in wc)
            {
                if (!moves.Contains(value))
                {
                    hasAll = false;
                }
            }
            return hasAll;
        }

        /// <summary>
        /// Validate the player choice
        /// </summary>
        /// <param name="choice"></param>
        /// <returns>bool</returns>
        private bool ValidateChoice(int choice)
        {
            // must be an int between 1 and 9
            if (choice < 1 || choice > 9)
            {
                Console.WriteLine("Your choice must be between 1 and 9.");
                return false;
            }
            // can not be a move already made by players
            if (playerOneMoves.Contains(choice) || playerTwoMoves.Contains(choice))
            {
                Console.WriteLine("This move has already been made.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Update the value of the squares array
        /// </summary>
        /// <param name="sq"></param>
        private void SetSquare(int sq)
        {
            if (CurrentPlayer)
            {
                playerOneMoves.Add(sq);
            }
            else
            {
                playerTwoMoves.Add(sq);
            }
            squares[sq - 1] = CurrentPlayer ? "X" : "O";
        }

        /// <summary>
        /// Output the lines of the game board
        /// </summary>
        /// <param name="line"></param>
        private void DrawLine(int line)
        {
            switch (line)
            {
                case 1:
                    Console.WriteLine(this.row, " ", " ", " ");
                    Console.WriteLine(row, squares[0], squares[1], squares[2]);
                    Console.WriteLine(div);
                    break;

                case 2:
                    Console.WriteLine(this.row, " ", " ", " ");
                    Console.WriteLine(row, squares[3], squares[4], squares[5]);
                    Console.WriteLine(div);
                    break;

                case 3:
                    Console.WriteLine(this.row, " ", " ", " ");
                    Console.WriteLine(row, squares[6], squares[7], squares[8]);
                    Console.WriteLine(this.row, " ", " ", " ");
                    break;
            }
        }
    }
}
