using LanguageLibrary.AST;
using LanguageLibrary.Parser.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class WhileStatement : IStatement
    {
        public Condition Condition { get; private set; }
        public LinkedList<Block> Blocks { get; private set; }

        public WhileStatement(LinkedList<Block> blocks, Condition cond)
        {
            Condition = cond;
            Blocks = blocks;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_WhileStatement(this);
        }
    }
}
