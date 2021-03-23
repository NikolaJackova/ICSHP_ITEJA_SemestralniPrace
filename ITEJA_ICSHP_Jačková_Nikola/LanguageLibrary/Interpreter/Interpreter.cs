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
    class Interpreter : IVisitor
    {
        public Parser.Parser Parser { get; private set; }

        public Stack<ExecutionContext<IASTItem>> ExecutionContexts { get; private set; }

        public Interpreter(Parser.Parser parser)
        {
            Parser = parser;
            ExecutionContexts = new Stack<ExecutionContext<IASTItem>>();
        }

        public void Interpret()
        {
            ExecutionContexts.Push(new ExecutionContext<IASTItem>());
            Program program = Parser.Parse();
            program.Accept(this);
        }
        public object Visit_Block(Block block)
        {
            throw new NotImplementedException();
        }

        public object Visit_ElseStatement(ElseStatement statement)
        {
            throw new NotImplementedException();
        }

        public object Visit_ForStatement(ForStatement statement)
        {
            throw new NotImplementedException();
        }

        public object Visit_IfStatement(IfStatement statement)
        {
            if ((bool)statement.Condition.Accept(this)) {
                foreach (var block in statement.Blocks)
                {
                    block.Accept(this);
                }
            }
            return null;
        }

        public object Visit_Program(Program program)
        {
            throw new NotImplementedException();
        }
        public object Visit_SetStatement(SetStatement statement)
        {
            throw new NotImplementedException();
        }
        public object Visit_Variable(Variable variable)
        {
            throw new NotImplementedException();
        }
        public object Visit_WhileStatement(WhileStatement statement)
        {
            while ((bool)statement.Condition.Accept(this)) {
                foreach (var block in statement.Blocks)
                {
                    block.Accept(this);
                }
            }
            return null;
        }
        #region CONDITION
        public object Visit_GreaterEqThanRel(GreaterEqThanRel condition)
        {
            return (double)condition.Left.Accept(this) >= (double)condition.Right.Accept(this);
        }
        public object Visit_GreaterThanRel(GreaterThanRel condition)
        {
            return (double)condition.Left.Accept(this) > (double)condition.Right.Accept(this);
        }
        public object Visit_LessEqThanRel(LessEqThanRel condition)
        {
            return (double)condition.Left.Accept(this) <= (double)condition.Right.Accept(this);
        }
        public object Visit_LessThanRel(LessThanRel condition)
        {
            return (double)condition.Left.Accept(this) < (double)condition.Right.Accept(this);
        }
        public object Visit_EqualsRel(EqualsRel condition)
        {
            return (double)condition.Left.Accept(this) == (double)condition.Right.Accept(this);
        }
        public object Visit_NotEqualRel(NotEqualRel condition)
        {
            return (double)condition.Left.Accept(this) != (double)condition.Right.Accept(this);
        }
        public object Visit_OneStatement(OneStatementCondition condition)
        {
            return condition.Left.Accept(this);
        }
        #endregion CONDITION

        #region EXPRESSION
        public object Visit_IdentExpression(IdentExpression expression)
        {
            return expression.Identifier;
        }
        public object Visit_StringExpression(StringExpression expression)
        {
            return expression.Text;
        }
        public object Visit_NumberExpression(NumberExpression expression)
        {
            return expression.Value;
        }
        public object Visit_PlusUnary(PlusUnary expression)
        {
            return +(double)expression.Expression.Accept(this);
        }
        public object Visit_MinusUnary(MinusUnary expression)
        {
            return -(double)expression.Expression.Accept(this);
        }
        #region BINARY_EXPRESSION
        public object Visit_Plus(Plus expression)
        {
            return (double)expression.Left.Accept(this) + (double)expression.Right.Accept(this);
        }
        public object Visit_Minus(Minus expression)
        {
            return (double)expression.Left.Accept(this) - (double)expression.Right.Accept(this);
        }
        public object Visit_Multiply(Multiply expression)
        {
            return (double)expression.Left.Accept(this) * (double)expression.Right.Accept(this);
        }
        public object Visit_Divide(Divide expression)
        {
            return (double)expression.Left.Accept(this) / (double)expression.Right.Accept(this);
        }
        #endregion BINARY_EXPRESSION
        #endregion EXPRESSION
    }
}
