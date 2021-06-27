using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Ultima
{
    public class IDX : UOFile
    {
        public List<Index> Indices { get; }

        public IDX(string path) : base(path)
        {
            Indices = new List<Index>();
        }

        public override void Load()
        {
            try
            {
                Indices.Clear();

                Open();

                using BinaryReader reader = new BinaryReader(_stream);
                int indexLength = 3 * 4;

                while(_stream.Position + indexLength < _stream.Length)
                {
                    int lookup = reader.ReadInt32();
                    int length = reader.ReadInt32();
                    int extra = reader.ReadInt32();

                    Indices.Add(new Index(lookup, length, extra));
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

                using BinaryWriter writer = new BinaryWriter(_stream);

                for (int i = 0; i < Indices.Count; i++)
                {
                    Index index = Indices[i];

                    writer.Write(index.Lookup);
                    writer.Write(index.Length);
                    writer.Write(index.Extra);
                }

                writer.Flush();
            }
            finally
            {
                Close();
            }
        }
    }
    
    public struct Index
    {
        public int Lookup { get; set; }
        public int Length { get; set; }
        public int Extra { get; set; }

        public Index(int lookup, int length, int extra) : this()
        {
            Lookup = lookup;
            Length = length;
            Extra = extra;
        }
    }
}
