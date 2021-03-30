using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LanguageLibrary.Parser;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    public partial class Console : Form
    {
        public LanguageLibrary.Parser.Program Program { get; private set; }
        public Console(LanguageLibrary.Parser.Program program)
        {
            Program = program;
            InitializeComponent();
            RunProgram();
        }

        private void RunProgram()
        {

        }
    }
}
