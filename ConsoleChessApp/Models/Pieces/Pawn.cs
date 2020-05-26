using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class Pawn : Piece
    {
        public bool HasMoved { get; private set; }

        public override string Icon { get => Color == Color.White ? "P" : "p"; }

        public Pawn(Board board, Color color, Square square, bool hasMoved = false) : base(board, color, square)
        {
            HasMoved = hasMoved;
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square. Column;

            // allow long move if pawn hasn't moved yet
            bool moveAllowed = false;
            if(!HasMoved)
            {
                moveAllowed = Convert.ToInt32(Color) * rowDiff <= 2 && columnDiff == 0;
            }
            // allow to move to 3 spaces in front of pawn
            moveAllowed |= Convert.ToInt32(Color) * rowDiff == 1 && Math.Abs(columnDiff) <= 1;

            return moveAllowed;
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
