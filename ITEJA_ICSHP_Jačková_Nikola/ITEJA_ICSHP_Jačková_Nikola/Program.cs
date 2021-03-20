using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string file = "D:\\OneDrive\\FEI\\03Programování v .NET a C#\\Semestrální práce\\ICSHP_ITEJA_SemestralniPrace\\ITEJA_ICSHP_Jačková_Nikola\\Grammar\\Example01.txt";
            file = File.ReadAllText(file);
            LanguageLibrary.Lexer.Lexer lexer = new LanguageLibrary.Lexer.Lexer(file);
            Console.WriteLine(lexer.TokensToString());
            Console.Read();
        }
    }
}
