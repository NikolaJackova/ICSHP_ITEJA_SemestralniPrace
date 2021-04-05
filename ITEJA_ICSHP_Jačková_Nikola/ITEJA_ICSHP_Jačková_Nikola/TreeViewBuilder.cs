using LanguageLibrary.Interpreter;
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
using System.Windows.Forms;

namespace GUI
{
    class TreeViewBuilder : IVisitor
    {
        private Parser Parser { get; set; }

        public TreeViewBuilder(Parser parser)
        {
            Parser = parser;
        }

        public void BuildTreeView(TreeView treeView)
        {
            LanguageLibrary.Parser.Program program = Parser.Parse();
            TreeNode root = (TreeNode)program.Accept(this);
            treeView.Nodes.Add(root);
        }

        public object VisitProgram(LanguageLibrary.Parser.Program program)
        {
            TreeNode programNode = new TreeNode()
            {
                Text = "Program"
            };
            programNode.Nodes.Add((TreeNode)program.Block.Accept(this));
            return programNode;
        }

        public object VisitBlock(Block block)
        {
            TreeNode blockNode = new TreeNode
            {
                Text = "Block"
            };
            foreach (var variable in block.Variables)
            {
                blockNode.Nodes.Add((TreeNode)variable.Accept(this));
            }
            foreach (var statement in block.Statements)
            {
                blockNode.Nodes.Add((TreeNode)statement.Accept(this));
            }
            return blockNode;
        }
        public object VisitVariable(VariableDeclaration variable)
        {
            TreeNode node = new TreeNode
            {
                Text = "Var"
            };
            node.Nodes.Add((TreeNode)variable.Var.Accept(this));
            return node;
        }
        #region STATEMENT
        public object VisitIfStatement(IfStatement statement)
        {
            TreeNode node = new TreeNode
            {
                Text = "If"
            };
            node.Nodes.Add((TreeNode)statement.Condition.Accept(this));
            foreach (var block in statement.Blocks)
            {
                node.Nodes.Add((TreeNode)block.Accept(this));
            }
            if (statement.ElseStatement != null)
            {
                node.Nodes.Add((TreeNode)VisitElseStatement(statement.ElseStatement));
            }
            return node;
        }

        public object VisitElseStatement(ElseStatement statement)
        {
            TreeNode node = new TreeNode
            {
                Text = "Else"
            };
            foreach (var block in statement.Blocks)
            {
                node.Nodes.Add((TreeNode)block.Accept(this));
            }
            return node;
        }
        public object VisitForStatement(ForStatement statement)
        {
            TreeNode node = new TreeNode
            {
                Text = "For"
            };
            node.Nodes.Add((TreeNode)statement.Identifier.Accept(this));
            node.Nodes.Add((TreeNode)statement.From.Accept(this));
            node.Nodes.Add((TreeNode)statement.To.Accept(this));
            node.Nodes.Add((TreeNode)statement.Statement.Accept(this));
            foreach (var block in statement.Blocks)
            {
                node.Nodes.Add((TreeNode)block.Accept(this));
            }
            return node;
        }
        public object VisitSetStatement(SetStatement statement)
        {
            TreeNode node = new TreeNode
            {
                Text = "Set: ="
            };
            node.Nodes.Add((TreeNode)statement.Identifier.Accept(this));
            node.Nodes.Add((TreeNode)statement.Expression.Accept(this));
            return node;
        }
        public object VisitWhileStatement(WhileStatement statement)
        {
            TreeNode node = new TreeNode
            {
                Text = "While"
            };
            node.Nodes.Add((TreeNode)statement.Condition.Accept(this));
            foreach (var block in statement.Blocks)
            {
                node.Nodes.Add((TreeNode)block.Accept(this));
            }
            return node;
        }
        #region METHOD
        public object VisitPrintMethod(PrintMethod method)
        {
            return MethodNode(method);
        }
        public object VisitForwardMethod(ForwardMethod method)
        {
            return MethodNode(method);
        }
        public object VisitRotateMethod(RotateMethod method)
        {
            return MethodNode(method);
        }
        public object VisitBackwardMethod(BackwardMethod method)
        {
            return MethodNode(method);
        }
        public object VisitChangePenMethod(ChangePenMethod method)
        {
            return MethodNode(method);
        }
        public object VisitPenVisibleMethod(PenVisibileMethod method)
        {
            return MethodNode(method);
        }
        #endregion METHOD
        #endregion STATEMENT

        #region CONDITION
        public object VisitEqualsRel(EqualsRel condition)
        {
            return BinaryRelCondition(condition);
        }
        public object VisitGreaterEqThanRel(GreaterEqThanRel condition)
        {
            return BinaryRelCondition(condition);
        }
        public object VisitGreaterThanRel(GreaterThanRel condition)
        {
            return BinaryRelCondition(condition);
        }
        public object VisitLessEqThanRel(LessEqThanRel condition)
        {
            return BinaryRelCondition(condition);
        }
        public object VisitLessThanRel(LessThanRel condition)
        {
            return BinaryRelCondition(condition);
        }
        public object VisitNotEqualRel(NotEqualRel condition)
        {
            return BinaryRelCondition(condition);
        }
        public object VisitOneStatement(OneStatementCondition condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "One Statement Condition"
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            return node;
        }
        #endregion CONDITION

        #region EXPRESSION
        public object VisitMinusUnary(MinusUnary expression)
        {
            return UnaryExpression(expression);
        }
        public object VisitPlusUnary(PlusUnary expression)
        {
            return UnaryExpression(expression);
        }
        #region BINARY_RELATION_EXPRESSION
        public object VisitDivide(Divide expression)
        {
            return BinaryRelationExpression(expression);
        }
        public object VisitMultiply(Multiply expression)
        {
            return BinaryRelationExpression(expression);
        }
        public object VisitPlus(Plus expression)
        {
            return BinaryRelationExpression(expression);
        }
        public object VisitMinus(Minus expression)
        {
            return BinaryRelationExpression(expression);
        }
        #endregion BINARY_RELATION_EXPRESSION
        public object VisitStringExpression(StringExpression expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "String"
            };
            node.Nodes.Add(expression.Text);
            return node;
        }
        public object VisitIdentExpression(IdentExpression expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Identifier"
            };
            node.Nodes.Add(expression.Identifier);
            return node;
        }
        public object VisitNumberExpression(NumberExpression expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Number"
            };
            node.Nodes.Add(expression.Value.ToString());
            return node;
        }
        #endregion EXPRESSION

        private object MethodNode(MethodStatement method)
        {
            TreeNode node = new TreeNode
            {
                Text = method.Identifier.Identifier
            };
            TreeNode parameterNode = new TreeNode("Parameters");
            node.Nodes.Add(parameterNode);
            foreach (var parameter in method.Parameters)
            {
                parameterNode.Nodes.Add((TreeNode)parameter.Accept(this));
            }
            return node;
        }

        private object BinaryRelCondition(BinaryRelCondition condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Operation: " + condition.Operation
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
            return node;
        }

        private object BinaryRelationExpression(BinaryExpression expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Operation: " + expression.Operation
            };
            node.Nodes.Add((TreeNode)expression.Left.Accept(this));
            node.Nodes.Add((TreeNode)expression.Right.Accept(this));
            return node;
        }

        private object UnaryExpression(UnaryExpression expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Unary: " + expression.Operation
            };
            node.Nodes.Add((TreeNode)expression.Expression.Accept(this));
            return node;
        }
    }
}
