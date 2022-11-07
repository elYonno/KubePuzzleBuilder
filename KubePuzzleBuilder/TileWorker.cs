using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace KubePuzzleBuilder
{
    internal class TileWorker
    {
        Tile[,] tiles = new Tile[6, 9];

        public TileWorker()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    String id = (i + 1) + "" + (j + 1);
                    Tile tile = new Tile(id);
                    tiles[i, j] = tile;
                }
            }
        }

        private Tile getTile(String id)
        {
            int face = Int32.Parse(id.Substring(0, 1));
            int number = Int32.Parse(id.Substring(1, 1));
            return tiles[face - 1, number - 1];
        }

        public void updateTileImage(String pictureID, int chosenPictureIndex, Label label)
        {
            String id = pictureID.Substring(pictureID.Length - 2);
            Tile tile = getTile(id);
            tile.setTileType(chosenPictureIndex);
            tile.resetTileOrientation();
            label.Text = tile.print();
        }

        public void rotateTile(String pictureID, bool clockwise, Label label)
        {
            String id = pictureID.Substring(pictureID.Length - 2);
            Tile tile = getTile(id);
            if (clockwise)
                tile.turnClockwise();
            else
                tile.turnAntiClockwise();
            label.Text = tile.print();
        }
    }
}
