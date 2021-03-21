using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    class Program
    {
        public Block Block { get; private set; }

        public Program(Block block)
        {
            Block = block;
        }
    }
}
