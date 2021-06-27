using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Ultima
{
    public class Map
    {
        public TileMatrix Tiles { get; set; }
        public StaticTileMatrix StaticTiles { get; set; }

        public Map(string mapFile, string staticsFile, string staticsIdxFile, int width, int height)
        {
            Tiles = new TileMatrix(mapFile, width, height);
            StaticTiles = new StaticTileMatrix(staticsFile, staticsIdxFile, width, height);
        }

        public Map(int width, int height, short tilesDefaultTileId, sbyte tilesDefaultZ)
        {
            Tiles = new TileMatrix(width, height, tilesDefaultTileId, tilesDefaultZ);
            StaticTiles = new StaticTileMatrix(width, height);
        }

        public void SetPaths(string mapFile, string staticsFile, string staticsIdxFile)
        {
            Tiles.Path = mapFile;
            StaticTiles.Path = staticsFile;
            StaticTiles.IdxPath = staticsIdxFile;
        }

        public void Load()
        {
            Tiles.Load();
            StaticTiles.Load();
        }

        public void Save()
        {
            Tiles.Save();
            StaticTiles.Save();
        }
    }
}
