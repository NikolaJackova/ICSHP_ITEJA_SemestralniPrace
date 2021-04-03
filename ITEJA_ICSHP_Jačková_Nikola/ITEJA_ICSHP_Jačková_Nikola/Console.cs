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
        }

        private void RunProgram(PaintEventArgs e)
        {
            Turtle.InitializeTurtle(consolePanel.Panel2, e);
            Engine.Interpreter.Interpret();
        }

        private void PrintMethod(string text)
        {
            richTextBoxConsole.Text += text + "\n";
        }

        private void RotateMethod(double angle)
        {
            Turtle.Rotate(angle);
        }

        private void ForwardMethod(double distance)
        {
            Turtle.Forward(distance);
        }
        private void BackwardMethod(double distance)
        {
            Turtle.Backward(distance);
        }
        private void SetDelegates()
        {
            Engine.Interpreter.SetPrint(PrintMethod);
            Engine.Interpreter.SetForward(ForwardMethod);
            Engine.Interpreter.SetRotate(RotateMethod);
            Engine.Interpreter.SetBackward(BackwardMethod);
        }

        private void ConsolePanel_DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            //TODO Reset
            RunProgram(e);
        }
    }
}
