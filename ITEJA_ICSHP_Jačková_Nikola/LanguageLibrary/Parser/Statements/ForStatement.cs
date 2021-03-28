using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class ForStatement : Statement
    {
        public IdentExpression Identifier { get; private set; }
        public Expression From { get; private set; }
        public Expression To { get; private set; }
        public SetStatement Statement { get; private set; }
        public LinkedList<Block> Blocks { get; private set; }
        public ForStatement(IdentExpression ident, Expression from, Expression to, SetStatement statement, LinkedList<Block> blocks)
        {
            Identifier = ident;
            From = from;
            To = to;
            Statement = statement;
            Blocks = blocks;
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitForStatement(this);
        }
    }
}
