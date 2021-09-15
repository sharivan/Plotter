using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Number : Token
    {
        private float value;

        public float Value
        {
            get
            {
                return value;
            }
        }

        public Number(float value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "number '" + value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "'";
        }
    }
}
