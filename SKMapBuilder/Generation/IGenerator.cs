using SKMapGenerator.Ultima;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Generation
{
    public interface IGenerator
    {
        public void Generate(ColorStore cs, Bitmap bmp, TileMatrix tileMatrix);
    }
}
