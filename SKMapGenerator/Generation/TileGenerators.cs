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

        public void Export(ColorStore cs, TileMatrix tileMatrix, string file)
        {
            Dictionary<int, Color> reversedCS = cs.Storage.ToDictionary(e => e.Value, e => e.Key);
            Bitmap bmp = new Bitmap(tileMatrix.Width, tileMatrix.Height);

            for (int x = 0; x < tileMatrix.Width; x++)
            {
                for (int y = 0; y < tileMatrix.Height; y++)
                {
                    ref var tile = ref tileMatrix.GetTile(x, y);

                    if (reversedCS.TryGetValue(tile.Z, out Color color))
                    {
                        bmp.SetPixel(x, y, color);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }

            bmp.Save(file);
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

        public void Export(ColorStore cs, TileMatrix tileMatrix, string file)
        {
            Dictionary<int, Color> reversedCS = cs.Storage.ToDictionary(e => e.Value, e => e.Key);
            Bitmap bmp = new Bitmap(tileMatrix.Width, tileMatrix.Height);

            for (int x = 0; x < tileMatrix.Width; x++)
            {
                for (int y = 0; y < tileMatrix.Height; y++)
                {
                    ref var tile = ref tileMatrix.GetTile(x, y);

                    if (reversedCS.TryGetValue(tile.TileId, out Color color))
                    {
                        bmp.SetPixel(x, y, color);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }

            bmp.Save(file);
        }
    }
}
