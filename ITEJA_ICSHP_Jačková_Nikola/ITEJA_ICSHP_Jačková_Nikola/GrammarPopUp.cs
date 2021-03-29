using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    public partial class GrammarPopUp : Form
    {
        public GrammarPopUp(string text)
        {
            InitializeComponent();
            richTextBox1.Text = text;
        }
    }
}
