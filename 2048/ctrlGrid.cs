using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace _2048
{
    public partial class ctrlGrid : UserControl
    {
        private TableLayoutPanel gridTable;
        private bool won = false;
        public delegate void GameOver(bool isWon);
        public event GameOver WhenGameIsOver;
       
        //constructor
        public ctrlGrid(int Rank)
        {
            InitializeComponent();
            gridTable = new TableLayoutPanel();
            init(Rank);
            //delegate is fed the Rules_onTileMoved method to handle the event
            clsRules.Instance.OnTileMoved += Rules_OnTileMoved;
            clsRules.Instance.OnTilesMerged += Rules_OnTilesMerged;
            this.splitContainer.Panel2.Controls.Add(gridTable);
        }

        public void init(int Rank)
        {
            scoreValueLabel.Text = "0";
            // 1st class rules
            clsRules.Instance.Reset();
            gridTable.Controls.Clear();
            clsRules.Instance.Rank = Rank;
            this.Size = new System.Drawing.Size(Rank * 85, Rank * 85);
            initGridTable(Rank);
            AddNewTile();            
        }

        // called by init method
        private void initGridTable(int Rank)
        {
            gridTable.RowStyles.Clear();
            gridTable.ColumnStyles.Clear();

            gridTable.Dock = DockStyle.Fill;
            gridTable.RowCount = Rank;
            gridTable.ColumnCount = Rank;
            gridTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            float rowColPrecentage = 100 / Rank;
            for (int i = 0; i < Rank; i++)
            {
                gridTable.RowStyles.Add(new RowStyle(SizeType.Percent, rowColPrecentage));
                gridTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, rowColPrecentage));
            }
        }

        private void AddNewTile()
        {
            clsTile Tile = clsRules.Instance.CreateNewTile();
            if (Tile != null)
            {
                Label tileLabel = new Label();
                tileLabel.TextAlign = ContentAlignment.MiddleCenter;
                tileLabel.Text = Tile.Value.ToString();
                tileLabel.Dock = DockStyle.Fill;
                tileLabel.Font = new Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tileLabel.BackColor = clsTileColors.Instance.GetBackColorFromValue(Tile.Value);
                tileLabel.ForeColor = clsTileColors.Instance.GetForeColorFromValue(Tile.Value);
                // adds label to grid table cells according to x,y positions
                gridTable.Controls.Add(tileLabel, Tile.xPosition, Tile.yPosition);
            }
        }

        //event handler for onTilesMerged event
        void Rules_OnTilesMerged(clsTile tileTobeUpdated, clsTile tileTobeRemoved)
        {
            // update score
            scoreValueLabel.Text = clsRules.Instance.Score.ToString();

            Control toBeRemovedControl = gridTable.GetControlFromPosition(tileTobeRemoved.xPosition, tileTobeRemoved.yPosition);
            Control toBeUpdatedControl = gridTable.GetControlFromPosition(tileTobeUpdated.xPosition, tileTobeUpdated.yPosition);
            
            if (toBeRemovedControl != null && toBeUpdatedControl != null)
            {
                int result = tileTobeRemoved.Value + tileTobeUpdated.Value;
                toBeUpdatedControl.Text = result.ToString();
                toBeUpdatedControl.BackColor = clsTileColors.Instance.GetBackColorFromValue(result);
                toBeUpdatedControl.ForeColor = clsTileColors.Instance.GetForeColorFromValue(result);
                // remove the control (Label) of one of the merged tiles 
                gridTable.Controls.Remove(toBeRemovedControl);
            }

            if (tileTobeUpdated.Value + tileTobeRemoved.Value == 2048)
            {
                won = true;
            }
        }

        //event handler for onTileMoved event
        void Rules_OnTileMoved(clsTile tileTobeMoved, enumMoveDirection direction)
        {
            Control toBeMovedControl = gridTable.GetControlFromPosition(tileTobeMoved.xPosition, tileTobeMoved.yPosition);
            if (toBeMovedControl != null)
            {
                switch (direction)
                {
                    case enumMoveDirection.Up:
                        {
                            // moves the c control (Label) up one cell
                            gridTable.SetCellPosition(toBeMovedControl, new TableLayoutPanelCellPosition(tileTobeMoved.xPosition, tileTobeMoved.yPosition - 1));
                            break;
                        }
                    case enumMoveDirection.Down:
                        {
                            gridTable.SetCellPosition(toBeMovedControl, new TableLayoutPanelCellPosition(tileTobeMoved.xPosition, tileTobeMoved.yPosition + 1));
                            break;
                        }
                    case enumMoveDirection.Right:
                        {
                            gridTable.SetCellPosition(toBeMovedControl, new TableLayoutPanelCellPosition(tileTobeMoved.xPosition + 1, tileTobeMoved.yPosition));
                            break;
                        }
                    case enumMoveDirection.Left:
                        {
                            gridTable.SetCellPosition(toBeMovedControl, new TableLayoutPanelCellPosition(tileTobeMoved.xPosition - 1, tileTobeMoved.yPosition));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        public void Reset()
        {
            won = false;
            this.scoreValueLabel.Text = "0";
            clsRules.Instance.Reset();
            gridTable.Controls.Clear();
            AddNewTile();
        }

        //event handler for key press
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool actionDone;
            switch (keyData)
            {
                case Keys.Up:
                    {
                        actionDone = clsRules.Instance.Up();
                        break;
                    }
                case Keys.Down:
                    {
                        actionDone = clsRules.Instance.Down();
                        break;
                    }
                case Keys.Right:
                    {
                        actionDone = clsRules.Instance.Right();
                        break;
                    }
                case Keys.Left:
                    {
                        actionDone = clsRules.Instance.Left();
                        break;
                    }
                default:
                    {
                        actionDone = false;
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
            }
            if (won && WhenGameIsOver != null)
            {
                //raise the IsWon flag and call the delegate GameOver
                WhenGameIsOver(true);
                return true;
            }

            if (actionDone)
            {
                AddNewTile();
            }
            // checks if game is over after each key press
            if (clsRules.Instance.isGameOver())
            {
                if(WhenGameIsOver!=null)
                {
                    // calls the delegate Game over and settin IsWon flag to false
                    WhenGameIsOver(false);
                }
            }
            return actionDone;
        }
    }
}
