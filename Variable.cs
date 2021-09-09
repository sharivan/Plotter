using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Variable : Token
    {
        public static bool IsVariable(char c)
        {
            return 'A' <= c && c <= 'Z' || 'a' <= c && c <= 'z';
        }

        private char value;

        public char Value
        {
            get
            {
                return value;
            }
        }

        public Variable(char value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "Variable: " + value;
        }
    }
}
