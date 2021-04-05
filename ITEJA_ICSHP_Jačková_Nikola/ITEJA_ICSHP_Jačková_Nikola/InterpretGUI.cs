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

namespace GUI
{
    public partial class InterpretGUI : Form
    {
        public string FilePath { get; private set; } = string.Empty;
        private LanguageLibraryEngine Engine { get; set; }
        public InterpretGUI()
        {
            Engine = LanguageLibraryEngine.GetInstance();
            InitializeComponent();
            //For testing purposes
            loadExample01ToolStripMenuItem.PerformClick();
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
            saveToolStripMenuItem1.Enabled = false;
            editorTextBox.Clear();
            tokensTextBox.Clear();
            treeViewAST.Nodes.Clear();
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
        private void OpenExample01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Text = OpenFileFromRootProjectDirectory(@"Grammar\Example01.txt", out string path);
            FilePath = path;
            saveToolStripMenuItem1.Enabled = true;
        }
        private void OpenExample02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Text = OpenFileFromRootProjectDirectory(@"Grammar\Example02.txt", out string path);
            FilePath = path;
            saveToolStripMenuItem1.Enabled = true;
        }
        private void OpenExample03ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTextBox.Text = OpenFileFromRootProjectDirectory(@"Grammar\Example03.txt", out string path);
            FilePath = path;
            saveToolStripMenuItem1.Enabled = true;
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private string OpenFileFromRootProjectDirectory(string fileName, out string path)
        {
            var projectFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).Parent.FullName;
            path = Path.Combine(projectFolder, fileName);
            return File.ReadAllText(path);
        }
        #endregion FILE

        #region INTERPRET
        private void ShowTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tokensTextBox.Text = string.Empty;
                if (InitializeInterpret(editorTextBox.Text)) {
                    tokensTextBox.Text = Engine.Interpreter.Parser.Lexer.TokensToString();
                }
            }
            catch (LanguageLibrary.Exceptions.LanguageException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                treeViewAST.Nodes.Clear();
                if (InitializeInterpret(editorTextBox.Text))
                {
                    TreeViewBuilder builder = new TreeViewBuilder(Engine.Interpreter.Parser);
                    builder.BuildTreeView(treeViewAST);
                    treeViewAST.ExpandAll();
                }
            }
            catch (LanguageLibrary.Exceptions.LanguageException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RunProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (InitializeInterpret(editorTextBox.Text))
                {
                    Console console = new Console(Engine);
                    console.Show();
                }

            }
            catch (LanguageLibrary.Exceptions.LanguageException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool InitializeInterpret(string source)
        {
            if (editorTextBox.Text == string.Empty)
            {
                MessageBox.Show("There is no code for process!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            Engine.InitializeInterpret(source);
            return true;
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
            GrammarPopUp grammar = new GrammarPopUp(OpenFileFromRootProjectDirectory(@"Grammar\ITEJA_Grammar.txt", out string path));
            grammar.Show();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a semestral work from ITEJA and ICSHP.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion ABOUT
    }
}
