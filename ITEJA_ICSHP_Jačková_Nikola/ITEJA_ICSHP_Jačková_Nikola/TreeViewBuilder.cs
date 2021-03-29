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

namespace ITEJA_ICSHP_Jačková_Nikola
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
            Parser.ResetParser();
            LanguageLibrary.Parser.Program program = Parser.Parse();
            TreeNode root = (TreeNode)program.Accept(this);
            treeView.Nodes.Add(root);
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

        public object VisitDivide(Divide expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Divide: /"
            };
            node.Nodes.Add((TreeNode)expression.Left.Accept(this));
            node.Nodes.Add((TreeNode)expression.Right.Accept(this));
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

        public object VisitEqualsRel(EqualsRel condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Equal: =="
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
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

        public object VisitGreaterEqThanRel(GreaterEqThanRel condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Greater Equal Than: >="
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
            return node;
        }

        public object VisitGreaterThanRel(GreaterThanRel condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Greater Than: >"
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
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

        public object VisitLessEqThanRel(LessEqThanRel condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Less Equal Than: <="
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
            return node;
        }

        public object VisitLessThanRel(LessThanRel condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Less Than: <"
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
            return node;
        }

        public object VisitMinus(Minus expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Minus: -"
            };
            node.Nodes.Add((TreeNode)expression.Left.Accept(this));
            node.Nodes.Add((TreeNode)expression.Right.Accept(this));
            return node;
        }

        public object VisitMinusUnary(MinusUnary expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Unary: -"
            };
            node.Nodes.Add((TreeNode)expression.Expression.Accept(this));
            return node;
        }

        public object VisitMultiply(Multiply expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Multiply: *"
            };
            node.Nodes.Add((TreeNode)expression.Left.Accept(this));
            node.Nodes.Add((TreeNode)expression.Right.Accept(this));
            return node;
        }

        public object VisitNotEqualRel(NotEqualRel condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "Not Equal: !="
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            node.Nodes.Add((TreeNode)condition.Right.Accept(this));
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

        public object VisitOneStatement(OneStatementCondition condition)
        {
            TreeNode node = new TreeNode
            {
                Text = "One Statement Condition"
            };
            node.Nodes.Add((TreeNode)condition.Left.Accept(this));
            return node;
        }

        public object VisitPlus(Plus expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Plus: +"
            };
            node.Nodes.Add((TreeNode)expression.Left.Accept(this));
            node.Nodes.Add((TreeNode)expression.Right.Accept(this));
            return node;
        }

        public object VisitPlusUnary(PlusUnary expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "Unary: +"
            };
            node.Nodes.Add((TreeNode)expression.Expression.Accept(this));
            return node;
        }

        public object VisitPrintMethod(PrintMethod method)
        {
            TreeNode node = new TreeNode
            {
                Text = "Print Method"
            };
            node.Nodes.Add(method.Identifier.Identifier);
            foreach (var parameter in method.Parameters)
            {
                node.Nodes.Add((TreeNode)parameter.Accept(this));
            }
            return node;
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

        public object VisitStringExpression(StringExpression expression)
        {
            TreeNode node = new TreeNode
            {
                Text = "String"
            };
            node.Nodes.Add(expression.Text);
            return node;
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
    }
}
