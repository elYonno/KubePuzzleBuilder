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
        CONVEYOR,
        TRAP,
        SCISSORS
    };

    internal class Tile
    {
        public string ID { get; }
        public int TileType { get; set; } = 0;
        public int TileOrientation { get; private set; } = 1;
        public ItemType Item { get; set; } = ItemType.NONE;

        public Tile(string ID)
        {
            this.ID = ID;
        }


        public void setItemType(ItemType itemType) { this.Item = itemType; }

        public ItemType getItemType() { return Item; }

        public void resetTileOrientation() { TileOrientation = 1; }

        public void turnClockwise()
        {
            TileOrientation++;
            if (TileOrientation > 4) TileOrientation = 1;
        }

        public void turnAntiClockwise()
        {
            TileOrientation--;
            if (TileOrientation < 1) TileOrientation = 4;
        }

        public string print()
        {
            return int.Parse(ID.Substring(1, 1)) + " : " + TileType + "," + TileOrientation + (Item == ItemType.NONE? "" : "*");
        }
    }
}
