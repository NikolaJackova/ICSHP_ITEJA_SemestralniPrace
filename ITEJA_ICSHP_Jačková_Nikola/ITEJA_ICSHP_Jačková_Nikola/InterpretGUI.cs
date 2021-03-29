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
        public LanguageLibrary.Interpreter.Interpreter Interpret { get; private set; }
        public InterpretGUI()
        {
            InitializeComponent();
        }

        #region FILE
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your work?", "Confirmation", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (saveToolStripMenuItem1.Enabled == true)
                {
                    saveToolStripMenuItem1.PerformClick();
                }
                else
                {
                    saveAsToolStripMenuItem1.PerformClick();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            leftTextBox.Clear();
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
            leftTextBox.Text = fileContent;
            saveToolStripMenuItem1.Enabled = true;
        }
        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                StreamWriter textWriter = new StreamWriter(FilePath);
                textWriter.Write(leftTextBox.Text);
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
                if ((_ = saveDialog.OpenFile()) != null)
                {
                    StreamWriter textWriter = new StreamWriter(saveDialog.FileName);
                    textWriter.Write(leftTextBox.Text);
                    textWriter.Close();
                }
            }
        }
        private void LoadExample01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExample("Example01.txt");
        }
        private void LoadExample02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExample("Example02.txt");
        }
        private void LoadExample03ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExample("Example03.txt");
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void OpenExample(string fileName)
        {
            var projectFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).Parent.FullName;
            var path = Path.Combine(projectFolder, @"Grammar\" + fileName);
            leftTextBox.Text = File.ReadAllText(path);
        }
        #endregion FILE
        #region INTERPRET
        private void ShowTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeInterpret(leftTextBox.Text);
            rightTextBox.Text = Interpret.Parser.Lexer.TokensToString();
        }
        private void ShowASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewAST.Nodes.Clear();
            InitializeInterpret(leftTextBox.Text);
            TreeViewBuilder builder = new TreeViewBuilder(Interpret.Parser);
            builder.BuildTreeView(treeViewAST);
            treeViewAST.ExpandAll();
        }
        private void RunProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeInterpret(leftTextBox.Text);
            Interpret.Interpret();
        }
        #endregion INTERPRET

        #region EDIT
        private void InitializeInterpret(string source)
        {
            LanguageLibrary.Lexer.Lexer lexer = new LanguageLibrary.Lexer.Lexer(source);
            LanguageLibrary.Parser.Parser parser = new LanguageLibrary.Parser.Parser(lexer);
            Interpret = new LanguageLibrary.Interpreter.Interpreter(parser);
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftTextBox.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftTextBox.Redo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftTextBox.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftTextBox.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftTextBox.Paste();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftTextBox.SelectAll();
        }
        #endregion EDIT
        #region FONT
        private void FontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                leftTextBox.SelectionFont = fontDialog.Font;
            }
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                leftTextBox.SelectionColor = colorDialog.Color;
            }
        }
        #endregion FONT
    }
}
