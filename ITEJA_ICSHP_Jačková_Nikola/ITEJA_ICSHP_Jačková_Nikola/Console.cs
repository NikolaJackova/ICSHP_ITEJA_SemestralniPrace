using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LanguageLibrary.Interpreter;
using LanguageLibrary.Parser;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    public partial class Console : Form
    {
        public Interpreter Interpreter { get; private set; }
        public Console(Interpreter interpreter)
        {
            Interpreter = interpreter;
            Interpreter.SetPrint(PrintMethod);
            InitializeComponent();
            RunProgram();
        }

        private void RunProgram()
        {
            Interpreter.Interpret();
        }

        private void PrintMethod(string text)
        {
            richTextBoxConsole.Text += text + "\n";
        }
    }
}
