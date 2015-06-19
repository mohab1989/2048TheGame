namespace _2048
{
    partial class frmMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.levelOptionsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.levelMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.levelHard = new System.Windows.Forms.ToolStripMenuItem();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.newGameStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            #region Upper Section
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameStripMenuItem,
            this.levelOptionsStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(428, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // newGameStripMenuItem
            // 
            this.newGameStripMenuItem.Name = "newGameStripMenuItem";
            this.newGameStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.newGameStripMenuItem.Text = "New Game";
            this.newGameStripMenuItem.Click += new System.EventHandler(this.newGameStripMenuItem_Click);
            // 
            // levelOptionsStripMenuItem
            // 
            this.levelOptionsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelEasy,
            this.levelMedium,
            this.levelHard});
            this.levelOptionsStripMenuItem.Name = "levelOptionsStripMenuItem";
            this.levelOptionsStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelOptionsStripMenuItem.Text = "Level";
            // 
            // levelEasy
            // 
            this.levelEasy.CheckOnClick = true;
            this.levelEasy.Name = "levelEasy";
            this.levelEasy.Size = new System.Drawing.Size(152, 22);
            this.levelEasy.Tag = 6;
            this.levelEasy.Text = "Easy - 6*6";
            this.levelEasy.Click += new System.EventHandler(this.levelOptionToolStripMenuItem_Click);
            // 
            // levelMedium
            // 
            this.levelMedium.CheckOnClick = true;
            this.levelMedium.Name = "levelMedium";
            this.levelMedium.Size = new System.Drawing.Size(152, 22);
            this.levelMedium.Tag = 5;
            this.levelMedium.Text = "Medium - 5*5";
            this.levelMedium.Click += new System.EventHandler(this.levelOptionToolStripMenuItem_Click);
            // 
            // levelHard
            // 
            this.levelHard.Checked = true;
            this.levelHard.CheckOnClick = true;
            this.levelHard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.levelHard.Name = "levelHard";
            this.levelHard.Size = new System.Drawing.Size(152, 22);
            this.levelHard.Tag = 4;
            this.levelHard.Text = "Hard - 4*4";
            this.levelHard.Click += new System.EventHandler(this.levelOptionToolStripMenuItem_Click);
            #endregion           
            #region Lower Section
            // 
            // containerPanel
            // 
            this.containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerPanel.Location = new System.Drawing.Point(0, 24);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(428, 328);
            this.containerPanel.TabIndex = 1;

            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(428, 352);
            this.Controls.Add(this.containerPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "2048";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            #endregion

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem levelOptionsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelEasy;
        private System.Windows.Forms.ToolStripMenuItem levelMedium;
        private System.Windows.Forms.ToolStripMenuItem levelHard;
        private System.Windows.Forms.Panel containerPanel;
        private System.Windows.Forms.ToolStripMenuItem newGameStripMenuItem;
    }
}