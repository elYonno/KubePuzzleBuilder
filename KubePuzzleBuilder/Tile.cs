using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KubePuzzleBuilder
{
    internal class Tile
    {
        private readonly String id;
        private int tileType;
        private int tileOrientation;

        public Tile(String id)
        {
            this.id = id;
        }

        public String getID() { return id; }

        public int getTileType() { return tileType; }

        public int getTileOrientation() { return tileOrientation; }

        public void setTileType(int tileType) { this.tileType = tileType; }

        public void resetTileOrientation() { this.tileOrientation = 1; }

        public void turnClockwise()
        {
            this.tileOrientation++;
            if (this.tileOrientation > 4) tileOrientation = 1;
        }

        public void turnAntiClockwise()
        {
            this.tileOrientation--;
            if (this.tileOrientation < 1) tileOrientation = 4;
        }

        public String print()
        {
            return Int32.Parse(id.Substring(1, 1)) + ": " + getTileType() + "," + getTileOrientation();
        }
    }
}
