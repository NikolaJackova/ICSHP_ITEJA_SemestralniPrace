﻿using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class StringExpression : IExpression
    {
        public string Text { get; private set; }

        public StringExpression(string text)
        {
            Text = text;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_StringExpression(this);
        }
    }
}
