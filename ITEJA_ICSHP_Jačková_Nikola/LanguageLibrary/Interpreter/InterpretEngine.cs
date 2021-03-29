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
        public object VisitPrintMethod(PrintMethod method)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var parameter in method.Parameters)
            {
                builder.Append(parameter.Accept(this));
            }
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }
        public object VisitForStatement(ForStatement statement)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.Vars.ExistsVariable(statement.Identifier.Identifier))
                {
                    context.Vars.SetVariable(statement.Identifier.Identifier, new Variable(VarType.NUMBER, statement.From.Accept(this)));
                }
            }
            for (int i = Convert.ToInt32(statement.From.Accept(this)); i <= Convert.ToInt32(statement.To.Accept(this)); i = Convert.ToInt32(VisitIdentExpression(statement.Statement.Identifier)))
            {
                foreach (var block in statement.Blocks)
                {
                    block.Accept(this);
                }
                statement.Statement.Accept(this);
            }
            return null;
        }

        public object VisitVariable(VariableDeclaration variable)
        {
            //TODO implement VariableType
            foreach (var context in ExecutionContexts)
            {
                if (context.Vars.ExistsVariable(variable.Var.Identifier))
                {
                    throw new InterpretException("Variable: " + variable.Var.Identifier + " already exists!");
                }
            }
            ExecutionContext actualContext = ExecutionContexts.Peek();
            actualContext.Vars.DeclareVariable(variable.Var.Identifier);
            return null;
        }

        #region STATEMENT
        public object VisitSetStatement(SetStatement statement)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.Vars.ExistsVariable(statement.Identifier.Identifier))
                {
                    if (statement.Expression.Accept(this) is string)
                    {
                        context.Vars.SetVariable(statement.Identifier.Identifier, new Variable(VarType.STRING, statement.Expression.Accept(this)));
                    } else if (statement.Expression.Accept(this) is double)
                    {
                        context.Vars.SetVariable(statement.Identifier.Identifier, new Variable(VarType.NUMBER, statement.Expression.Accept(this)));
                    }
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
                if (context.Vars.ExistsVariable(expression.Identifier))
                {
                    return context.Vars.GetVariable(expression.Identifier);
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
