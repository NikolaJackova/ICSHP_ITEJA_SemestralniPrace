namespace GUI
{
    partial class GrammarPopUp
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
            this.richTextBoxGrammar = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxGrammar
            // 
            this.richTextBoxGrammar.BackColor = System.Drawing.Color.White;
            this.richTextBoxGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxGrammar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBoxGrammar.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxGrammar.Name = "richTextBoxGrammar";
            this.richTextBoxGrammar.ReadOnly = true;
            this.richTextBoxGrammar.Size = new System.Drawing.Size(982, 553);
            this.richTextBoxGrammar.TabIndex = 0;
            this.richTextBoxGrammar.Text = "";
            // 
            // GrammarPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.richTextBoxGrammar);
            this.Name = "GrammarPopUp";
            this.Text = "Grammar";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxGrammar;
    }
}