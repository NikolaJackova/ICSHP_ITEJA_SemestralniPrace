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
    class InterpretEngine : IVisitor
    {
        public Parser.Parser Parser { get; private set; }
        private Stack<ExecutionContext> ExecutionContexts { get; set; }

        public InterpretEngine(Parser.Parser parser)
        {
            Parser = parser;
            ExecutionContexts = new Stack<ExecutionContext>();
        }
        
        public void Interpret()
        {
            ExecutionContexts.Push(new ExecutionContext());
            Program program = Parser.Parse();
            program.Accept(this);
        }
        public object VisitProgram(Program program)
        {
            return program.Block.Accept(this);
        }
        public object VisitBlock(Block block)
        {
            //Creating new execution context for block
            ExecutionContexts.Push(new ExecutionContext());
            foreach (var variable in block.Variables)
            {
                variable.Accept(this);
            }
            foreach (var statement in block.Statements)
            {
                statement.Accept(this);
            }
            //Removing block's execution contexts from stack
            ExecutionContexts.Pop();
            return null;
        }

        public object VisitForStatement(ForStatement statement)
        {
            throw new NotImplementedException();
        }

        public object VisitVariable(Variable variable)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.ExistsVariable(variable.Var.Identifier))
                {
                    throw new InterpretException("Variable: " + variable.Var.Identifier + " already exists!");
                }
            }
            ExecutionContext actualContext = ExecutionContexts.Peek();
            actualContext.DeclareVariable(variable.Var.Identifier);
            return null;
        }

        #region STATEMENT
        public object VisitSetStatement(SetStatement statement)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.ExistsVariable(statement.Identifier.Identifier))
                {
                    context.SetVariable(statement.Identifier.Identifier, statement.Expression.Accept(this));
                    return null;
                }
            }
            throw new InterpretException("Variable: " + statement.Identifier.Identifier + " was not declared!");
        }
        public object VisitIfStatement(IfStatement statement)
        {
            if ((bool)statement.Condition.Accept(this))
            {
                foreach (var block in statement.Blocks)
                {
                    block.Accept(this);
                }
            }
            else
            {
                if (statement.ElseStatement != null)
                {
                    VisitElseStatement(statement.ElseStatement);
                }
            }
            return null;
        }
        public object VisitElseStatement(ElseStatement statement)
        {
            foreach (var block in statement.Blocks)
            {
                block.Accept(this);
            }
            return null;
        }
        public object VisitWhileStatement(WhileStatement statement)
        {
            while ((bool)statement.Condition.Accept(this))
            {
                foreach (var block in statement.Blocks)
                {
                    block.Accept(this);
                }
            }
            return null;
        }
        #endregion STATEMENT

        #region CONDITION
        public object VisitGreaterEqThanRel(GreaterEqThanRel condition)
        {
            return (double)condition.Left.Accept(this) >= (double)condition.Right.Accept(this);
        }
        public object VisitGreaterThanRel(GreaterThanRel condition)
        {
            return (double)condition.Left.Accept(this) > (double)condition.Right.Accept(this);
        }
        public object VisitLessEqThanRel(LessEqThanRel condition)
        {
            return (double)condition.Left.Accept(this) <= (double)condition.Right.Accept(this);
        }
        public object VisitLessThanRel(LessThanRel condition)
        {
            return (double)condition.Left.Accept(this) < (double)condition.Right.Accept(this);
        }
        public object VisitEqualsRel(EqualsRel condition)
        {
            return (double)condition.Left.Accept(this) == (double)condition.Right.Accept(this);
        }
        public object VisitNotEqualRel(NotEqualRel condition)
        {
            return (double)condition.Left.Accept(this) != (double)condition.Right.Accept(this);
        }
        public object VisitOneStatement(OneStatementCondition condition)
        {
            return condition.Left.Accept(this);
        }
        #endregion CONDITION

        #region EXPRESSION
        public object VisitIdentExpression(IdentExpression expression)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.ExistsVariable(expression.Identifier))
                {
                    return context.GetVariable(expression.Identifier);
                }
            }
            throw new InterpretException("Variable: " + expression.Identifier + " does not exists!");
        }
        public object VisitStringExpression(StringExpression expression)
        {
            return expression.Text;
        }
        public object VisitNumberExpression(NumberExpression expression)
        {
            return expression.Value;
        }
        public object VisitPlusUnary(PlusUnary expression)
        {
            return +(double)expression.Expression.Accept(this);
        }
        public object VisitMinusUnary(MinusUnary expression)
        {
            return -(double)expression.Expression.Accept(this);
        }
        #region BINARY_EXPRESSION
        public object VisitPlus(Plus expression)
        {
            return (double)expression.Left.Accept(this) + (double)expression.Right.Accept(this);
        }
        public object VisitMinus(Minus expression)
        {
            return (double)expression.Left.Accept(this) - (double)expression.Right.Accept(this);
        }
        public object VisitMultiply(Multiply expression)
        {
            return (double)expression.Left.Accept(this) * (double)expression.Right.Accept(this);
        }
        public object VisitDivide(Divide expression)
        {
            return (double)expression.Left.Accept(this) / (double)expression.Right.Accept(this);
        }
        #endregion BINARY_EXPRESSION
        #endregion EXPRESSION
    }
}
