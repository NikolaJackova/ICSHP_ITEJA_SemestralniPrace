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
        public string FilePath { get; private set; } = string.Empty;
        public InterpretGUI()
        {
            InitializeComponent();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            saveToolStripMenuItem1.Enabled = false;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Text files (*.txt)|*.txt",
                RestoreDirectory = true
            };

            var fileContent = string.Empty;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = fileDialog.FileName;
                var fileStream = fileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
            richTextBox.Text = fileContent;
            saveToolStripMenuItem1.Enabled = true;
        }

        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                StreamWriter textWriter = new StreamWriter(FilePath);
                textWriter.Write(richTextBox.Text);
                textWriter.Close();
            }
        }

        private void SaveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                RestoreDirectory = true
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Stream textStream;
                if ((textStream = saveDialog.OpenFile()) != null)
                {
                    StreamWriter textWriter = new StreamWriter(saveDialog.FileName);
                    textWriter.Write(richTextBox.Text);
                    textWriter.Close();
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
