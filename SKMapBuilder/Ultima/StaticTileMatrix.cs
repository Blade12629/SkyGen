using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Ultima
{
    public class StaticTileMatrix : UOFile
    {
        public string IdxPath { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BlockWidth { get; private set; }
        public int BlockHeight { get; private set; }

        StaticBlock[] _staticBlocks;

        public StaticTileMatrix(string path, string idxPath, int width, int height) : base(path)
        {
            IdxPath = idxPath;
            Width = width;
            Height = height;

            BlockWidth = width >> 3;
            BlockHeight = height >> 3;
        }

        public StaticTileMatrix(int width, int height) : this(null, null, width, height)
        {
            int total = BlockWidth * BlockHeight;
            StaticBlock[] blocks = new StaticBlock[total];

            for (int i = 0; i < total; i++)
            {
                blocks[i] = new StaticBlock(-1, new Static[64]);
            }

            _staticBlocks = blocks;
        }

        public ref StaticBlock GetStaticBlock(int x, int y)
        {
            return ref _staticBlocks[(x >> 3) * BlockHeight + (y >> 3)];
        }

        public ref Static GetStaticTile(int x, int y)
        {
            ref StaticBlock block = ref GetStaticBlock(x, y);

            return ref block.Statics[((y & 0x7) << 3) + (x & 0x7)];
        }

        public override void Load()
        {
            try
            {
                Open();

                using BinaryReader reader = new BinaryReader(_stream);
                using IDX idx = new IDX(IdxPath);
                idx.Load();

                List<StaticBlock> staticBlocks = new List<StaticBlock>();
                Index[] indices = idx.Indices.OrderBy(e => e.Lookup).ToArray();

                for (int i = 0; i < indices.Length; i++)
                {
                    ref Index index = ref indices[i];

                    if (index.Lookup == -1)
                    {
                        staticBlocks.Add(new StaticBlock(-1, Array.Empty<Static>()));
                        continue;
                    }

                    Static[] statics = new Static[index.Length / 7];

                    for (int x = 0; x < statics.Length; x++)
                    {
                        short tileId = reader.ReadInt16();
                        byte x_ = reader.ReadByte();
                        byte y = reader.ReadByte();
                        sbyte z = reader.ReadSByte();
                        short unkown = reader.ReadInt16();

                        statics[x] = new Static(tileId, x_, y, z, unkown);
                    }

                    staticBlocks.Add(new StaticBlock(index.Lookup, statics.OrderBy(s => s.X * BlockHeight + s.Y).ToArray()));
                }

                _staticBlocks = staticBlocks.OrderBy(b => b.Lookup).ToArray();
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

                using BinaryWriter writer = new BinaryWriter(_stream);
                using IDX idx = new IDX(IdxPath);

                StaticBlock[] staticBlocks = _staticBlocks;

                for (int i = 0; i < staticBlocks.Length; i++)
                {
                    ref StaticBlock block = ref staticBlocks[i];

                    int length = block.Statics.Length * 7;
                    int start = (int)_stream.Position;

                    if (block.Lookup == -1)
                    {
                        idx.Indices.Add(new Index(-1, -1, -1));
                    }
                    else
                    {
                        idx.Indices.Add(new Index(start, length, 0));
                    }

                    for (int x = 0; x < block.Statics.Length; x++)
                    {
                        ref Static @static = ref block.Statics[x];

                        writer.Write(@static.TileId);
                        writer.Write(@static.X);
                        writer.Write(@static.Y);
                        writer.Write(@static.Z);
                        writer.Write(@static.Unkown);
                    }
                }

                idx.Save();
                writer.Flush();
            }
            finally
            {
                Close();
            }
        }
    }

    public struct StaticBlock
    {
        /// <summary>
        /// -1 if empty, otherwise last lookup or 0
        /// </summary>
        public int Lookup { get; set; }
        public Static[] Statics { get; set; }

        public StaticBlock(int lookup, Static[] statics) : this()
        {
            Lookup = lookup;
            Statics = statics;
        }
    }
}
