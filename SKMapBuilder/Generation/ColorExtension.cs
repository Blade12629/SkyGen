using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Generation
{
    public static class ColorExtension
    {
        public static uint ToKey(this Color c)
        {
            return (uint)(c.R |
                         (c.G << 8) |
                         (c.B << 16) |
                         (c.A << 24));
        }

        public static Color ToColor(this uint v)
        {
            return Color.FromArgb((int)(v >> 24) & 0xFF,
                                  (int)v & 0xFF,
                                  (int)(v >> 8) & 0xFF,
                                  (int)(v >> 16) & 0xFF);
        }
    }
}
