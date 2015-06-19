using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class frmMain : Form
    {
        ctrlGrid Grid;
        ToolStripMenuItem currentLevel;
        
        // constructor
        public frmMain()
        {
            InitializeComponent();
            currentLevel = levelHard;
            Grid = new ctrlGrid(4);
            Grid.WhenGameIsOver += _grid_WhenGameIsOver;
            this.containerPanel.Controls.Clear();
            Grid.Dock = DockStyle.Fill;
            this.containerPanel.Controls.Add(Grid);
        }

        void _grid_WhenGameIsOver(bool isWon)
        {
            string msg = string.Empty;
            if(isWon)
            {
                msg = "You Won! :)";
            }
            else
            {
                msg = "You Lost! :(";
            }

            DialogResult res = MessageBox.Show(msg + "\nDo you want to play a new game?", msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res== System.Windows.Forms.DialogResult.Yes)
            {
                newGame();
            }
            else
            {
                Application.Exit();
            }
        }

        private void levelOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem option = sender as ToolStripMenuItem;
            if(option.Tag != null && option.Tag is int)
            {
                currentLevel.Checked = false;
                currentLevel = option;
                int Rank = (int)option.Tag;
                Grid.init(Rank);
            }
        }


        private void newGameStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void newGame()
        {
            Grid.Reset();
        }
    }
}
