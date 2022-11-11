using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KubePuzzleBuilder
{
    internal enum ItemType
    {
        NONE,
        GREEN_KEY,
        YELLOW_KEY,
        GREEN_LOCK,
        YELLOW_LOCK,
        BUTTON,
        GATE,
        PORTAL,
        LEVER,
        CONVEYOR
    };

    internal class Tile
    {
        private readonly string id;
        private int tileType = 0;
        private int tileOrientation = 1;
        private ItemType itemType = ItemType.NONE;

        public Tile(string id)
        {
            this.id = id;
        }

        public string getID() { return id; }

        public int getTileType() { return tileType; }

        public int getTileOrientation() { return tileOrientation; }

        public void setTileType(int tileType) { this.tileType = tileType; }

        public void setItemType(ItemType itemType) { this.itemType = itemType; }

        public ItemType getItemType() { return itemType; }

        public void resetTileOrientation() { tileOrientation = 1; }

        public void turnClockwise()
        {
            tileOrientation++;
            if (tileOrientation > 4) tileOrientation = 1;
        }

        public void turnAntiClockwise()
        {
            tileOrientation--;
            if (tileOrientation < 1) tileOrientation = 4;
        }

        public string print()
        {
            return Int32.Parse(id.Substring(1, 1)) + ": " + getTileType() + "," + getTileOrientation();
        }
    }
}
