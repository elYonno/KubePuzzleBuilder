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
                    string id = (i + 1) + "" + (j + 1);
                    Tile tile = new(id);
                    tiles[i, j] = tile;
                }
            }
        }

        private Tile getTile(string pictureID)
        {
            string id = pictureID.Substring(pictureID.Length - 2);
            int face = Int32.Parse(id.Substring(0, 1));
            int number = Int32.Parse(id.Substring(1, 1));
            return tiles[face - 1, number - 1];
        }

        public string updateTileImage(string pictureID, int chosenPictureIndex)
        {
            Tile tile = getTile(pictureID);
            tile.TileType = chosenPictureIndex;
            tile.resetTileOrientation();
            return tile.print();
        }

        public string updateTileItem(string pictureID, ItemType itemType)
        {
            Tile tile = getTile(pictureID);
            tile.Item = itemType;
            return tile.print();
        }

        public string rotateTile(string pictureID, bool clockwise)
        {
            Tile tile = getTile(pictureID);
            if (clockwise)
                tile.turnClockwise();
            else
                tile.turnAntiClockwise();
            return tile.print();
        }

        public int getOrientation(string pictureID)
        {
            Tile tile = getTile(pictureID);
            return tile.TileOrientation;
        }

        public string export()
        {
            string json = "[\n";

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Tile tile = tiles[i, j];
                    
                    json += "\t{\n";
                    json += "\t\t\"id\": " + tile.ID + ",\n";
                    json += "\t\t\"type\": " + tile.TileType + ",\n";
                    json += "\t\t\"orientation\": " + tile.TileOrientation + ",\n";
                    json += "\t\t\"item\": \"" + tile.Item + "\"\n";
                    json += (i == 5 && j == 8)? "\t}\n" : "\t},\n";
                }
            }

            json += "]";
            return json;
        }
    }
}
