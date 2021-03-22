using LanguageLibrary.AST;
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
        public object Visit_Block(Block block)
        {
            throw new NotImplementedException();
        }

        public object Visit_Divide(Divide expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_ElseStatement(ElseStatement statement)
        {
            throw new NotImplementedException();
        }

        public object Visit_EqualsRel(EqualsRel condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_ForStatement(ForStatement statement)
        {
            throw new NotImplementedException();
        }

        public object Visit_GreaterEqThanRel(GreaterEqThanRel condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_GreaterThanRel(GreaterThanRel condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_IdentExpression(IdentExpression expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_IfStatement(IfStatement statement)
        {
            throw new NotImplementedException();
        }

        public object Visit_LessEqThanRel(LessEqThanRel condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_LessThanRel(LessThanRel condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_Minus(Minus expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_MinusUnary(MinusUnary expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_Multiply(Multiply expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_NotEqualRel(NotEqualRel condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_NumberExpression(NumberExpression expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_OneStatement(OneStatementCondition condition)
        {
            throw new NotImplementedException();
        }

        public object Visit_Plus(Plus expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_PlusUnary(PlusUnary expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_Program(Program program)
        {
            throw new NotImplementedException();
        }

        public object Visit_SetStatement(SetStatement statement)
        {
            throw new NotImplementedException();
        }

        public object Visit_StringExpression(StringExpression expression)
        {
            throw new NotImplementedException();
        }

        public object Visit_Variable(Variable variable)
        {
            throw new NotImplementedException();
        }

        public object Visit_WhileStatement(WhileStatement statement)
        {
            throw new NotImplementedException();
        }
    }
}
