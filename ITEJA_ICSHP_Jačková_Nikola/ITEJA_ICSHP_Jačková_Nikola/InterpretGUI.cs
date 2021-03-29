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
            editorTextBox.Clear();
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
            editorTextBox.Text = fileContent;
            saveToolStripMenuItem1.Enabled = true;
        }
        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                StreamWriter textWriter = new StreamWriter(FilePath);
                textWriter.Write(editorTextBox.Text);
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
                    textWriter.Write(editorTextBox.Text);
                    textWriter.Close();
                }
            }
        }
        private void LoadExample01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Text = OpenFileFromRootProjectDirectory(@"Grammar\Example01.txt");
        }
        private void LoadExample02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Text = OpenFileFromRootProjectDirectory(@"Grammar\Example02.txt");
        }
        private void LoadExample03ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Text = OpenFileFromRootProjectDirectory(@"Grammar\Example03.txt");
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private string OpenFileFromRootProjectDirectory(string fileName)
        {
            var projectFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).Parent.FullName;
            var path = Path.Combine(projectFolder, fileName);
            return File.ReadAllText(path);
        }
        #endregion FILE

        #region INTERPRET
        private void ShowTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeInterpret(editorTextBox.Text);
            tokensTextBox.Text = Interpret.Parser.Lexer.TokensToString();
        }
        private void ShowASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewAST.Nodes.Clear();
            InitializeInterpret(editorTextBox.Text);
            TreeViewBuilder builder = new TreeViewBuilder(Interpret.Parser);
            builder.BuildTreeView(treeViewAST);
            treeViewAST.ExpandAll();
        }
        private void RunProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeInterpret(editorTextBox.Text);
            Interpret.Interpret();
        }
        private void InitializeInterpret(string source)
        {
            if (editorTextBox.Text == string.Empty)
            {
                MessageBox.Show("There is no code for process!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            LanguageLibrary.Lexer.Lexer lexer = new LanguageLibrary.Lexer.Lexer(source);
            LanguageLibrary.Parser.Parser parser = new LanguageLibrary.Parser.Parser(lexer);
            Interpret = new LanguageLibrary.Interpreter.Interpreter(parser);
        }
        #endregion INTERPRET

        #region EDIT
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Redo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Paste();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.SelectAll();
        }
        #endregion EDIT

        #region FONT
        private void FontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                editorTextBox.SelectionFont = fontDialog.Font;
            }
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                editorTextBox.SelectionColor = colorDialog.Color;
            }
        }
        #endregion FONT

        #region ABOUT
        private void ViewGrammarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrammarPopUp grammar = new GrammarPopUp(OpenFileFromRootProjectDirectory(@"Grammar\ITEJA_Grammar.txt"));
            grammar.Show();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a semestral work from ITEJA and ICSHP.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion ABOUT
    }
}
