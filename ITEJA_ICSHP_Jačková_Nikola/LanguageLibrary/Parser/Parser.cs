using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageLibrary;

namespace LanguageLibrary.Parser
{
    class Parser
    {
        public Lexer.Lexer Lexer { get; private set; }

        public Parser(Lexer.Lexer lexer)
        {
            Lexer = lexer;
        }
    }
}
