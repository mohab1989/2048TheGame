using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class clsRules
    {
        private List<clsTile> _createdTiles;
        public int Rank { get; set; }
        public int Score { get; private set; }

        public delegate void MoveTile(clsTile tileTobeMoved, enumMoveDirection direction);
        // delegate is initialized with null
        public event MoveTile OnTileMoved = null;
        public delegate void MergeTiles(clsTile tileTobeUpdated, clsTile tileTobeRemoved);
        public event MergeTiles OnTilesMerged = null;

        // Implement Single tone design pattern
        private static clsRules _instance;

        public static clsRules Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new clsRules();
                return _instance;
            }
        }


        public List<clsTile> CreatedTiles
        {
            get
            {
                return _createdTiles;
            }
        }
        
        // constructor called from Instance 
        private clsRules()
        {
            _createdTiles = new List<clsTile>();
            Score = 0;
        }

        public void Reset()
        {
            _createdTiles.Clear();
            Score = 0;
        }

        public clsTile CreateNewTile()
        {
            if (_createdTiles.Count < (Rank * Rank))
            {
                Random random = new Random();
                int xPos = random.Next(0, Rank);
                int yPos = random.Next(0, Rank);
                bool occupied = _createdTiles.Any(t => t.xPosition == xPos && t.yPosition == yPos);
                while (occupied)
                {
                    xPos = random.Next(0, Rank);
                    yPos = random.Next(0, Rank);
                    occupied = _createdTiles.Any(t => t.xPosition == xPos && t.yPosition == yPos);
                }

                clsTile Tile = new clsTile();
                Tile.xPosition = xPos;
                Tile.yPosition = yPos;
                Tile.Value = random.NextDouble() < 0.3 ? 4 : 2;
                _createdTiles.Add(Tile);
                return Tile;
            }
            else
                return null;
        }

        #region Up KeyPress
        //
        // returns boolean to indicate if key is pressed
        //
        public bool Up()
        {
            bool result = false;
            // the for loop sweeps the rows moving all the tiles from down to up (resposible for slight Animation)
            for (int i = 1; i < Rank; i++)
            {
                List<clsTile> row_i_Tiles = _createdTiles.FindAll(t => t.yPosition == i);
                foreach (clsTile row_i_Tile in row_i_Tiles)
                {
                    clsTile occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == i - 1) && (t.xPosition == row_i_Tile.xPosition));
                    if (occupiedUpperTile != null)
                    {
                        if (MergeifPossible(occupiedUpperTile, row_i_Tile))
                        {
                            result = true;
                            moveUpIfPossible(occupiedUpperTile, true);
                        }
                    }
                    else
                    {
                        // if all the tiles are already up you can't press up again so check if you can move up
                        result = moveUpIfPossible(row_i_Tile, false);
                    }
                }
            }
            return result;
        }
        //
        // checks if tiles can move up or no 
        // the input bolean expression is to check if the tile is merged before hence it can move up again to merge again.
        // if it didnt merge that means it has reached the upmost position.
        // the input from Tile class is either row_i_Tile meaning the tile over it is not occupied or it can be occupiedUpperTile if merge has happened.
        // reuturns the boolean expression result if true it raises the ActionDone flag in keypress method in ctrlGrid if false sets the flag to false
        //
        bool moveUpIfPossible(clsTile currentTile, bool isMergedBefore)
        {
            bool result = false;

            clsTile occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == (currentTile.yPosition - 1)) && (t.xPosition == currentTile.xPosition));
            int currentYPos = currentTile.yPosition;
            while (occupiedUpperTile == null && currentYPos > 0)
            {
                result = true;
                int ind = _createdTiles.IndexOf(currentTile);
                currentYPos = currentYPos - 1;
                if (OnTileMoved != null)
                    //event that calls on the delegate MoveTiles
                    OnTileMoved(_createdTiles[ind], enumMoveDirection.Up);
                    // we need to update the createdTiles list with the new position that's why i used _createdTiles[ind] instead of currentTile
                     _createdTiles[ind].yPosition = currentYPos;
                    //after moving up one cell check again if upper tile is occupied
                    occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == currentYPos - 1) && (t.xPosition == currentTile.xPosition));
                //
                // merge after moving up
                // before trying to merge check if there's an upper tile to merge with in the 1st place & if the current tile have been merged before
                //
                if (occupiedUpperTile != null && !isMergedBefore)
                {
                    if (MergeifPossible(occupiedUpperTile, currentTile))
                    {
                        isMergedBefore = true;
                        currentYPos = currentYPos - 1;
                        occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == occupiedUpperTile.yPosition - 1) && (t.xPosition == occupiedUpperTile.xPosition));
                    }
                }
            }
            return result;
        }
        #endregion

        #region Left KeyPress
        public bool Left()
        {
            bool result = false;
            for (int i = 1; i < Rank; i++)
            {
                List<clsTile> row_i_Tiles = _createdTiles.FindAll(t => t.xPosition == i);
                foreach (clsTile row_i_Tile in row_i_Tiles)
                {
                    clsTile occupiedUpperTile = _createdTiles.Find(t => (t.xPosition == i - 1) && (t.yPosition == row_i_Tile.yPosition));
                    if (occupiedUpperTile != null)
                    {
                        if (MergeifPossible(occupiedUpperTile, row_i_Tile))
                        {
                            result = true;
                            moveLeftIfPossible(occupiedUpperTile, true);
                        }
                    }
                    else
                    {
                        result = moveLeftIfPossible(row_i_Tile, false);
                    }
                }
            }
            return result;
        }

        bool moveLeftIfPossible(clsTile currentTile, bool isMergedBefore)
        {
            bool result = false;

            clsTile occupiedLeftTile = _createdTiles.Find(t => (t.xPosition == (currentTile.xPosition - 1)) && (t.yPosition == currentTile.yPosition));
            int currentXPos = currentTile.xPosition;
            while (occupiedLeftTile == null && currentXPos > 0)
            {
                result = true;
                int ind = _createdTiles.IndexOf(currentTile);
                currentXPos = currentXPos - 1;
                if (OnTileMoved != null)
                    OnTileMoved(_createdTiles[ind], enumMoveDirection.Left);
                _createdTiles[ind].xPosition = currentXPos;
                occupiedLeftTile = _createdTiles.Find(t => (t.xPosition == currentXPos - 1) && (t.yPosition == currentTile.yPosition));
                if (occupiedLeftTile != null && !isMergedBefore)
                {
                    if (MergeifPossible(occupiedLeftTile, currentTile))
                    {
                        isMergedBefore = true;
                        currentXPos = currentXPos - 1;
                        occupiedLeftTile = _createdTiles.Find(t => (t.xPosition == occupiedLeftTile.xPosition - 1) && (t.yPosition == occupiedLeftTile.yPosition));
                    }
                }
            }
            return result;
        }
        #endregion

        #region Down KeyPress
        public bool Down()
        {
            bool result = false;
            for (int i = (Rank - 1); i >= 0; i--)
            {
                List<clsTile> row_i_Tiles = _createdTiles.FindAll(t => t.yPosition == i);
                foreach (clsTile row_i_Tile in row_i_Tiles)
                {
                    clsTile occupiedLowerTile = _createdTiles.Find(t => (t.yPosition == i + 1) && (t.xPosition == row_i_Tile.xPosition));
                    if (occupiedLowerTile != null)
                    {
                        if (MergeifPossible(occupiedLowerTile, row_i_Tile))
                        {
                            result = true;
                            moveDownIfPossible(occupiedLowerTile, true);
                        }
                    }
                    else
                    {
                        result = moveDownIfPossible(row_i_Tile, false);
                    }
                }
            }
            return result;
        }

        bool moveDownIfPossible(clsTile currentTile, bool isMergedBefore)
        {
            bool result = false;

            clsTile occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == (currentTile.yPosition + 1)) && (t.xPosition == currentTile.xPosition));
            int currentYPos = currentTile.yPosition;
            while (occupiedUpperTile == null && currentYPos < (Rank - 1))
            {
                result = true;
                int ind = _createdTiles.IndexOf(currentTile);
                currentYPos = currentYPos + 1;
                if (OnTileMoved != null)
                    OnTileMoved(_createdTiles[ind], enumMoveDirection.Down);
                _createdTiles[ind].yPosition = currentYPos;
                occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == currentYPos + 1) && (t.xPosition == currentTile.xPosition));
                if (occupiedUpperTile != null && !isMergedBefore)
                {
                    if (MergeifPossible(occupiedUpperTile, currentTile))
                    {
                        isMergedBefore = true;
                        currentYPos = currentYPos + 1;
                        occupiedUpperTile = _createdTiles.Find(t => (t.yPosition == occupiedUpperTile.yPosition + 1) && (t.xPosition == occupiedUpperTile.xPosition));
                    }
                }
            }
            return result;
        }
        #endregion

        #region Right KeyPress
        public bool Right()
        {
            bool result = false;
            for (int i = (Rank - 1); i >= 0; i--)
            {
                List<clsTile> row_i_Tiles = _createdTiles.FindAll(t => t.xPosition == i);
                foreach (clsTile row_i_Tile in row_i_Tiles)
                {
                    clsTile occupiedLowerTile = _createdTiles.Find(t => (t.xPosition == i + 1) && (t.yPosition == row_i_Tile.yPosition));
                    if (occupiedLowerTile != null)
                    {
                        if (MergeifPossible(occupiedLowerTile, row_i_Tile))
                        {
                            result = true;
                            moveRightIfPossible(occupiedLowerTile, true);
                        }
                    }
                    else
                    {
                        result = moveRightIfPossible(row_i_Tile, false);
                    }
                }
            }
            return result;
        }

        bool moveRightIfPossible(clsTile currentTile, bool isMergedBefore)
        {
            bool result = false;

            clsTile occupiedRightTile = _createdTiles.Find(t => (t.xPosition == (currentTile.xPosition + 1)) && (t.yPosition == currentTile.yPosition));
            int currentXPos = currentTile.xPosition;
            while (occupiedRightTile == null && currentXPos < (Rank - 1))
            {
                result = true;
                int ind = _createdTiles.IndexOf(currentTile);
                currentXPos = currentXPos + 1;
                // event onTileMoved that call the delegate MoveTile
                if (OnTileMoved != null)
                    OnTileMoved(_createdTiles[ind], enumMoveDirection.Right);
                _createdTiles[ind].xPosition = currentXPos;
                occupiedRightTile = _createdTiles.Find(t => (t.xPosition == currentXPos + 1) && (t.yPosition == currentTile.yPosition));
                if (occupiedRightTile != null && !isMergedBefore)
                {
                    if (MergeifPossible(occupiedRightTile, currentTile))
                    {
                        isMergedBefore = true;
                        currentXPos = currentXPos + 1;
                        occupiedRightTile = _createdTiles.Find(t => (t.xPosition == occupiedRightTile.xPosition + 1) && (t.yPosition == occupiedRightTile.yPosition));
                    }
                }
            }
            return result;
        }
        #endregion

        // checks if moved tile is due for a merge or not 
        bool MergeifPossible(clsTile tileTobeUpdated, clsTile tileTobeRemoved)
        {
            if (tileTobeRemoved.Value == tileTobeUpdated.Value)
            {
                int toBeUpdatedValue = tileTobeRemoved.Value + tileTobeUpdated.Value;
                Score += toBeUpdatedValue;
                // event onTilesMerged that calls the delegate MergeTiles
                if (OnTilesMerged != null)
                    OnTilesMerged(tileTobeUpdated, tileTobeRemoved);
                // update the tile value in the createdTiles list
                int index = _createdTiles.IndexOf(tileTobeUpdated);
                _createdTiles[index].Value = toBeUpdatedValue;
                _createdTiles.Remove(tileTobeRemoved);
                return true;
            }

            return false;
        }

        // check after each key press if game is over
        public bool isGameOver()
        {
            if (_createdTiles.Count < (Rank * Rank))
                return false;
            // scans ALL created tiles and look around it for possible merges
            foreach(clsTile Tile in _createdTiles)
            {
                //look up
                clsTile aroundTile = _createdTiles.Find(t => (t.xPosition == Tile.xPosition) && (t.yPosition == Tile.yPosition - 1));
                //if null is up proceed to look in different direction but if condition check return false meaning game isnt over yet
                if (aroundTile!=null && (Tile.Value == aroundTile.Value))
                    return false;

                //look down
                aroundTile = _createdTiles.Find(t => (t.xPosition == Tile.xPosition) && (t.yPosition == Tile.yPosition + 1));
                if (aroundTile != null && (Tile.Value == aroundTile.Value))
                    return false;

                //look left
                aroundTile = _createdTiles.Find(t => (t.yPosition == Tile.yPosition) && (t.xPosition == Tile.xPosition - 1));
                if (aroundTile != null && (Tile.Value == aroundTile.Value))
                    return false;
                //look right

                aroundTile = _createdTiles.Find(t => (t.yPosition == Tile.yPosition) && (t.xPosition == Tile.xPosition + 1));
                if (aroundTile != null && (Tile.Value == aroundTile.Value))
                    return false;
            }
            return true;
        }
    }
}
