using LanguageLibrary.Interpreter;
using LanguageLibrary.Parser.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class IfStatement : Statement
    {
        public Condition Condition { get; private set; }

        public LinkedList<Block> Blocks { get; private set; }

        public ElseStatement ElseStatement { get; private set; }
        public IfStatement(LinkedList<Block> blocks, Condition cond)
        {
            Condition = cond;
            Blocks = blocks;
            ElseStatement = null;
        }

        public IfStatement(LinkedList<Block> blocks, Condition cond, ElseStatement elseStatement)
        {
            Condition = cond;
            Blocks = blocks;
            ElseStatement = elseStatement;
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitIfStatement(this);
        }
    }
}
