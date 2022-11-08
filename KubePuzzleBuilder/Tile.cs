using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KubePuzzleBuilder
{
    internal class Tile
    {
        private readonly string id;
        private int tileType = 0;
        private int tileOrientation = 1;

        public Tile(string id)
        {
            this.id = id;
        }

        public string getID() { return id; }

        public int getTileType() { return tileType; }

        public int getTileOrientation() { return tileOrientation; }

        public void setTileType(int tileType) { this.tileType = tileType; }

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
