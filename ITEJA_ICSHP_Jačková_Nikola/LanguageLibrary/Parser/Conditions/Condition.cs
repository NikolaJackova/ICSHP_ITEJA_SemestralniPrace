using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    class Condition
    {
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }
        public string Operation { get; protected set; }

        public Condition(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }
}
