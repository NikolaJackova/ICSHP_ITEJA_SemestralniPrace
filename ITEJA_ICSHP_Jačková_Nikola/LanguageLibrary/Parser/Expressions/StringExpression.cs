using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    class StringExpression : Expression
    {
        public string Text { get; private set; }

        public StringExpression(string text)
        {
            Text = text;
        }
    }
}
