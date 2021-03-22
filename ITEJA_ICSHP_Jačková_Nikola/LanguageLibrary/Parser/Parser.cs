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
using LanguageLibrary.Parser.Variables;

namespace LanguageLibrary.Parser
{
    public class Parser
    {
        public Lexer.Lexer Lexer { get; private set; }
        private Token CurrentToken { get; set; }

        public Parser(Lexer.Lexer lexer)
        {
            Lexer = lexer;
            CurrentToken = Lexer.GetNextToken();
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

            return new Program(block);
        }

        private Block GetBlock()
        {
            LinkedList<Variable> declarations = GetDeclarations();
            LinkedList<IStatement> statements = GetStatements();
            return new Block(statements, declarations);
        }

        private LinkedList<Variable> GetDeclarations()
        {
            LinkedList<Variable> declarations = new LinkedList<Variable>();
            if (CurrentToken.TokenType == TokenType.VAR)
            {
                NextToken(TokenType.VAR, "There should be var token!");
                while (true)
                {
                    Token token = CurrentToken;
                    NextToken(TokenType.IDENTIFIER, "There should be ident token!");
                    Variable variable = new Variable(new IdentExpression(token.Value));
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
                        throw new LanguageException("Invalid token in declaration!");
                    }
                }
            }
            return declarations;
        }

