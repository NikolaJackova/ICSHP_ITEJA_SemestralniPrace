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

    public delegate void PrintMethodDelegate(string text);
    public delegate void ForwardMethodDelegate(double distance);
    public delegate void BackwardMethodDelegate(double distance);
    public delegate void RotateMethodDelegate(double angle);
    public delegate void ChangePenDelegate(string color, double width);
    public delegate void PenVisibileDelegate(double visibility);

    class InterpretEngine : IVisitor
    {
        public PrintMethodDelegate Print { get; set; }
        public ForwardMethodDelegate Forward { get; set; }
        public RotateMethodDelegate Rotate { get; set; }
        public BackwardMethodDelegate Backward { get; set; }
        public PenVisibileDelegate PenVisible { get; set; }
        public ChangePenDelegate ChangePen { get; set; }
        private Parser.Parser Parser { get; set; }
        private Stack<ExecutionContext> ExecutionContexts { get; set; }

        public InterpretEngine(Parser.Parser parser)
        {
            Parser = parser;
            ExecutionContexts = new Stack<ExecutionContext>();
        }

        public void Interpret()
        {
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
        public object VisitVariable(VariableDeclaration variable)
        {
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
                    Variable variable = context.Vars.GetVariable(statement.Identifier.Identifier);
                    object obj = statement.Expression.Accept(this);
                    if (obj is string)
                    {
                        if (variable?.Type == VarType.STRING || variable == null)
                        {
                            context.Vars.SetVariable(statement.Identifier.Identifier,
                                new Variable(VarType.STRING, statement.Expression.Accept(this), statement.Identifier));
                        }
                        else
                        {
                            throw new InterpretException("You are trying to cast wrong value into number variable!");
                        }
                    }
                    else if (obj is double)
                    {
                        if (variable?.Type == VarType.NUMBER || variable == null)
                        {
                            context.Vars.SetVariable(statement.Identifier.Identifier,
                                new Variable(VarType.NUMBER, statement.Expression.Accept(this), statement.Identifier));
                        }
                        else
                        {
                            throw new InterpretException("You are trying to cast wrong value into string variable!");
                        }
                    }
                    return null;
                }
            }
            throw new InterpretException("Variable: " + statement.Identifier.Identifier + " was not declared!");
        }
        public object VisitForStatement(ForStatement statement)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.Vars.ExistsVariable(statement.Identifier.Identifier))
                {
                    context.Vars.SetVariable(statement.Identifier.Identifier, new Variable(VarType.NUMBER, Math.Round(Convert.ToDouble(statement.From.Accept(this)), 0), statement.Identifier));
                    break;
                }
            }
            for (int i = Convert.ToInt32(statement.From.Accept(this)); i <= Convert.ToInt32(statement.To.Accept(this)); i =
                Convert.ToInt32(VisitIdentExpression(statement.Statement.Identifier)))
            {
                foreach (var block in statement.Blocks)
                {
                    block.Accept(this);
                }
                //Assigning typically i variable to value in statement
                statement.Statement.Accept(this);
            }
            return null;
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
        #region METHOD
        public object VisitPrintMethod(PrintMethod method)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var parameter in method.Parameters)
            {
                builder.Append(parameter.Accept(this));
            }
            Print(builder.ToString());
            Console.WriteLine(builder.ToString());
            return null;
        }
        public object VisitForwardMethod(ForwardMethod method)
        {
            if (method.Parameters.Count == 1)
            {
                object obj = method.Parameters.ElementAt(0).Accept(this);
                if (obj is double @double)
                {
                    Forward(@double);
                }
                else
                {
                    throw new InterpretException("Only number expressions are allowed!");

                }
                return null;
            }
            throw new InterpretException("There is too much parameters in forward method! Only 1 is allowed!");
        }

        public object VisitRotateMethod(RotateMethod method)
        {
            if (method.Parameters.Count <= 1)
            {
                object obj = method.Parameters.ElementAt(0).Accept(this);
                if (obj is double @double)
                {
                    Rotate(@double);
                }
                else
                {
                    throw new InterpretException("Only number expressions are allowed!");
                }

                return null;
            }
            throw new InterpretException("There is too much parameters in rotate method! Only 1 is allowed!");
        }

        public object VisitBackwardMethod(BackwardMethod method)
        {
            if (method.Parameters.Count == 1)
            {
                object obj = method.Parameters.ElementAt(0).Accept(this);
                if (obj is double @double)
                {
                    Backward(@double);
                }
                else
                {
                    throw new InterpretException("Only number expressions are allowed!");

                }
                return null;
            }
            throw new InterpretException("There is too much parameters in backward method! Only 1 is allowed!");
        }
        public object VisitChangePenMethod(ChangePenMethod method)
        {
            if (method.Parameters.Count == 2)
            {
                object obj = method.Parameters.ElementAt(0).Accept(this);
                if (obj is string @string)
                {
                    object obj2 = method.Parameters.ElementAt(1).Accept(this);
                    if (obj2 is double @double)
                    {
                        ChangePen(@string, @double);
                    }
                }
                else
                {
                    throw new InterpretException("Only string expressions are allowed!");

                }
                return null;
            }
            throw new InterpretException("There is too much parameters in backward method! Only 2 are allowed!");
        }
        public object VisitPenVisibleMethod(PenVisibileMethod method)
        {
            if (method.Parameters.Count == 1)
            {
                object obj = method.Parameters.ElementAt(0).Accept(this);
                if (obj is double @double)
                {
                    PenVisible(@double);
                }
                else
                {
                    throw new InterpretException("Only number expression are allowed!");

                }
                return null;
            }
            throw new InterpretException("There is to much parameters in visibility method! Only 1 is allowed!");
        }
        #endregion METHOD
        #endregion STATEMENT

        #region CONDITION
        public object VisitGreaterEqThanRel(GreaterEqThanRel condition)
        {
            if (!IsBinaryRelConditionValid(condition, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return ((string)condition.Left.Accept(this)).Length >= ((string)condition.Right.Accept(this)).Length;
            }
            else if (message == "number")
            {
                return (double)condition.Left.Accept(this) >= (double)condition.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Greater Or Equal Than Relation!");
        }
        public object VisitGreaterThanRel(GreaterThanRel condition)
        {
            if (!IsBinaryRelConditionValid(condition, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return ((string)condition.Left.Accept(this)).Length > ((string)condition.Right.Accept(this)).Length;
            }
            else if (message == "number")
            {
                return (double)condition.Left.Accept(this) > (double)condition.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Greater Than Relation!");
        }
        public object VisitLessEqThanRel(LessEqThanRel condition)
        {
            if (!IsBinaryRelConditionValid(condition, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return ((string)condition.Left.Accept(this)).Length <= ((string)condition.Right.Accept(this)).Length;
            }
            else if (message == "number")
            {
                return (double)condition.Left.Accept(this) <= (double)condition.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Less Or Equal Than Relation!");
        }
        public object VisitLessThanRel(LessThanRel condition)
        {
            if (!IsBinaryRelConditionValid(condition, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return ((string)condition.Left.Accept(this)).Length < ((string)condition.Right.Accept(this)).Length;
            }
            else if (message == "number")
            {
                return (double)condition.Left.Accept(this) < (double)condition.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Less Than Relation!");
        }
        public object VisitEqualsRel(EqualsRel condition)
        {
            if (!IsBinaryRelConditionValid(condition, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return (string)condition.Left.Accept(this) == (string)condition.Right.Accept(this);
            }
            else if (message == "number")
            {
                return (double)condition.Left.Accept(this) == (double)condition.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Equal Relation!");
        }
        public object VisitNotEqualRel(NotEqualRel condition)
        {
            if (!IsBinaryRelConditionValid(condition, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return (string)condition.Left.Accept(this) != (string)condition.Right.Accept(this);
            }
            else if (message == "number")
            {
                return (double)condition.Left.Accept(this) != (double)condition.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Not Equal Relation!");
        }
        public object VisitOneStatement(OneStatementCondition condition)
        {
            if (condition.Left is StringExpression || condition.Left.Accept(this) is string)
            {
                throw new InterpretException("String cannot be as a condition in one statement condition!");
            }
            double obj = (double)condition.Left.Accept(this);
            if (obj > 0)
            {
                return true;
            }
            return false;
        }
        #endregion CONDITION

        #region EXPRESSION
        public object VisitIdentExpression(IdentExpression expression)
        {
            foreach (var context in ExecutionContexts)
            {
                if (context.Vars.ExistsVariable(expression.Identifier))
                {
                    return context.Vars.GetVariable(expression.Identifier).Value;
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
            if (!IsBinaryExpressionValid(expression, out string message))
            {
                throw new InterpretException(message);
            }
            if (message == "string")
            {
                return String.Concat((string)expression.Left.Accept(this), (string)expression.Right.Accept(this));
            }
            else if (message == "number")
            {
                return (double)expression.Left.Accept(this) + (double)expression.Right.Accept(this);
            }
            throw new InterpretException("Unexprected exception in Plus Relation!");
        }
        public object VisitMinus(Minus expression)
        {
            if (!IsBinaryExpressionValid(expression, out string message))
            {
                throw new InterpretException(message);
            }
            return (double)expression.Left.Accept(this) - (double)expression.Right.Accept(this);
        }
        public object VisitMultiply(Multiply expression)
        {
            if (!IsBinaryExpressionValid(expression, out string message))
            {
                throw new InterpretException(message);
            }
            return (double)expression.Left.Accept(this) * (double)expression.Right.Accept(this);
        }
        public object VisitDivide(Divide expression)
        {
            if (!IsBinaryExpressionValid(expression, out string message))
            {
                throw new InterpretException(message);
            }
            return (double)expression.Left.Accept(this) / (double)expression.Right.Accept(this);
        }
        #endregion BINARY_EXPRESSION
        #endregion EXPRESSION

        private bool IsBinaryExpressionValid(BinaryExpression expression, out string message)
        {
            if (expression.Left.Accept(this) is string && expression.Right.Accept(this) is double ||
            expression.Right.Accept(this) is string && expression.Left.Accept(this) is double)
            {
                message = "You cannot + or - or * or / different datatypes!";
                return false;
            }
            if ((expression.Left.Accept(this) is string || expression.Right.Accept(this) is string) && !(expression is Plus))
            {
                message = "You cannot - or * or / with string datatypes!";
                return false;
            }
            if (expression.Left.Accept(this) is string)
            {
                message = "string";
            }
            else
            {
                message = "number";
            }
            return true;
        }

        private bool IsBinaryRelConditionValid(BinaryRelCondition condition, out string message)
        {
            //Testing if condition is comparing same datatype
            if ((condition.Left.Accept(this) is string && condition.Right.Accept(this) is double) ||
                (condition.Right.Accept(this) is string && condition.Left.Accept(this) is double))
            {
                message = "You cannot compare different datatypes!";
                return false;
            }
            if (condition.Left.Accept(this) is string)
            {
                message = "string";
            }
            else
            {
                message = "number";
            }
            return true;
        }
    }
}
