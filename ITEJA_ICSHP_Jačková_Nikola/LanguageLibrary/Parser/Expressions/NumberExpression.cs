using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    class NumberExpression : Expression
    {
        public double Value { get; private set; }

        public NumberExpression(double number) {
            Value = number;
        }
    }
}
