using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class ElseStatement : IStatement
    {
        public LinkedList<Block> Blocks { get; private set; }
        public ElseStatement(LinkedList<Block> blocks)
        {
            Blocks = blocks;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_ElseStatement(this);
        }
    }
}
