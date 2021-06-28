using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Generation.ColorStore cs = new Generation.ColorStore(255);

            for (int i = 0; i < 249; i++)
            {
                cs.Add(System.Drawing.Color.FromArgb(255, i, i, i), -124 + i);
            }

            cs.Save("heightmap.grayscale.colorstore");

            Environment.Exit(0);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
