using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageLibrary;
using LanguageLibrary.Exceptions;
using LanguageLibrary.Lexer.Tokens;
using LanguageLibrary.Parser.Conditions;
using LanguageLibrary.Parser.Expressions;
using LanguageLibrary.Parser.Statements;
using LanguageLibrary.Parser.Variables;

namespace LanguageLibrary.Parser
{
    public class Parser
    {
        public Lexer.Lexer Lexer { get; private set; }
        private ParserEngine Engine { get; set; }

        public Parser(Lexer.Lexer lexer)
        {
            Engine = new ParserEngine(lexer);
            Lexer = lexer;
        }
        /// <summary>
        /// Method for parsing stream of tokens from Lexer
        /// </summary>
        /// <returns>Program as a root node</returns>
        public Program Parse()
        {
            Engine.ResetParser();
            return Engine.GetProgram();
        }
    }
}
