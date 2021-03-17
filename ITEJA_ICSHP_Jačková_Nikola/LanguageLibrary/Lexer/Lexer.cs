using LanguageLibrary.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Lexer
{
    public class Lexer
    {
        public LinkedList<Token> Tokens { get; set; }
        private IEnumerator<Token> Enumerator { get; set; }
        private LexerEngine Engine { get; set; }

        public Lexer(string source)
        {
            Engine = new LexerEngine(source);
            Tokens = Engine.LoadList();
            Enumerator = Tokens.GetEnumerator();
        }
        public Token GetNextToken()
        {
            if (Enumerator.MoveNext())
            {
                return Enumerator.Current;
            }
            //throw new IndexOutOfRangeException("There is no more tokens!");
            return null;
        }
        public string TokensToString() {
            StringBuilder builder = new StringBuilder();
            foreach (var token in Tokens)
            {
                builder.Append(token.ToString() + "\n");
            }
            return builder.ToString();
        }
    }
}
