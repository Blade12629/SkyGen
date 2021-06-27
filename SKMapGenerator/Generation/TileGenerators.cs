using SKMapGenerator.Ultima;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Generation
{
    public class HeightMapGenerator : IGenerator
    {
        public void Generate(ColorStore cs, Bitmap bmp, TileMatrix tileMatrix)
        {
            for (int x = 0; x < bmp.Width && x < tileMatrix.Width; x++)
            {
                for (int y = 0; y < bmp.Height && y < tileMatrix.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    ref var tile = ref tileMatrix.GetTile(x, y);
                    sbyte z = (sbyte)cs[color];

                    tile.Z = z;
                }
            }
        }
    }

    public class TileMapGenerator : IGenerator
    {
        public void Generate(ColorStore cs, Bitmap bmp, TileMatrix tileMatrix)
        {
            for (int x = 0; x < bmp.Width && x < tileMatrix.Width; x++)
            {
                for (int y = 0; y < bmp.Height && y < tileMatrix.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    ref var tile = ref tileMatrix.GetTile(x, y);
                    short tileId = (short)cs[color];

                    tile.TileId = tileId;
                }
            }
        }
    }
}
