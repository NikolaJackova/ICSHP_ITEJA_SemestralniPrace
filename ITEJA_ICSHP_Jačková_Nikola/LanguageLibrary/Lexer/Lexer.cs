using LanguageLibrary.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Lexer
{
    /// <summary>
    /// Class for lexical analysis for fictional language
    /// </summary>
    public class Lexer
    {
        /// <summary>
        /// LinkedList for tokens
        /// </summary>
        public LinkedList<Token> Tokens { get; set; }
        private IEnumerator<Token> Enumerator { get; set; }
        private LexerEngine Engine { get; set; }

        public Lexer(string source)
        {
            Engine = new LexerEngine(source);
            Tokens = Engine.LoadList();
            Enumerator = Tokens.GetEnumerator();
        }
        /// <summary>
        /// Returns next token in list using enumerator
        /// </summary>
        /// <returns>Next token in token list</returns>
        public Token GetNextToken()
        {
            if (Enumerator.MoveNext())
            {
                return Enumerator.Current;
            }
            return null;
        }
        public void ResetEnumerator()
        {
            Enumerator.Reset();
        }
        /// <summary>
        /// Returns one string representing tokens in token list
        /// </summary>
        /// <returns>String for tokens in token list</returns>
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
