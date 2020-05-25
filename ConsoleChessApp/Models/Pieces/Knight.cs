using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class Knight : Piece
    {
        public override string Icon { get => Color == Color.White ? "N" : "n"; }

        public Knight(Board board, Color color, Square square) : base(board, color, square)
        {
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square.Column;

            // L-shape moves
            return Math.Abs(rowDiff * columnDiff) == 2;
        }
    }
}
