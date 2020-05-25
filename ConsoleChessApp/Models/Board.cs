using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ConsoleChessApp
{
    public class Board
    {
        public int Size { get; private set; }

        private Piece[,] _board;

        public Board(int size = 8)
        {
            Size = size;
            _board = new Piece[size,size];
        }
        
        public void AddPiece(Piece piece)
        {
            _board[piece.Square.Row, piece.Square.Column] = piece;
        }

        public bool Move(string oldSquare, string newSquare, Color playerColor)
        {
            return TryMovePiece(new Square(oldSquare), new Square(newSquare), playerColor);
        }

        public bool TryMovePiece(Square oldSquare, Square newSquare, Color playerColor)
        {
            // check piece is moving squares
            if (oldSquare.Notation != newSquare.Notation)
            {
                // check new  square is empty or occupied by opposing piece
                if (GetPieceAtSquare(newSquare)?.Color != playerColor)
                {
                    // clone the board and its content
                    var tempBoard = CloneBoard();

                    // try move on the clone board
                    if (tempBoard[oldSquare.Row, oldSquare.Column]?.TryMove(newSquare) ?? false)
                    {
                        tempBoard[newSquare.Row, newSquare.Column] = tempBoard[oldSquare.Row, oldSquare.Column];
                        tempBoard[oldSquare.Row, oldSquare.Column] = null;

                        // check the player's king is not in check
                        if (!IsKingInCheck(playerColor))
                        {
                            // commit new position
                            _board = tempBoard;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        public Piece GetPieceAtSquare(Square square)
        {
            return _board[square.Row, square.Column];
        }

        public bool IsKingInCheck(Color color)
        {
            // TO DO: add logic to assess king status

            // find king

            // check knight moves

            // check diagonal moves

            // check line moves

            return false;
        }

        private Piece[,] CloneBoard()
        {
            Piece[,] newBoard = new Piece[Size, Size];

            // traverse the board matrix and deep clone each piece
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    newBoard[row, col] = _board[row, col]?.DeepClone();
                }
            }

            return newBoard;
        }

        public void PrintBoard() 
        { 
            for(int row = Size-1; row >= 0; row--)
            {
                // print row specifier
                Console.Write("{0} ", row+1);

                for(int col = 0; col < Size; col++)
                {

                    // print piece
                    Console.Write("|{0}", _board[row, col]?.Icon ?? ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a b c d e f g h");

        }


    }
}
