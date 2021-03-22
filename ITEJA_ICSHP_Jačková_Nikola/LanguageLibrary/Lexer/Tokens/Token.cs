using LanguageLibrary.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Lexer.Tokens
{
    /// <summary>
    /// Class representing token
    /// </summary>
    public class Token
    {
        public string Value { get; set; }
        public TokenType TokenType { get; private set; }

        public Token(string value, TokenType type)
        {
            this.Value = value;
            this.TokenType = type;
        }
        public override string ToString()
        {
            return "Token{type: " + TokenType + ", value: " + Value + "}";
        }
    }
}
