using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Variable : Token
    {
        public static bool IsLetter(char c)
        {
            return 'A' <= c && c <= 'Z' || 'a' <= c && c <= 'z';
        }

        public static bool CanBeAVariableName(string name)
        {
            if (name.Length == 0)
                return false;

            if (Constant.IsConstant(name))
                return false;

            if (Function.IsFunction(name))
                return false;

            if (!IsLetter(name[0]))
                return false;

            for (int i = 1; i < name.Length; i++)
            {
                char c = name[i];
                if (!IsLetter(c) && c != '_')
                    return false;
            }

            return true;
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Variable(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return "variable '" + name + "'";
        }
    }
}
