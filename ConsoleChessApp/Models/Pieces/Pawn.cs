using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class Pawn : Piece
    {
        public bool HasMoved { get; private set; }

        public override string Icon { get => Color == Color.White ? "R" : "r"; }

        public Pawn(Board board, Color color, Square square, bool hasMoved = false) : base(board, color, square)
        {
            HasMoved = hasMoved;
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square. Column;

            // only allow horizontal or vertical movement
            return Convert.ToInt32(Color) * rowDiff == 1 && Math.Abs(columnDiff) <= 1;
        }

        public override bool TryMove(Square newSquare)
        {
            if(base.TryMove(newSquare))
            {
                HasMoved = false;
                return true;
            }
            return false;
        }
    }
}
