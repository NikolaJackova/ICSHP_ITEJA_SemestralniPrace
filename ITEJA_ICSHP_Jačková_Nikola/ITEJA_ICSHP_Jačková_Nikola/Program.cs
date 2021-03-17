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
            string file = "";
            /*using (var sr = new StreamReader("Example01.txt")) {
                file = sr.ReadToEnd();
            }*/
            file = File.ReadAllText("Example01.txt");

            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
            LanguageLibrary.Lexer.Lexer lex = new LanguageLibrary.Lexer.Lexer(file);
            Console.WriteLine(lex.TokensToString());
            Console.ReadKey();
        }
    }
}
