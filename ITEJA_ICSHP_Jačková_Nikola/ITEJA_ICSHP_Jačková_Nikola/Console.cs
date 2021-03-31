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
        private LanguageLibraryEngine Engine { get; set; }
        private Turtle Turtle { get; set; }

        public Console(LanguageLibraryEngine engine)
        {
            Engine = engine;
            Turtle = Turtle.GetInstance();
            SetDelegates();
            InitializeComponent();
            RunProgram();
        }

        private void RunProgram()
        {
            Turtle.InitializeTurtle(splitContainer1.Panel2);
            Engine.Interpreter.Interpret();
        }

        private void PrintMethod(string text)
        {
            richTextBoxConsole.Text += text + "\n";
        }

        private void ForwardMethod(double distance = 20)
        {
            Turtle.Forward(distance);
        }
        
        private void SetDelegates()
        {
            Engine.Interpreter.SetPrint(PrintMethod);
            Engine.Interpreter.SetForward(ForwardMethod);
        }
    }
}
