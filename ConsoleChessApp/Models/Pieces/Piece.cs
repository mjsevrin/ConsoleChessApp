using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public abstract class Piece
    {
        private readonly Board _board;

        public readonly Color Color;

        public Square Square { get; set; }

        public abstract string Icon { get; }

        public Piece(Board board, Color color, Square square)
        {
            _board = board;
            Color = color;
            Square = square;
        }

        public Piece DeepClone()
        {
            Piece clone = (Piece)MemberwiseClone();
            clone.Square = new Square(Square.Notation);
            return clone;
        }

        public virtual bool TryMove(Square newSquare)
        {
            //check against piece movement
            if (IsValidMove(newSquare))
            {
                Square = newSquare;
                return true;
            }
            return false;
        }

        public abstract bool IsValidMove(Square new_square);
    }
}
