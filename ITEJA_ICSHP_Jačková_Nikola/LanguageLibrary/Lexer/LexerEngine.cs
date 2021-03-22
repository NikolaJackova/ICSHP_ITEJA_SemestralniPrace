using LanguageLibrary.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Lexer
{
    class LexerEngine
    {
        private char CurrentChar { get; set; }
        private int Position { get; set; }
        public string Source { get; set; }
        private IDictionary<string, TokenType> KeyWords { get; set; }

        #region Public Methods
        public LexerEngine(string source)
        {
            InitDictionary();
            Source = source;
            Position = 0;
            CurrentChar = Source[Position];
        }

        public LinkedList<Token> LoadList()
        {
            LinkedList<Token> tokens = new LinkedList<Token>();
            while (Position < Source.Length)
            {
                if (char.IsDigit(CurrentChar))
                {
                    tokens.AddLast(GetNumberToken());
                    continue;
                }
                else if (char.IsLetter(CurrentChar))
                {
                    tokens.AddLast(GetTextToken());
                    continue;
                }
                else if (CurrentChar == '\"')
                {
                    tokens.AddLast(new Token(CurrentChar.ToString(), TokenType.QUOTE));
                    NextChar();
                    tokens.AddLast(GetStringToken());
                    tokens.AddLast(new Token(CurrentChar.ToString(), TokenType.QUOTE));
                    NextChar();
                    continue;
                }
                else if (char.IsWhiteSpace(CurrentChar))
                {
                    SkipWhiteSpace();
                    continue;
                }
                else if (IsTwoCharToken())
                {
                    tokens.AddLast(GetTwoSymbolToken());
                    if (NextChar())
                    {
                        if (NextChar())
                        {
                            continue;
                        }
                    }
                    break;
                }
                else if (IsOneCharToken())
                {
                    tokens.AddLast(GetOneSymbolToken());
                    if (NextChar())
                    {
                        continue;
                    }
                    break;
                }
                else {
                    throw new LanguageException("Wrong character!");
                }
            }
            return tokens;
        }
        #endregion Public Methods

        #region Private Methods
        #region Get Token Methods
        private Token GetStringToken()
        {
            string @string = "";
            bool nextChar = true;
            while (nextChar && CurrentChar != '\"')
            {
                @string += CurrentChar;
                nextChar = NextChar();
            }
            return new Token(@string, TokenType.STRING);

        }
        private Token GetTextToken()
        {
            string result = CurrentChar.ToString();
            bool nextChar = NextChar();
            while (nextChar && char.IsLetterOrDigit(CurrentChar))
            {
                result += CurrentChar;
                nextChar = NextChar();
            }
            if (KeyWords.TryGetValue(result, out TokenType type))
            {
                return new Token(result, type);
            }
            return new Token(result, TokenType.IDENTIFIER);
        }
        private Token GetNumberToken()
        {
            string number = "";
            bool nextChar = true;
            while (nextChar && char.IsDigit(CurrentChar))
            {
                number += CurrentChar;
                nextChar = NextChar();
            }
            if (CurrentChar == '.')
            {
                number += CurrentChar;
                nextChar = NextChar();
                while (nextChar && char.IsDigit(CurrentChar))
                {
                    number += CurrentChar;
                    nextChar = NextChar();
                }
            }
            return new Token(number, TokenType.NUMBER);
        }
        private Token GetOneSymbolToken()
        {
            switch (CurrentChar)
            {
                case '+':
                    return new Token("+", TokenType.PLUS);
                case '-':
                    return new Token("-", TokenType.MINUS);
                case '*':
                    return new Token("*", TokenType.MULTIPLY);
                case '/':
                    return new Token("/", TokenType.DIVIDE);
                case '.':
                    return new Token(".", TokenType.DOT);
                case ';':
                    return new Token(";", TokenType.SEMICOLON);
                case ',':
                    return new Token(",", TokenType.COMMA);
                case '<':
                    return new Token("<", TokenType.LESS_THEN);
                case '>':
                    return new Token(">", TokenType.GREATER_THEN);
                case '=':
                    return new Token("=", TokenType.ASSIGN);
                case '(':
                    return new Token("(", TokenType.L_ROUND_BRACKET);
                case ')':
                    return new Token(")", TokenType.R_ROUND_BRACKET);
                case '{':
                    return new Token("{", TokenType.L_CURLY_BRACKET);
                case '}':
                    return new Token("}", TokenType.R_CURLY_BRACKET);
            }
            throw new LanguageException("Unknown one-symbol token!");
        }

        private Token GetTwoSymbolToken()
        {
            if (CurrentChar == '<' && ShowNextChar() == '=')
            {
                return new Token("<=", TokenType.LESS_EQ_THEN);
            }
            else if (CurrentChar == '>' && ShowNextChar() == '=')
            {
                return new Token(">=", TokenType.GREATER_EQ_THEN);
            }
            else if (CurrentChar == '=' && ShowNextChar() == '=')
            {
                return new Token("==", TokenType.EQUAL);
            }
            else if (CurrentChar == '!' && ShowNextChar() == '=')
            {
                return new Token("!=", TokenType.NOT_EQUAL);
            }
            throw new LanguageException("Unknown two-symbol token!");
        }
        #endregion Get Token Methods
        private char ShowNextChar()
        {
            if (Position + 1 > Source.Length - 1)
            {
                return char.MinValue;
            }
            return Source[Position + 1];
        }
        private bool IsTwoCharToken()
        {
            return (CurrentChar == '<' && ShowNextChar() == '=') || (CurrentChar == '>' && ShowNextChar() == '=') ||
                (CurrentChar == '=' && ShowNextChar() == '=') || (CurrentChar == '!' && ShowNextChar() == '=');
        }
        private bool IsOneCharToken()
        {
            return CurrentChar == '+' || CurrentChar == '-' || CurrentChar == '*' || CurrentChar == '/' || CurrentChar == '.' ||
                CurrentChar == ',' || CurrentChar == ';' || CurrentChar == '<' || CurrentChar == '>' || CurrentChar == '=' ||
                CurrentChar == '(' || CurrentChar == ')' || CurrentChar == '{' || CurrentChar == '}';
        }
        private void SkipWhiteSpace()
        {
            bool nextChar = true;
            while (nextChar && char.IsWhiteSpace(CurrentChar))
            {
                nextChar = NextChar();
            }
        }
        private bool NextChar()
        {
            if (Position + 1 > Source.Length - 1)
            {
                return false;
            }
            Position += 1;
            CurrentChar = Source[Position];
            return true;
        }
        private void InitDictionary()
        {
            KeyWords = new Dictionary<string, TokenType>()
            {
                {"BEGIN", TokenType.BEGIN},
                {"END", TokenType.END},
                {"if", TokenType.IF},
                {"else", TokenType.ELSE},
                {"while", TokenType.WHILE},
                { "var", TokenType.VAR},
                {"method", TokenType.METHOD},
                {"for", TokenType.FOR},
                {"from", TokenType.FROM},
                {"to", TokenType.TO}
            };
        }
        #endregion Private Methods
    }
}
