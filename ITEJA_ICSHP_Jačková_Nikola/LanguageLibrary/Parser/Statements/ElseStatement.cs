using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class ElseStatement : Statement
    {
        public LinkedList<Block> Blocks { get; private set; }
        public ElseStatement(LinkedList<Block> blocks)
        {
            Blocks = blocks;
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitElseStatement(this);
        }
    }
}
