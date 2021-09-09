using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Symbol : Token
    {
        public static readonly char[] SYMBOLS = { '+', '-', '*', '/', '^', '(', ')', ','};

        public static bool IsSymbol(char c)
        {
            for (int i = 0; i < SYMBOLS.Length; i++)
            {
                if (c == SYMBOLS[i])
                    return true;
            }

            return false;
        }

        private char value;

        public char Value
        {
            get
            {
                return value;
            }
        }

        public Symbol(char value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "Symbol: " + value;
        }
    }
}
