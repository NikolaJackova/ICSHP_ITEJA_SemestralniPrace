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

namespace GUI
{
    public partial class Console : Form
    {
        private LanguageLibraryEngine Engine { get; set; }
        private Turtle Turtle { get; set; }

        public Console(LanguageLibraryEngine engine)
        {
            InitializeComponent();
            Engine = engine;
            Turtle = Turtle.GetInstance();
            SetDelegates();
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

        private void ChangePenMethod(string color, double width)
        {
            Turtle.ChangePen(color, width);
        }
        private void SetVisibility(double visibility)
        {
            Turtle.SetVisibility(visibility);
        }
        private void SetDelegates()
        {
            Engine.Interpreter.SetPrint(PrintMethod);
            Engine.Interpreter.SetForward(ForwardMethod);
            Engine.Interpreter.SetRotate(RotateMethod);
            Engine.Interpreter.SetBackward(BackwardMethod);
            Engine.Interpreter.SetChangePen(ChangePenMethod);
            Engine.Interpreter.SetPenVisible(SetVisibility);
        }

        private void ConsolePanel_DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            RunProgram(e);
        }
    }
}
