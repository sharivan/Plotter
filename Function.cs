using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Function : Token
    {
        public static readonly string[] FUNCTIONS = { "sqrt", "cbrt", "exp", "ln", "sen", "cos", "tg", "sec", "cosec", "cotg", "arcsen", "arccos", "arctg", "abs", "senh", "cosh", "tgh", "argsenh", "argcosh", "argtgh" };

        public static bool IsFunction(string name)
        {
            for (int i = 0; i < FUNCTIONS.Length; i++)
                if (name == FUNCTIONS[i])
                    return true;

            return false;
        }

        public string Name { get; }

        public Function(string name)
        {
            this.Name = name;
        }

        public override string ToString() => "function '" + Name + "'";
    }
}
