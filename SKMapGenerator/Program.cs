using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKMapGenerator.Generation;
using SKMapGenerator.Ultima;

namespace SKMapGenerator
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ColorStore csHeights = ColorStore.GetReverseHeightCS();
            //ColorStore csTiles = ColorStore.GetReverseTileCS();

            //TileMatrix matrix = new TileMatrix(@"D:\Electronic Arts\ClassicUO\map0.mul", 7168, 4096);
            //matrix.Load();

            //TileMapGenerator gen = new TileMapGenerator();
            //gen.Export(csTiles, matrix, @"D:\reposSSD\SKUOEdit\Test\reversedTiles.bmp");

            //HeightMapGenerator gen2 = new HeightMapGenerator();
            //gen2.Export(csHeights, matrix, @"D:\reposSSD\SKUOEdit\Test\reversedHeights.bmp");

            //Environment.Exit(0);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
