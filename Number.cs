using System;
using System.Collections.Generic;
using System.Text;

namespace Plotter
{
    public class Number : Token
    {
        public float Value { get; }

        public Number(float value)
        {
            this.Value = value;
        }

        public override string ToString() => "number '" + Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "'";
    }
}
