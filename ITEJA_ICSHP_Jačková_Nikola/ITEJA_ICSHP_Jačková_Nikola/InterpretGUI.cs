using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    public partial class InterpretGUI : Form
    {
        public InterpretGUI()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Filter = "Text files (*.txt)|*.txt";
            fileDialog.RestoreDirectory = true;

            var fileContent = string.Empty;
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = fileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
            codeTextBox.Text = fileContent;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
