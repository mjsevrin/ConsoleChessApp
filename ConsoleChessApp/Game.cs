using ConsoleChessApp.Models.Pieces;
using System;

namespace ConsoleChessApp
{
    class Game
    {
        static void Main()
        {
            Board board = new Board();

            board.AddPiece(new Rook(board, Color.White, new Square("a1")));

            board.PrintBoard(); 
            
            board.Move("a1", "a2", Color.White);
            

            board.PrintBoard();

            
        }
    }
}
