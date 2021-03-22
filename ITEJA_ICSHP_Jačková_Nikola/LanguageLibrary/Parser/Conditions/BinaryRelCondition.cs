using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    public abstract class BinaryRelCondition : Condition
    {
        public IExpression Right { get; private set; }
        public string Operation { get; protected set; }

        public BinaryRelCondition(IExpression left, IExpression right) :base(left)
        {
            Right = right;
        }
    }
}
