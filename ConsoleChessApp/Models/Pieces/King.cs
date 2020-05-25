using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class King : Piece
    {
        public bool HasMoved { get; private set; }

        public override string Icon { get => Color == Color.White ? "K" : "k"; }

        public King(Board board, Color color, Square square, bool hasMoved = false) : base(board, color, square)
        {
            HasMoved = hasMoved;
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square.Column;

            // 1 square, diagonal, vertical, and horizontal movements
            return Math.Abs(rowDiff) <= 1 && Math.Abs(columnDiff) <= 1;
        }

        public override bool TryMove(Square newSquare)
        {
            if (base.TryMove(newSquare))
            {
                HasMoved = false;
                return true;
            }
            return false;
        }
    }
}
