using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing;
using SKMapGenerator.Generation;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace SKMapGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            MainTask(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainTask(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Dictionary<int, string> options = new Dictionary<int, string>()
                {
                    { 1, "Generate map from heightmap image" },
                    { 2, "Generate map from tilemap image" },
                    { 3, "Generate map from tile- and heightmap image" },
                    { 4, "Exit" }
                };

                int selected = ReadOption(options);

                if (selected == 4)
                {
                    Environment.Exit(0);
                    return;
                }

                switch(selected)
                {
                    case 1:
                        Console.WriteLine("Make sure you have the following files in the folder!: heights.bmp, heights.colorstore");
                        break;

                    case 2:

                        Console.WriteLine("Make sure you have the following files in the folder!: tiles.bmp, tiles.colorstore");
                        break;

                    case 3:
                        Console.WriteLine("Make sure you have the following files in the folder!: heights.bmp, heights.colorstore, tiles.bmp, tiles.colorstore");
                        break;
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                int index = 0;

                while (File.Exists($"map{index}.mul") ||
                      File.Exists($"statics{index}.mul") ||
                      File.Exists($"staidx{index}.mul"))
                    index++;

                switch(selected)
                {
                    case 1:
                        CreateMap("heights.bmp", null, null, "heights.colorstore", $"map{index}.mul", $"statics{index}.mul", $"staidx{index}.mul");
                        break;

                    case 2:
                        CreateMap(null, "tiles.bmp", "tiles.colorstore", null, $"map{index}.mul", $"statics{index}.mul", $"staidx{index}.mul");
                        break;

                    case 3:
                        CreateMap("heights.bmp", "tiles.bmp", "tiles.colorstore", "heights.colorstore", $"map{index}.mul", $"statics{index}.mul", $"staidx{index}.mul");
                        break;
                }
                //CreateTestHeightColorStore("mapHeights.colorstore");
                //CreateTestTileColorStore("mapTiles.colorstore");

                //CreateFromHeightmap("mapHeights2.bmp", "mapTiles2.bmp", "mapTiles.colorstore", "mapHeights.colorstore", @"D:\Electronic Arts\ClassicUO\map6.mul", @"D:\Electronic Arts\ClassicUO\statics6.mul", @"D:\Electronic Arts\ClassicUO\staidx6.mul");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }

        static int ReadOption(Dictionary<int, string> options)
        {
            StringBuilder menu = new StringBuilder();

            menu.AppendLine("Enter the option you want to pick:");
            options = options.OrderBy(o => o.Key).ToDictionary(o => o.Key, o => o.Value);

            foreach(var pair in options)
            {
                menu.AppendLine($"- {pair.Key}: {pair.Value}");
            }

            Refresh();

            for (; ; )
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) ||
                    !int.TryParse(input, out int option) ||
                    option > options.Count || option <= 0)
                {
                    Refresh();
                    continue;
                }

                return option;
            }

            void Refresh()
            {
                Console.Clear();
                Console.WriteLine(menu.ToString());
            }
        }

        static void CreateTestTileColorStore(string path)
        {
            ColorStore store = new ColorStore(5);

            store.AddRange((Color.FromArgb(255, 255,   0,   0).ToArgb(), 500),
                           (Color.FromArgb(255,   0, 255,   0).ToArgb(), 4),
                           (Color.FromArgb(255,   0,   0, 255).ToArgb(), 168),

                           (Color.FromArgb(255, 255, 255, 255).ToArgb(), 283),
                           (Color.FromArgb(255,   0,   0,   0).ToArgb(), 0));

            store.Save(path);
        }

        static void CreateTestHeightColorStore(string path)
        {
            ColorStore store = new ColorStore(255 / 2);
            sbyte baseHeight = 0;

            for (int i = 0; i < 10; i++)
            {
                int diff = i * 5;
                Color c = Color.FromArgb(255, 125 + diff, 125 + diff, 125 + diff);

                store.Add(c.ToArgb(), baseHeight + diff);
            }

            store.Save(path);
        }

        static void CreateMap(string heightBmpPath, string tileBmpPath, string tileColorStorePath, string heightColorStorePath, string mapPath, string staticsPath, string staidxPath)
        {
            Ultima.TileMatrix tileMatrix = null;
            bool generatedHeight = false;
            bool generatedTile = false;

            try
            {
                if (!string.IsNullOrEmpty(heightBmpPath) && File.Exists(heightBmpPath) && File.Exists(heightColorStorePath))
                {
                    ColorStore csHeight = new ColorStore(255);
                    csHeight.Load(heightColorStorePath);

                    using (Bitmap bmpHeight = (Bitmap)Image.FromFile(heightBmpPath))
                    {
                        tileMatrix = new Ultima.TileMatrix(bmpHeight.Width, bmpHeight.Height, 0, 0);
                        
                        HeightMapGenerator hmg = new HeightMapGenerator();
                        hmg.Generate(csHeight, bmpHeight, tileMatrix);
                    }

                    generatedHeight = true;
                }

                if (!string.IsNullOrEmpty(tileBmpPath) && File.Exists(tileBmpPath) && File.Exists(tileColorStorePath))
                {
                    ColorStore csTile = new ColorStore(255);
                    csTile.Load(tileColorStorePath);

                    using (Bitmap bmpTile = (Bitmap)Image.FromFile(tileBmpPath))
                    {
                        if (tileMatrix == null)
                            tileMatrix = new Ultima.TileMatrix(bmpTile.Width, bmpTile.Height, 0, 0);

                        TileMapGenerator tmg = new TileMapGenerator();
                        tmg.Generate(csTile, bmpTile, tileMatrix);
                    }

                    generatedTile = true;
                }

                if (generatedHeight || generatedTile)
                {
                    Ultima.Map map = new Ultima.Map(tileMatrix.Width, tileMatrix.Height, 0, 0);
                    map.Tiles = tileMatrix;
                    map.SetPaths(mapPath, staticsPath, staidxPath);

                    map.Save();

                    Console.WriteLine($"Map saved to:\n{mapPath}\n{staticsPath}\n{staidxPath}");
                }
                else
                {
                    Console.WriteLine("Something went wrong processing the map, please check your input");
                }
            }
            finally
            {
            }
        }

        static void CreateFromHeightmap(string heightBmpPath, string tileBmpPath, string tileColorStorePath, string heightColorStorePath, string mapPath, string staticsPath, string staidxPath)
        {
            HeightMapGenerator hmg = new HeightMapGenerator();
            TileMapGenerator tmg = new TileMapGenerator();
            ColorStore csTiles = new ColorStore(10);
            ColorStore csHeights = new ColorStore(10);

            csTiles.Load(tileColorStorePath);
            csHeights.Load(heightColorStorePath);

            Ultima.TileMatrix tileMatrix;

            using (Bitmap bmpTiles = (Bitmap)Image.FromFile(tileBmpPath))
            {
                tileMatrix = new Ultima.TileMatrix(bmpTiles.Width, bmpTiles.Height, 0, 0);
                tmg.Generate(csTiles, bmpTiles, tileMatrix);
            }

            using (Bitmap bmpHeights = (Bitmap)Image.FromFile(heightBmpPath))
            {
                hmg.Generate(csHeights, bmpHeights, tileMatrix);
            }

            Ultima.Map map = new Ultima.Map(tileMatrix.Width, tileMatrix.Height, 0, 0);
            map.Tiles = tileMatrix;

            map.SetPaths(mapPath, staticsPath, staidxPath);
            map.Save();
        }

        static void CreateTestMap()
        {
            int width = 8192;
            int height = 8192;
            sbyte realHeight = 5;

            Ultima.Map map = new Ultima.Map(width, height, 283, realHeight);
            map.SetPaths("map6.mul", "statics6.mul", "staidx6.mul");

            for (int x = 0; x < width; x++)
            {
                ref Ultima.Tile tile = ref map.Tiles.GetTile(x, height - 1);
                ref Ultima.Tile tile2 = ref map.Tiles.GetTile(x, 0);

                tile.TileId = 5;
                tile.Z = 0;

                tile2.TileId = 5;
                tile2.Z = 0;
            }

            for (int y = 0; y < height; y++)
            {
                ref Ultima.Tile tile = ref map.Tiles.GetTile(width - 1, y);
                ref Ultima.Tile tile2 = ref map.Tiles.GetTile(0, y);

                tile.TileId = 5;
                tile.Z = 0;

                tile2.TileId = 5;
                tile2.Z = 0;
            }

            ref var staticBlock = ref map.StaticTiles.GetStaticBlock(width - 1, height - 1);
            var statics = staticBlock.Statics;

            if (statics.Length < 64)
                Array.Resize(ref statics, 64);

            short index = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ref var st = ref statics[index++];
                    st.TileId = (short)(index + 100);
                    st.X = (byte)x;
                    st.Y = (byte)y;
                    st.Z = realHeight;
                }
            }

            if (staticBlock.Lookup == -1)
                staticBlock.Lookup = 0;

            map.Save();
        }
    }
}
