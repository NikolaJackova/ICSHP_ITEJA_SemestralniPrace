using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Lexer.Tokens
{
    /// <summary>
    /// Enum representing possible types of tokens
    /// </summary>
    public enum TokenType
    {
        BEGIN,
        END,

        IF,
        ELSE,
        WHILE,
        VAR,
        METHOD,
        FOR,
        FROM,
        TO,

        DOT,
        SEMICOLON,
        COMMA,
        QUOTE,

        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,

        STRING,
        NUMBER,
        IDENTIFIER,

        EQUAL,
        NOT_EQUAL,
        LESS_THEN,
        GREATER_THEN,
        LESS_EQ_THEN,
        GREATER_EQ_THEN,
        ASSIGN,

        L_ROUND_BRACKET,
        R_ROUND_BRACKET,
        L_CURLY_BRACKET,
        R_CURLY_BRACKET
    }
}
