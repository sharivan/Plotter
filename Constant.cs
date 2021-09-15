using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Constant : Token
    {    
        public static readonly string[] CONSTANTS = { "e", "pi" };

        public static bool IsConstant(string name)
        {
            for (int i = 0; i < CONSTANTS.Length; i++)
                if (name == CONSTANTS[i])
                    return true;

            return false;
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Constant(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return "constant '" + name + "'";
        }
    }
}
