using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Exceptions
{
    public class LanguageException : Exception
    {
        public LanguageException(string message) : base(message) { }
    }
}
