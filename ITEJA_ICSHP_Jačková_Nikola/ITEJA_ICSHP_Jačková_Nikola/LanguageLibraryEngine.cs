using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    public class LanguageLibraryEngine
    {
        private static LanguageLibraryEngine Instance { get; set; }
        public LanguageLibrary.Interpreter.Interpreter Interpreter { get; private set; }

        public static LanguageLibraryEngine GetInstance()
        {
            if (Instance == null)
            {
                Instance = new LanguageLibraryEngine();
            }
            return Instance;
        }

        public void InitializeInterpret(string source)
        {
            Interpreter = new LanguageLibrary.Interpreter.Interpreter(source);
        }
    }
}
