using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Exceptions
{
    public class InterpretException : LanguageException
    {
        public InterpretException(string message) : base(message) { }
    }
}
