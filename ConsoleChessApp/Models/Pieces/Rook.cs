using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp.Models.Pieces
{
    public class Rook : Piece
    {
        public bool HasMoved { get; private set; }

        public override string Icon { get => Color == Color.White ? "R" : "r"; }

        public Rook(Board board, Color color, Square square, bool hasMoved = false) : base(board, color, square)
        {
            HasMoved = hasMoved;
        }

        public override bool IsValidMove(Square newSquare)
        {
            int rowDiff = newSquare.Row - Square.Row;
            int columnDiff = newSquare.Column - Square. Column;

            if(rowDiff == 0 || columnDiff == 0)
            {
                // TO DO: IMPLEMENT PATH LOGIC

                // check for clear path to new square
                return true;    
                    
            }
            return false;
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
