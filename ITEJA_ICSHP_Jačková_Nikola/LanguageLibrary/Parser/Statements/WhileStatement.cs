using LanguageLibrary.Parser.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    class WhileStatement : Statement
    {
        public Condition Condition { get; private set; }
        public Block Block { get; private set; }

        public WhileStatement(Block block, Condition cond)
        {
            Condition = cond;
            Block = block;
        }
    }
}
