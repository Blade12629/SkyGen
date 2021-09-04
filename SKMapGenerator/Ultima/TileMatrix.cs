using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Ultima
{
    public class TileMatrix : UOFile
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BlockWidth { get; private set; }
        public int BlockHeight { get; private set; }

        TileBlock[] _tileBlocks;

        public TileMatrix(string path, int width, int height) : base(path)
        {
            Width = width;
            Height = height;

            BlockWidth = width >> 3;
            BlockHeight = height >> 3;
        }

        public TileMatrix(int width, int height, short defaultTileId, sbyte defaultZ) : this(null, width, height)
        {
            TileBlock[] blocks = new TileBlock[BlockWidth * BlockHeight];
            int index = 0;
            int header = 0;

            for (int x = 0; x < BlockWidth; x++)
            {
                for (int y = 0; y < BlockHeight; y++)
                {
                    Tile[] tiles = new Tile[64];

                    for (int i = 0; i < tiles.Length; i++)
                        tiles[i] = new Tile(defaultTileId, defaultZ);

                    blocks[index++] = new TileBlock(header, tiles);
                }
            }

            _tileBlocks = blocks;
        }

        public void AddBlock(TileBlock block)
        {
            Array.Resize(ref _tileBlocks, _tileBlocks.Length + 1);
            _tileBlocks[_tileBlocks.Length - 1] = block;
        }

        public void SetAllTiles(short tileId, sbyte z)
        {
            for (int i = 0; i < _tileBlocks.Length; i++)
            {
                ref TileBlock block = ref _tileBlocks[i];

                for (int x = 0; x < block.Tiles.Length; x++)
                {
                    ref Tile tile = ref block.Tiles[x];

                    tile.TileId = tileId;
                    tile.Z = z;
                }
            }
        }

        public ref TileBlock GetBlock(int x, int y)
        {
            return ref _tileBlocks[(x >> 3) * BlockHeight + (y >> 3)];
        }

        public ref Tile GetTile(int x, int y)
        {
            ref TileBlock block = ref GetBlock(x, y);
            Tile[] tiles = block.Tiles;

            return ref tiles[((y & 0x7) << 3) + (x & 0x7)];
        }

        public override void Load()
        {
            try
            {
                Open();

                using (BinaryReader reader = new BinaryReader(_stream))
                {
                    List<TileBlock> tileBlocks = new List<TileBlock>();

                    int lengthPerBlock = 4 + (64 * 3);

                    while (_stream.Position + lengthPerBlock <= _stream.Length)
                    {
                        int header = reader.ReadInt32();
                        Tile[] tiles = new Tile[64];

                        for (int i = 0; i < tiles.Length; i++)
                        {
                            short id = reader.ReadInt16();
                            sbyte z = reader.ReadSByte();

                            tiles[i] = new Tile(id, z);
                        }

                        tileBlocks.Add(new TileBlock(header, tiles));
                    }

                    _tileBlocks = tileBlocks.ToArray();
                }
            }
            finally
            {
                Close();
            }
        }

        public override void Save()
        {
            try
            {
                Create();

                using (BinaryWriter writer = new BinaryWriter(_stream))
                {
                    TileBlock[] blocks = _tileBlocks;

                    for (int i = 0; i < blocks.Length; i++)
                    {
                        ref TileBlock block = ref blocks[i];
                        Tile[] tiles = block.Tiles;

                        writer.Write(block.Header);

                        for (int x = 0; x < tiles.Length; x++)
                        {
                            ref Tile tile = ref tiles[x];

                            writer.Write(tile.TileId);
                            writer.Write(tile.Z);
                        }
                    }

                    writer.Flush();
                }
            }
            finally
            {
                Close();
            }
        }
    }

    public struct TileBlock
    {
        public int Header { get; set; }
        public Tile[] Tiles { get; set; }

        public TileBlock(int header, Tile[] tiles) : this()
        {
            Header = header;
            Tiles = tiles;
        }
    }

    public struct Tile
    {
        public short TileId { get; set; }
        public sbyte Z { get; set; }

        public Tile(short tileId, sbyte z) : this()
        {
            TileId = tileId;
            Z = z;
        }
    }

    public struct Static : IEquatable<Static>
    {
        public ushort TileId { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public sbyte Z { get; set; }
        public short Hue { get; set; }
        public bool WriteZeroStaticId { get; set; }
        public bool IsNullRef { get; set; }

        public Static(ushort tileId, byte x, byte y, sbyte z, short hue, bool writeZeroStaticId = false)
        {
            TileId = tileId;
            X = x;
            Y = y;
            Z = z;
            Hue = hue;
            WriteZeroStaticId = writeZeroStaticId;
            IsNullRef = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Static @static && Equals(@static);
        }

        public bool Equals(Static other)
        {
            return TileId == other.TileId &&
                   X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            int hashCode = -59855996;
            hashCode = hashCode * -1521134295 + TileId.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Static left, Static right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Static left, Static right)
        {
            return !(left == right);
        }
    }
}
