using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class Bishop : Piece
    {
        public override string Icon { get => Color == Color.White ? "B" : "b"; }

        public Bishop(Board board, Color color, Square square) : base(board, color, square)
        {
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square.Column;

            // diagonal movements only
            return rowDiff != 0 && Math.Abs(rowDiff) == Math.Abs(columnDiff);
        }
    }
}
