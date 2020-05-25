using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class Queen : Piece
    {
        public override string Icon { get => Color == Color.White ? "Q" : "q"; }

        public Queen(Board board, Color color, Square square) : base(board, color, square)
        {
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square.Column;

            // diagonal, vertical, and horizontal movements
            return Math.Abs(rowDiff) == Math.Abs(columnDiff) || rowDiff == 0 || columnDiff == 0;
        }
    }
}
