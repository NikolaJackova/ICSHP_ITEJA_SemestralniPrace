using LanguageLibrary.Parser.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    class IfStatement : Statement
    {
        public Condition Condition { get; private set; }
        public Block Block { get; private set; }

        public ElseStatement ElseStatement { get; private set; }
        public IfStatement(Block block, Condition cond)
        {
            Condition = cond;
            Block = block;
            ElseStatement = null;
        }

        public IfStatement(Block block, Condition cond, ElseStatement elseStatement)
        {
            Condition = cond;
            Block = block;
            ElseStatement = elseStatement;
        }
    }
}
