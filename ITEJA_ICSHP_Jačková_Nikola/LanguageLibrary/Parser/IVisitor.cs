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

namespace LanguageLibrary.Parser
{
    /// <summary>
    /// Interface for visitor pattern
    /// Sources: http://alumni.cs.ucr.edu/~lgao/teaching/visitor.html
    ///          https://refactoring.guru/design-patterns/visitor
    /// </summary>
    public interface IVisitor
    {
        #region CONDITION
        object VisitEqualsRel(EqualsRel condition);
        object VisitGreaterEqThanRel(GreaterEqThanRel condition);
        object VisitGreaterThanRel(GreaterThanRel condition);
        object VisitLessEqThanRel(LessEqThanRel condition);
        object VisitLessThanRel(LessThanRel condition);
        object VisitNotEqualRel(NotEqualRel condition);
        object VisitOneStatement(OneStatementCondition condition);
        #endregion CONDITION

        #region EXPRESSION
        object VisitDivide(Divide expression);
        object VisitMinus(Minus expression);
        object VisitPlus(Plus expression);
        object VisitMultiply(Multiply expression);
        object VisitNumberExpression(NumberExpression expression);
        object VisitPlusUnary(PlusUnary expression);
        object VisitMinusUnary(MinusUnary expression);
        object VisitStringExpression(StringExpression expression);
        object VisitIdentExpression(IdentExpression expression);
        #endregion EXPRESSION

        #region STATEMENT
        object VisitElseStatement(ElseStatement statement);
        object VisitIfStatement(IfStatement statement);
        object VisitForStatement(ForStatement statement);
        object VisitSetStatement(SetStatement statement);
        object VisitWhileStatement(WhileStatement statement);
        object VisitPrintMethod(PrintMethod method);
        object VisitForwardMethod(ForwardMethod method);
        #endregion STATEMENT

        object VisitBlock(Block block);
        object VisitVariable(VariableDeclaration variable);
        object VisitProgram(Program program);
    }
}
