using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Exceptions
{
    public class ParserException : LanguageException
    {
        public ParserException(string message) : base(message) { }
    }
}
