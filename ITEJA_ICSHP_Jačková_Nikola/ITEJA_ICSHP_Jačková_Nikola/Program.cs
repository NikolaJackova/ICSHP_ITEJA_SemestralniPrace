using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LanguageLibrary.Lexer;
using LanguageLibrary.Parser;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*string file = "D:\\OneDrive\\FEI\\03Programování v .NET a C#\\Semestrální práce\\ICSHP_ITEJA_SemestralniPrace\\ITEJA_ICSHP_Jačková_Nikola\\Grammar\\Example03.txt";
            file = File.ReadAllText(file);
            Lexer lexer = new Lexer(file);
            Parser parser = new Parser(lexer);
            //LanguageLibrary.Parser.Program prog = parser.Parse();
            LanguageLibrary.Interpreter.Interpreter interpret = new LanguageLibrary.Interpreter.Interpreter(parser);
            interpret.Interpret();
            Console.Read();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InterpretGUI());
        }
    }
}
