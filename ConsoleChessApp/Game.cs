using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    class Game
    {
        public Board Board { get; private set; }

        public int Turn { get; private set; }

        public void PlayGame()
        {
            Board board = new Board();
            board.Init();
            board.PrintBoard();

            // game loop
            string input = "";
            int moveCounter = 1;
            while (input != "X")
            {
                // player 1 takes turns
                MakeMove(board, Color.White);

                // player 2 takes turns
                MakeMove(board, Color.Black);

                moveCounter++;
            }
        }

        public void MakeMove(Board board, Color playerColor)
        {
            string[] move;
            do
            {
                move = WaitForValidMove();
            } while (board.Move(move[0], move[1], playerColor));

            board.PrintBoard();
        }

        public string[] WaitForValidMove()
        {
            string input;
            do
            {
                input = Console.ReadLine();
            } while (!ValidateStringMove(input));

            return input.Split('-');
        }

        public bool ValidateStringMove(string move)
        {
            move = move.ToLower();
            if (move == "o-o" || move == "o-o-o")
                return true;
            if (move.Length == 5)
            {
                if (move[2] == '-')
                {
                    string[] squares = move.Split('-');
                    foreach (string square in squares)
                    {
                        if (!ValidSquare(square))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public bool ValidSquare(string square)
        {
            if (square[0] >= 'a' && square[0] <= 'h')
            {
                if (int.Parse(square[1].ToString()) >= 1 && int.Parse(square[1].ToString()) <= 8)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
