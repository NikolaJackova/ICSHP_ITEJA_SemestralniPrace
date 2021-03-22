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

namespace LanguageLibrary.AST
{
    public interface IVisitor
    {
        #region CONDITION
        object Visit_EqualsRel(EqualsRel condition);
        object Visit_GreaterEqThanRel(GreaterEqThanRel condition);
        object Visit_GreaterThanRel(GreaterThanRel condition);
        object Visit_LessEqThanRel(LessEqThanRel condition);
        object Visit_LessThanRel(LessThanRel condition);
        object Visit_NotEqualRel(NotEqualRel condition);
        object Visit_OneStatement(OneStatementCondition condition);
        #endregion

        #region EXPRESSION
        object Visit_Divide(Divide expression);
        object Visit_Minus(Minus expression);
        object Visit_Plus(Plus expression);
        object Visit_Multiply(Multiply expression);
        object Visit_NumberExpression(NumberExpression expression);
        object Visit_PlusUnary(PlusUnary expression);
        object Visit_MinusUnary(MinusUnary expression);
        object Visit_StringExpression(StringExpression expression);
        object Visit_IdentExpression(IdentExpression expression);
        #endregion

        #region STATEMENT
        object Visit_ElseStatement(ElseStatement statement);
        object Visit_IfStatement(IfStatement statement);
        object Visit_ForStatement(ForStatement statement);
        object Visit_SetStatement(SetStatement statement);
        object Visit_WhileStatement(WhileStatement statement);
        #endregion

        object Visit_Block(Block block);
        object Visit_Variable(Variable variable);
        object Visit_Program(Program program);
    }
}
