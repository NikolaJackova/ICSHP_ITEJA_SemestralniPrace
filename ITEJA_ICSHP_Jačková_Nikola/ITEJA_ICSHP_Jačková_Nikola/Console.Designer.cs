namespace GUI
{
    partial class Console
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.consolePanel = new System.Windows.Forms.SplitContainer();
            this.richTextBoxConsole = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.consolePanel)).BeginInit();
            this.consolePanel.Panel1.SuspendLayout();
            this.consolePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // consolePanel
            // 
            this.consolePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consolePanel.Location = new System.Drawing.Point(0, 0);
            this.consolePanel.Name = "consolePanel";
            // 
            // consolePanel.Panel1
            // 
            this.consolePanel.Panel1.Controls.Add(this.richTextBoxConsole);
            // 
            // consolePanel.Panel2
            // 
            this.consolePanel.Panel2.BackColor = System.Drawing.SystemColors.Info;
            this.consolePanel.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.ConsolePanel_DrawingPanel_Paint);
            this.consolePanel.Size = new System.Drawing.Size(1192, 721);
            this.consolePanel.SplitterDistance = 465;
            this.consolePanel.TabIndex = 0;
            // 
            // richTextBoxConsole
            // 
            this.richTextBoxConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxConsole.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxConsole.Name = "richTextBoxConsole";
            this.richTextBoxConsole.Size = new System.Drawing.Size(465, 721);
            this.richTextBoxConsole.TabIndex = 1;
            this.richTextBoxConsole.Text = "";
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 721);
            this.Controls.Add(this.consolePanel);
            this.DoubleBuffered = true;
            this.Name = "Console";
            this.Text = "Console";
            this.consolePanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.consolePanel)).EndInit();
            this.consolePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer consolePanel;
        private System.Windows.Forms.RichTextBox richTextBoxConsole;
    }
}