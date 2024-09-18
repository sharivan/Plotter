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

        public char Value { get; }

        public Symbol(char value)
        {
            this.Value = value;
        }

        public override string ToString() => "symbol '" + Value + "'";
    }
}
