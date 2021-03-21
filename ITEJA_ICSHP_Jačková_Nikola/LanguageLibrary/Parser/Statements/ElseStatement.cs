using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    class ElseStatement : Statement
    {
        public Block Block { get; private set; }
        public ElseStatement(Block block)
        {
            Block = block;
        }
    }
}