        private LinkedList<IStatement> GetStatements()
        {
            LinkedList<IStatement> statements = new LinkedList<IStatement>();

            while (CurrentToken.TokenType != TokenType.END && CurrentToken.TokenType != TokenType.R_CURLY_BRACKET)
            {
                statements.AddLast(GetStatement());
            }
            return statements;
        }
        private IStatement GetStatement()
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
                    throw new NotSupportedException("Not supported yet!");
            }
            throw new LanguageException("Statement does not return value!");
        }

        private IStatement GetMethodStatement()
        {
            NextToken(TokenType.METHOD, "There should be method token!");
            IdentExpression identifier = new IdentExpression(CurrentToken.Value);
            NextToken(TokenType.IDENTIFIER, "There should be ident token!");
            LinkedList<IExpression> parameters = GetParameters();
            return new MethodStatement(identifier, parameters);
        }

        private LinkedList<IExpression> GetParameters()
        {
            LinkedList<IExpression> paramList = new LinkedList<IExpression>();
            NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
            while (CurrentToken.TokenType != TokenType.R_ROUND_BRACKET)
            {
                if (TokenType.NUMBER == CurrentToken.TokenType || TokenType.IDENTIFIER == CurrentToken.TokenType || TokenType.L_ROUND_BRACKET == CurrentToken.TokenType)
                {
                    IExpression expression = GetExpression();
                    paramList.AddLast(expression);
                    
                }
                else if (TokenType.QUOTE == CurrentToken.TokenType)
                {
                    NextToken(TokenType.QUOTE, "There should be quote token!");
                    IExpression expression = new StringExpression(CurrentToken.Value);
                    NextToken(TokenType.STRING);
                    NextToken(TokenType.QUOTE, "There should be quote token!");
                    paramList.AddLast(expression);
                }
            }
            return paramList;
        }

        private IStatement GetSetStatement()
        {
            IdentExpression identifier = new IdentExpression(CurrentToken.Value);
            NextToken(TokenType.IDENTIFIER, "There should be ident token!");
            NextToken(TokenType.ASSIGN, "There shold be assign token!");
            if (TokenType.NUMBER == CurrentToken.TokenType || TokenType.IDENTIFIER == CurrentToken.TokenType || TokenType.L_ROUND_BRACKET == CurrentToken.TokenType)
            {
                IExpression expression = GetExpression();
                NextToken(TokenType.SEMICOLON, "There should be semicolon token!");
                return new SetStatement(identifier, expression);
            }
            else if (TokenType.QUOTE == CurrentToken.TokenType)
            {
                NextToken(TokenType.QUOTE, "There should be quote token!");
                IExpression expression = new StringExpression(CurrentToken.Value);
                NextToken(TokenType.STRING);
                NextToken(TokenType.QUOTE, "There should be quote token!");
                NextToken(TokenType.SEMICOLON, "There should be semicolon token!");
                return new SetStatement(identifier, expression);
            }
            throw new LanguageException("Set statement does not return value!");
        }

        private IStatement GetForStatement()
        {
            NextToken(TokenType.FOR, "There should be for token!");
            IdentExpression ident = new IdentExpression(CurrentToken.Value);
            NextToken(TokenType.IDENTIFIER, "There should be ident token!");
            NextToken(TokenType.COMMA, "There should be comma token!");
            NextToken(TokenType.FROM, "There should be from token!");
            IExpression from = GetExpression();
            NextToken(TokenType.TO, "There should be to token!");
            IExpression to = GetExpression();
            NextToken(TokenType.COMMA, "There should be comma token!");
            IStatement statement = GetSetStatement();
            NextToken(TokenType.L_CURLY_BRACKET, "There should be left curly bracket!");
            LinkedList<Block> blocks = new LinkedList<Block>();
            do
            {
                blocks.AddLast(GetBlock());
            } while (CurrentToken.TokenType == TokenType.VAR);
            NextToken(TokenType.R_CURLY_BRACKET, "There should be right curly bracket!");
            return new ForStatement(ident, from, to, statement, blocks);
        }

        private IStatement GetWhileStatement()
        {
            NextToken(TokenType.WHILE, "There should be while token!");
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

        private IStatement GetIfStatement()
        {
            NextToken(TokenType.IF, "There should be if token!");
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
                NextToken(TokenType.ELSE, "There should be else token!");
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
            IExpression left = GetExpression();
            Token tokenRel = CurrentToken;
            if (tokenRel.TokenType != TokenType.EQUAL && tokenRel.TokenType != TokenType.NOT_EQUAL && tokenRel.TokenType != TokenType.GREATER_THEN
                && tokenRel.TokenType != TokenType.GREATER_EQ_THEN && tokenRel.TokenType != TokenType.LESS_THEN && tokenRel.TokenType != TokenType.LESS_EQ_THEN)
            {
                return new OneStatementCondition(left);
            }
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
            throw new LanguageException("Condition does not return a value!");
        }
        private IExpression GetExpression()
        {
            IExpression expression = GetTerm();
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
        private IExpression GetTerm()
        {
            IExpression expression = GetFactor();
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
        private IExpression GetFactor()
        {
            Token token = CurrentToken;
            switch (token.TokenType)
            {
                case TokenType.NUMBER:
                    NextToken(TokenType.NUMBER, "There should be number token!");
                    return new NumberExpression(int.Parse(token.Value));
                case TokenType.IDENTIFIER:
                    NextToken(TokenType.IDENTIFIER, "There should be identifier token!");
                    return new IdentExpression(token.Value);
                case TokenType.PLUS:
                    NextToken(TokenType.PLUS, "There should be plus token!");
                    return new PlusUnary(GetFactor());
                case TokenType.MINUS:
                    NextToken(TokenType.PLUS, "There should be minus token!");
                    return new MinusUnary(GetFactor());
                case TokenType.L_ROUND_BRACKET:
                    NextToken(TokenType.L_ROUND_BRACKET, "There should be left round bracket!");
                    IExpression expression = GetExpression();
                    NextToken(TokenType.R_ROUND_BRACKET, "There should be right round bracket!");
                    return expression;
            }
            throw new LanguageException("Factor does not return a value!");
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

        private void NextToken(TokenType type)
        {
            if (CurrentToken.TokenType == type)
            {
                CurrentToken = Lexer.GetNextToken();
            }
            else
            {
                throw new LanguageException("TokenType does not match!");
            }
        }
    }
}
