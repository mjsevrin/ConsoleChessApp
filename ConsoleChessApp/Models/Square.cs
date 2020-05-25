using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ConsoleChessApp
{
    public class Square
    {

        public int Column { get; set; }
        public int Row { get; set; }

        public string Notation
        {
            get
            {
                string notation = Convert.ToChar(Column+97).ToString();
                notation += Convert.ToString(Row + 1);
                return notation;
            }
        }

        public Square(string notation)
        {
            string lowerCaseNotation = notation.ToLower();
            
            Column = lowerCaseNotation[0]-97;

            Row = int.Parse(lowerCaseNotation.Substring(1,1))-1;
        }

        public Square(int row, int column)
        {
            Column = column;
            Row = row;
        }
    }
}
