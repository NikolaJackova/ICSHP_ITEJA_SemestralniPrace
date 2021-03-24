using LanguageLibrary.Exceptions;
using LanguageLibrary.Parser;
using LanguageLibrary.Parser.Conditions;
using LanguageLibrary.Parser.Expressions;
using LanguageLibrary.Parser.Statements;
using LanguageLibrary.Parser.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Interpreter
{
    public class Interpreter
    {
        private InterpretEngine Engine { get; set; }
        public Interpreter(Parser.Parser parser)
        {
            Engine = new InterpretEngine(parser);
            
        }
        /// <summary>
        /// Interprets AST from Parser through InterpetEngine
        /// </summary>
        public void Interpret()
        {
            Engine.Interpret();
        }
    }
}
