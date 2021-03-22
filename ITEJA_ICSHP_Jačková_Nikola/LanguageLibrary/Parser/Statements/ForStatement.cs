using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class ForStatement : IStatement
    {
        public IdentExpression Identifier { get; private set; }
        public IExpression From { get; private set; }
        public IExpression To { get; private set; }
        public IStatement Statement { get; private set; }
        public LinkedList<Block> Blocks { get; private set; }
        public ForStatement(IdentExpression ident, IExpression from, IExpression to, IStatement statement, LinkedList<Block> blocks)
        {
            Identifier = ident;
            From = from;
            To = to;
            Statement = statement;
            Blocks = blocks;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_ForStatement(this);
        }
    }
}
