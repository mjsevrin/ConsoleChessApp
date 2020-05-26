using ConsoleChessApp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        public void Init()
        {
            // rooks
            AddPiece(new Rook(this, Color.White, new Square("a1")));
            AddPiece(new Rook(this, Color.White, new Square("h1")));
            AddPiece(new Rook(this, Color.Black, new Square("a8")));
            AddPiece(new Rook(this, Color.Black, new Square("h8")));

            // knights
            AddPiece(new Knight(this, Color.White, new Square("b1")));
            AddPiece(new Knight(this, Color.White, new Square("g1")));
            AddPiece(new Knight(this, Color.Black, new Square("b8")));
            AddPiece(new Knight(this, Color.Black, new Square("g8")));

            // bishops
            AddPiece(new Bishop(this, Color.White, new Square("c1")));
            AddPiece(new Bishop(this, Color.White, new Square("f1")));
            AddPiece(new Bishop(this, Color.Black, new Square("c8")));
            AddPiece(new Bishop(this, Color.Black, new Square("f8")));

            // queens 
            AddPiece(new Queen(this, Color.White, new Square("d1")));
            AddPiece(new Queen(this, Color.Black, new Square("d8")));

            // kings
            AddPiece(new King(this, Color.White, new Square("e1")));
            AddPiece(new King(this, Color.Black, new Square("e8")));

            // pawns
            for(int col = 0; col <= 7; col++)
            {
                AddPiece(new Pawn(this, Color.White, new Square(1, col)));
                AddPiece(new Pawn(this, Color.Black, new Square(6, col)));
            }
        }

        public void Clear()
        {
            _board = new Piece[Size, Size];
        }

        public void Reset()
        {
            Clear();
            Init();
        }

        public void AddPiece(Piece piece)
        {
            _board[piece.Square.Row, piece.Square.Column] = piece;
        }

        public bool Move(string oldSquare, string newSquare, Color playerColor)
        {
            // TO DO: add logic for special moves (O-O, O-O-O, En Passant)

            return TryMovePiece(new Square(oldSquare), new Square(newSquare), playerColor);
        }

        public bool TryMovePiece(Square oldSquare, Square newSquare, Color playerColor)
        {
            // check target square is valid placement
            if (ValidateSquare(oldSquare, newSquare, playerColor))
            {
                // check path to newSquare
                if (ValidatePath(oldSquare, newSquare))
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

        private bool ValidateSquare(Square oldSquare, Square newSquare, Color playerColor)
        {
            // check piece is moving squares
            if (oldSquare.Notation != newSquare.Notation)
            {
                // check new  square is empty or occupied by opposing piece
                if (GetPieceAtSquare(newSquare)?.Color != playerColor)
                {
                    // check piece being moved exists and is of the player's color
                    if (GetPieceAtSquare(oldSquare)?.Color == playerColor)
                        return true;
                }
            }
            return false;
        }

        private bool ValidatePath(Square oldSquare, Square newSquare)
        {
            // Knights can jump over pieces
            if(GetPieceAtSquare(oldSquare).GetType() != typeof(Knight))
            {
                return RecursiveCheckPath(CalculateSubPath(oldSquare, newSquare));
            }

            return true;
        }

        private bool RecursiveCheckPath(Tuple<Square, Square> path)
        {
            Square oldSquare = path.Item1;
            Square newSquare = path.Item2;

            // base case
            if (oldSquare.Notation == newSquare.Notation)
            {
                return true;
            }

            // recursive case
            if(GetPieceAtSquare(newSquare) == null)
            {
                return RecursiveCheckPath(CalculateSubPath(oldSquare, newSquare));
            }
            else
            {
                return false;
            }
        }

        public Tuple<Square, Square> CalculateSubPath(Square oldSquare, Square newSquare)
        {
            // calculate next closest square
            int rowStep = oldSquare.Row.CompareTo(newSquare.Row);
            int colStep = oldSquare.Column.CompareTo(newSquare.Column);
            Square pathSquare = new Square(newSquare.Row + rowStep, newSquare.Column + colStep);

            return new Tuple<Square, Square>(oldSquare, pathSquare);
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
