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
        public Parser.Parser Parser { get; private set; }

        public Interpreter(string source){
            Lexer.Lexer lexer = new Lexer.Lexer(source);
            Parser = new Parser.Parser(lexer);
            Engine = new InterpretEngine(Parser);
        }
        /// <summary>
        /// Interprets AST from Parser through InterpetEngine
        /// </summary>
        public void Interpret()
        {
            Engine.Interpret();
        }
        public void SetPrint(PrintMethodDelegate @delegate)
        {
            Engine.Print = @delegate;
        }

        public void SetForward(ForwardMethodDelegate @delegate)
        {
            Engine.Forward = @delegate;
        }

        public void SetRotate(RotateMethodDelegate @delegate)
        {
            Engine.Rotate = @delegate;
        }

        public void SetBackward(BackwardMethodDelegate @delegate)
        {
            Engine.Backward = @delegate;
        }

        public void SetChangePen(ChangePenDelegate @delegate)
        {
            Engine.ChangePen = @delegate;
        }

        public void SetPenVisible(PenVisibileDelegate @delegate)
        {
            Engine.PenVisible = @delegate;
        }
    }
}
