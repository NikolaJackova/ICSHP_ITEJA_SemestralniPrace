using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    public class Program : IASTItem
    {
        internal Block Block { get; set; }

        internal Program(Block block)
        {
            Block = block;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.Visit_Program(this);
        }
    }
}
