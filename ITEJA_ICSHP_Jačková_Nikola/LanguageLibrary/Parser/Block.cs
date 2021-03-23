using LanguageLibrary.Parser.Statements;
using LanguageLibrary.Parser.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    public class Block : IASTItem
    {
        public LinkedList<Statement> Statements { get; private set; }

        public LinkedList<Variable> Variables { get; private set; }
        public Block(LinkedList<Statement> statements, LinkedList<Variable> variables)
        {
            Statements = statements;
            Variables = variables;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitBlock(this);
        }
    }
}
