using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageLibrary;
using LanguageLibrary.Lexer.Tokens;
using LanguageLibrary.Parser.Conditions;
using LanguageLibrary.Parser.Expressions;
using LanguageLibrary.Parser.Statements;

namespace LanguageLibrary.Parser
{
    class Parser
    {
        public Lexer.Lexer Lexer { get; private set; }
        private Token CurrentToken { get; set; }
        public Parser(Lexer.Lexer lexer)
        {
            Lexer = lexer;
        }

        public Program Parse()
        {
            Program program = GetProgram();
            return program;
        }

        private Program GetProgram()
        {
            NextToken(TokenType.BEGIN, "There should be begin token!");
            Block block = GetBlock();
            NextToken(TokenType.END, "There should be end token!");
            NextToken(TokenType.DOT, "There should be dot token!");
            //TODO verify end of the source
            return new Program(block);
        }

        private Block GetBlock()
        {
            LinkedList<Statement> statements = GetStatements();
            return new Block(statements);
        }

        private LinkedList<Statement> GetStatements()
        {
            LinkedList<Statement> statements = new LinkedList<Statement>();

            while (CurrentToken.TokenType != TokenType.END)
            {
                statements.AddLast(GetStatement());
            }
            return statements;
        }
        private Statement GetStatement()
        {
            switch (CurrentToken.TokenType)
            {
                case TokenType.IF:
                    return GetIfStatement();
                case TokenType.WHILE:
                    return GetWhileStatement();
                case TokenType.FOR:
                    return GetForStatement();
                case TokenType.IDENTIFIER:
                    return GetSetStatement();
                case TokenType.VAR:
                    return GetVarStatement();
                case TokenType.METHOD:
                    return GetMethodStatement();
            }
            throw new LanguageException("Something wrong in statement!");
        }

        private Statement GetMethodStatement()
        {
            throw new NotImplementedException();
        }

        private Statement GetVarStatement()
        {
            throw new NotImplementedException();
        }

        private Statement GetSetStatement()
        {
            throw new NotImplementedException();
        }

        private Statement GetForStatement()
        {
            throw new NotImplementedException();
        }

        private Statement GetWhileStatement()
        {
            NextToken(TokenType.WHILE, "There should be while token!");
            NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
            Condition condition = GetCondition();
            NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
            NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
            Block block = GetBlock();
            NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
            return new WhileStatement(block, condition);
        }

        private Statement GetIfStatement()
        {
            NextToken(TokenType.IF, "There should be if token!");
            NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
            Condition condition = GetCondition();
            NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
            NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
            Block block = GetBlock();
            NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
            if (CurrentToken.TokenType == TokenType.ELSE)
            {
                NextToken(TokenType.ELSE, "There should be else token!");
                NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
                Block blockElse = GetBlock();
                NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
                ElseStatement elseSt = new ElseStatement(blockElse);
                return new IfStatement(block, condition, elseSt);
            }
            else
            {
                return new IfStatement(block, condition);
            }
        }

        private Condition GetCondition()
        {
            //TODO one expression condition
            Expression left = GetExpression();
            Token tokenRel = CurrentToken;
            NextToken(tokenRel.TokenType, "");
            switch (tokenRel.TokenType)
            {
                case TokenType.EQUAL:
                    return new EqualsRel(left, GetExpression());
                case TokenType.NOT_EQUAL:
                    return new NotEqualRel(left, GetExpression());
                case TokenType.GREATER_THEN:
                    return new GreaterThanRel(left, GetExpression());
                case TokenType.GREATER_EQ_THEN:
                    return new GreaterEqThanRel(left, GetExpression());
                case TokenType.LESS_THEN:
                    return new LessThanRel(left, GetExpression());
                case TokenType.LESS_EQ_THEN:
                    return new LessEqThanRel(left, GetExpression());
            }
            throw new LanguageException("Something wrong in condition!");
        }
        private Expression GetExpression()
        {
            Expression expression = GetTerm();
            while (CurrentToken.TokenType == TokenType.PLUS || CurrentToken.TokenType == TokenType.MINUS)
            {
                if (CurrentToken.TokenType == TokenType.PLUS)
                {
                    NextToken(TokenType.PLUS, "There should be plus token!");
                    expression = new Plus(expression, GetTerm());
                }
                else if (CurrentToken.TokenType == TokenType.MINUS)
                {
                    NextToken(TokenType.MINUS, "There should be minus token!");
                    expression = new Minus(expression, GetTerm());
                }
            }
            return expression;
        }
        private Expression GetTerm()
        {
            Expression expression = GetFactor();
            while (CurrentToken.TokenType == TokenType.MULTIPLY || CurrentToken.TokenType == TokenType.DIVIDE)
            {
                if (CurrentToken.TokenType == TokenType.MULTIPLY)
                {
                    NextToken(TokenType.MULTIPLY, "There should be multiply token!");
                    expression = new Multiply(expression, GetFactor());
                }
                else if (CurrentToken.TokenType == TokenType.DIVIDE)
                {
                    NextToken(TokenType.DIVIDE, "There should be divide token!");
                    expression = new Divide(expression, GetFactor());
                }
            }
            return expression;
        }
        private Expression GetFactor()
        {
            Token token = CurrentToken;
            switch (token.TokenType)
            {
                case TokenType.NUMBER:
                    NextToken(TokenType.NUMBER, "There should be number token!");
                    return new NumberExpression(int.Parse(CurrentToken.Value));
                case TokenType.IDENTIFIER:
                    NextToken(TokenType.IDENTIFIER, "There should be identifier token!");
                    return null;
                case TokenType.STRING:
                    NextToken(TokenType.STRING, "There should be string token!");
                    return new StringExpression(token.Value);
                case TokenType.PLUS:
                    NextToken(TokenType.PLUS, "There should be plus token!");
                    return new PlusUnary(GetFactor());
                case TokenType.MINUS:
                    NextToken(TokenType.PLUS, "There should be minus token!");
                    return new MinusUnary(GetFactor());
                case TokenType.L_ROUND_BRACKET:
                    NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
                    Expression expression = GetExpression();
                    NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
                    return expression;
            }
            throw new LanguageException("Something wrong in factor!");
        }

        private void NextToken(TokenType type, string message)
        {
            if (CurrentToken.TokenType == type)
            {
                CurrentToken = Lexer.GetNextToken();
            }
            else
            {
                throw new LanguageException(message);
            }
        }
    }
}
