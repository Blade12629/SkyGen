using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Adobe
{
    public class ACOWriter : IDisposable
    {
        public bool IsDisposed { get; private set; }

        FileStream _stream;

        public ACOWriter(string path)
        {
            _stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
        }

        ~ACOWriter()
        {
            Dispose(false);
        }

        public void WritePalette(Dictionary<Color, string> colors)
        {
            Write((short)0); // version
            Write((short)colors.Count);

            for (int i = 0; i < colors.Count; i++)
            {
                var pair = colors.ElementAt(i);

                Write((short)0); // colorspace (rgb)
                Write((ushort)(pair.Key.R * 256));
                Write((ushort)(pair.Key.G * 256));
                Write((ushort)(pair.Key.B * 256));

                Write(pair.Value);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {
                IsDisposed = true;

                _stream?.Dispose();
                _stream = null;
            }
        }

        void Write(byte b)
        {
            _stream.WriteByte(b);
        }

        void Write(params byte[] b)
        {
            if (b == null)
                return;

            _stream.Write(b);
        }

        void Write(short v)
        {
            Write((byte)(v >> 8),
                  (byte)v);
        }

        void Write(ushort v)
        {
            Write((byte)(v >> 8),
                  (byte)v);
        }

        void Write(int v)
        {
            Write((byte)(v >> 32),
                  (byte)(v >> 24),
                  (byte)(v >> 16),
                  (byte)(v >> 8),
                  (byte)v);
        }

        void Write(string v)
        {
            Write(Encoding.BigEndianUnicode.GetBytes(v));
        }
    }
}
