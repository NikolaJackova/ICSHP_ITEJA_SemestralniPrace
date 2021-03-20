using LanguageLibrary.Parser.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    class Block
    {
        public LinkedList<Statement> Statements { get; private set; }
    }
}
