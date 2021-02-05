using System;
using System.Collections.Generic;

namespace TitTacToeEX
{
    class Board
    {
        public bool IsPlayable { get; private set; }
        public bool CurrentPlayer { get; private set; }
        public string PlayerName => CurrentPlayer ? "Player 1" : "Player 2";

        public string WinState { get; private set; }
        
        string[] squares = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        readonly string row = "  {0}  |  {1}  |  {2}  ";
        readonly string div = "_____|_____|_____";

        List<int> playerOneMoves = new List<int>();
        List<int> playerTwoMoves = new List<int>();

        public Board()
        {
            CurrentPlayer = true;
            IsPlayable = true;
        }

        public void Draw()
        {
            Console.Clear();
            DrawLine(1);
            DrawLine(2);
            DrawLine(3);
            Console.WriteLine();
        }

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

        public void Results()
        {
            Draw();
            Console.WriteLine(WinState);
            Console.ReadLine();
        }

        private void CheckGameState()
        {
            // does either player have over three moves
            if (playerOneMoves.Count < 3 && playerTwoMoves.Count <3)
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

        private bool IsWinner(List<int> moves)
        {
            // check for 1,2,3
            if (CheckMoves(moves, new int[]{ 1, 2, 3})) {
                return true;
            }
            // check for 4,5,6
            if (CheckMoves(moves, new int[] { 4, 5, 6 }))
            {
                return true;
            }
            // check for 7,8,9
            if (CheckMoves(moves, new int[] { 7,8,9 }))
            {
                return true;
            }
            // check for 1,4,7
            if (CheckMoves(moves, new int[] { 1,4,7 }))
            {
                return true;
            }
            // check for 2,5,8
            if (CheckMoves(moves, new int[] { 2,5,8 }))
            {
                return true;
            }
            // check for 3,6,9
            if (CheckMoves(moves, new int[] { 3,6,9 }))
            {
                return true;
            }
            // check for 1,5,9
            if (CheckMoves(moves, new int[] { 1,5,9 }))
            {
                return true;
            }
            // check for 3,5,7
            if (CheckMoves(moves, new int[] { 3,5,7 }))
            {
                return true;
            }
            return false;
        }

        private bool CheckMoves(List<int> moves, int[] vs)
        {
            bool hasAll = true;
            foreach (int value in vs)
            {
                if (!moves.Contains(value))
                {
                    hasAll = false;
                }
            }
            return hasAll;
        }

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

        private void SetSquare(int sq)
        {
            if (CurrentPlayer)
            {
                playerOneMoves.Add(sq);
            } else
            {
                playerTwoMoves.Add(sq);
            }
            squares[sq - 1] = CurrentPlayer? "X" : "O";
        }

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
