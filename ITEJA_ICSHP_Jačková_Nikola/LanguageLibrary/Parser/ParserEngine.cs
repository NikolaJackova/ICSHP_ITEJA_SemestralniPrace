using LanguageLibrary.Exceptions;
using LanguageLibrary.Lexer.Tokens;
using LanguageLibrary.Parser.Conditions;
using LanguageLibrary.Parser.Expressions;
using LanguageLibrary.Parser.Statements;
using LanguageLibrary.Parser.Variables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    class ParserEngine
    {
        public Lexer.Lexer Lexer { get; private set; }
        private Token CurrentToken { get; set; }
        public ParserEngine(Lexer.Lexer lexer)
        {
            Lexer = lexer;
            CurrentToken = Lexer.GetNextToken();
        }

        public Program GetProgram()
        {
            NextToken(TokenType.BEGIN, "There should be begin token!");

            Block block = GetBlock();

            NextToken(TokenType.END, "There should be end token!");
            NextToken(TokenType.DOT, "There should be dot token!");

            return new Program(block);
        }

        public void ResetParser()
        {
            Lexer.ResetEnumerator();
            CurrentToken = Lexer.GetNextToken();
        }

        private Block GetBlock()
        {
            LinkedList<VariableDeclaration> declarations = GetDeclarations();
            LinkedList<Statement> statements = GetStatements();
            return new Block(statements, declarations);
        }

        private LinkedList<VariableDeclaration> GetDeclarations()
        {
            LinkedList<VariableDeclaration> declarations = new LinkedList<VariableDeclaration>();
            if (CurrentToken.TokenType == TokenType.VAR)
            {
                NextToken(TokenType.VAR);
                while (true)
                {
                    VariableDeclaration variable = new VariableDeclaration(new IdentExpression(CurrentToken.Value));
                    NextToken(TokenType.IDENTIFIER, "There should be ident token!");
                    declarations.AddLast(variable);
                    if (CurrentToken.TokenType == TokenType.COMMA)
                    {
                        NextToken(TokenType.COMMA);
                    }
                    else if (CurrentToken.TokenType == TokenType.SEMICOLON)
                    {
                        NextToken(TokenType.SEMICOLON);
                        break;
                    }
                    else
                    {
                        throw new ParserException("Invalid token in declaration!\nToken: " + CurrentToken.ToString() + ".");
                    }
                }
            }
            return declarations;
        }

        private LinkedList<Statement> GetStatements()
        {
            LinkedList<Statement> statements = new LinkedList<Statement>();

            while (CurrentToken.TokenType != TokenType.END && CurrentToken.TokenType != TokenType.R_CURLY_BRACKET)
            {
                Statement statement = GetStatement();
                if (statement != null)
                {
                    statements.AddLast(statement);
                }
                else
                {
                    break;
                }
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
                case TokenType.METHOD:
                    return GetMethodStatement();
                case TokenType.VAR:
                    return null;
            }
            throw new ParserException("Statement does not return value!\nToken: " + CurrentToken.ToString() + ".");
        }

        private Statement GetMethodStatement()
        {
            NextToken(TokenType.METHOD);
            IdentExpression identifier = new IdentExpression(CurrentToken.Value);
            NextToken(TokenType.IDENTIFIER, "There should be ident token!");
            LinkedList<Expression> parameters = GetParameters();
            NextToken(TokenType.SEMICOLON, "There should be semicolon!");
            switch (identifier.Identifier)
            {
                case "Print":
                    return new PrintMethod(parameters);
                case "":
                    break;
            }
            //return new MethodStatement(identifier, parameters);
            return null;
        }

        private LinkedList<Expression> GetParameters()
        {
            LinkedList<Expression> paramList = new LinkedList<Expression>();
            NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
            while (CurrentToken.TokenType != TokenType.R_ROUND_BRACKET)
            {
                if (TokenType.NUMBER == CurrentToken.TokenType || TokenType.IDENTIFIER == CurrentToken.TokenType || TokenType.L_ROUND_BRACKET == CurrentToken.TokenType)
                {
                    Expression expression = GetExpression();
                    paramList.AddLast(expression);

                }
                else if (TokenType.QUOTE == CurrentToken.TokenType)
                {
                    NextToken(TokenType.QUOTE);
                    Expression expression = new StringExpression(CurrentToken.Value);
                    NextToken(TokenType.STRING);
                    NextToken(TokenType.QUOTE, "There should be quote token!");
                    paramList.AddLast(expression);
                }
                if (CurrentToken.TokenType == TokenType.COMMA)
                {
                    NextToken(TokenType.COMMA);
                }
            }
            NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
            return paramList;
        }

        private Statement GetSetStatement()
        {
            IdentExpression identifier = new IdentExpression(CurrentToken.Value);
            NextToken(TokenType.IDENTIFIER, "There should be ident token!");
            NextToken(TokenType.ASSIGN, "There shold be assign token!");
            if (TokenType.NUMBER == CurrentToken.TokenType || TokenType.IDENTIFIER == CurrentToken.TokenType || TokenType.L_ROUND_BRACKET == CurrentToken.TokenType)
            {
                Expression expression = GetExpression();
                NextToken(TokenType.SEMICOLON, "There should be semicolon token!");
                return new SetStatement(identifier, expression);
            }
            else if (TokenType.QUOTE == CurrentToken.TokenType)
            {
                NextToken(TokenType.QUOTE);
                Expression expression = new StringExpression(CurrentToken.Value);
                NextToken(TokenType.STRING);
                NextToken(TokenType.QUOTE, "There should be quote token!");
                NextToken(TokenType.SEMICOLON, "There should be semicolon token!");
                return new SetStatement(identifier, expression);
            }
            throw new ParserException("Set statement does not return value!\nToken: " + CurrentToken.ToString());
        }

        private Statement GetForStatement()
        {
            NextToken(TokenType.FOR);
            IdentExpression ident = new IdentExpression(CurrentToken.Value);
            NextToken(TokenType.IDENTIFIER, "There should be ident token!");
            NextToken(TokenType.COMMA, "There should be comma token!");
            NextToken(TokenType.FROM, "There should be from token!");
            Expression from = GetExpression();
            NextToken(TokenType.TO, "There should be to token!");
            Expression to = GetExpression();
            NextToken(TokenType.COMMA, "There should be comma token!");
            SetStatement statement = (SetStatement)GetSetStatement();
            NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
            LinkedList<Block> blocks = new LinkedList<Block>();
            do
            {
                blocks.AddLast(GetBlock());
            } while (CurrentToken.TokenType == TokenType.VAR);
            NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
            return new ForStatement(ident, from, to, statement, blocks);
        }

        private Statement GetWhileStatement()
        {
            NextToken(TokenType.WHILE);
            NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
            Condition condition = GetCondition();
            NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
            NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
            LinkedList<Block> blocks = new LinkedList<Block>();
            do
            {
                blocks.AddLast(GetBlock());
            } while (CurrentToken.TokenType == TokenType.VAR);
            NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
            return new WhileStatement(blocks, condition);
        }

        private Statement GetIfStatement()
        {
            NextToken(TokenType.IF);
            NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
            Condition condition = GetCondition();
            NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
            NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
            LinkedList<Block> blocks = new LinkedList<Block>();
            do
            {
                blocks.AddLast(GetBlock());
            } while (CurrentToken.TokenType == TokenType.VAR);
            NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
            if (CurrentToken.TokenType == TokenType.ELSE)
            {
                NextToken(TokenType.ELSE);
                NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
                LinkedList<Block> elseBlocks = new LinkedList<Block>();
                do
                {
                    elseBlocks.AddLast(GetBlock());
                } while (CurrentToken.TokenType == TokenType.VAR);
                NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
                ElseStatement elseSt = new ElseStatement(elseBlocks);
                return new IfStatement(blocks, condition, elseSt);
            }
            else
            {
                return new IfStatement(blocks, condition);
            }
        }

        private Condition GetCondition()
        {
            Expression left = GetExpression();
            Token tokenRel = CurrentToken;
            if (tokenRel.TokenType != TokenType.EQUAL && tokenRel.TokenType != TokenType.NOT_EQUAL && tokenRel.TokenType != TokenType.GREATER_THEN
                && tokenRel.TokenType != TokenType.GREATER_EQ_THEN && tokenRel.TokenType != TokenType.LESS_THEN && tokenRel.TokenType != TokenType.LESS_EQ_THEN)
            {
                return new OneStatementCondition(left);
            }
            NextToken(tokenRel.TokenType);
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
            throw new ParserException("Condition does not return a value!\nToken: " + tokenRel.ToString() + ".");
        }
        private Expression GetExpression()
        {
            Expression expression = GetTerm();
            while (CurrentToken.TokenType == TokenType.PLUS || CurrentToken.TokenType == TokenType.MINUS)
            {
                if (CurrentToken.TokenType == TokenType.PLUS)
                {
                    NextToken(TokenType.PLUS);
                    expression = new Plus(expression, GetTerm());
                }
                else if (CurrentToken.TokenType == TokenType.MINUS)
                {
                    NextToken(TokenType.MINUS);
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
                    NextToken(TokenType.MULTIPLY);
                    expression = new Multiply(expression, GetFactor());
                }
                else if (CurrentToken.TokenType == TokenType.DIVIDE)
                {
                    NextToken(TokenType.DIVIDE);
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
                    NextToken(TokenType.NUMBER);
                    return new NumberExpression(double.Parse(token.Value.Replace(".",",")));
                case TokenType.IDENTIFIER:
                    NextToken(TokenType.IDENTIFIER);
                    return new IdentExpression(token.Value);
                case TokenType.QUOTE:
                    NextToken(TokenType.QUOTE);
                    token = CurrentToken;
                    NextToken(TokenType.STRING);
                    NextToken(TokenType.QUOTE);
                    return new StringExpression(token.Value);
                case TokenType.PLUS:
                    NextToken(TokenType.PLUS);
                    return new PlusUnary(GetFactor());
                case TokenType.MINUS:
                    NextToken(TokenType.MINUS);
                    return new MinusUnary(GetFactor());
                case TokenType.L_ROUND_BRACKET:
                    NextToken(TokenType.L_ROUND_BRACKET);
                    Expression expression = GetExpression();
                    NextToken(TokenType.R_ROUND_BRACKET);
                    return expression;
            }
            throw new ParserException("Factor does not return a value!\nToken: " + token.ToString() + ".");
        }

        private void NextToken(TokenType type, string message)
        {
            if (CurrentToken.TokenType == type)
            {
                CurrentToken = Lexer.GetNextToken();
            }
            else
            {
                throw new ParserException(message + "\nToken: " + CurrentToken.ToString() + ".");
            }
        }

        private void NextToken(TokenType type)
        {
            if (CurrentToken.TokenType == type)
            {
                CurrentToken = Lexer.GetNextToken();
            }
            else
            {
                throw new ParserException("TokenType does not match!\nToken: " + CurrentToken.ToString() + ".");
            }
        }
    }
}
