using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Generation
{
    public class ColorStore
    {
        Dictionary<int, int> _storage;

        public ColorStore(int startCapacity)
        {
            _storage = new Dictionary<int, int>(startCapacity);
        }

        public int this[int color]
        {
            get
            {
                _ = TryGet(color, out int value);
                return value;
            }
            set
            {
                Update(color, value);
            }
        }

        public bool Add(int color, int value)
        {
            return _storage.TryAdd(color, value);
        }

        public void AddRange(params (int, int)[] values)
        {
            if (values == null)
                return;

            for (int i = 0; i < values.Length; i++)
                Add(values[i].Item1, values[i].Item2);
        }

        public bool TryGet(int color, out int value)
        {
            return _storage.TryGetValue(color, out value);
        }

        public void Update(int color, int value)
        {
            _storage[color] = value;
        }

        public void Clear()
        {
            _storage.Clear();
        }

        public void Save(string path)
        {
            using System.IO.FileStream fstream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            using System.IO.StreamWriter w = new System.IO.StreamWriter(fstream, Encoding.UTF8);

            w.WriteLine("RGBA = Value");

            for (int i = 0; i < _storage.Count; i++)
            {
                var storagePair = _storage.ElementAt(i);
                w.WriteLine($"0x{storagePair.Key.ToString("X")} = {storagePair.Value}");
            }

            w.Flush();
        }

        public void Load(string path)
        {
            using System.IO.FileStream fstream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            using System.IO.StreamReader reader = new System.IO.StreamReader(fstream, Encoding.UTF8);

            if (!reader.EndOfStream) // first line is just an explenation for the formatting
                _ = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace(" ", "");

                if (string.IsNullOrEmpty(line))
                    continue;

                int index = line.IndexOf('=');

                if (index == -1)
                    continue;

                int colorValue = int.Parse(line.Substring(2, index - 2), System.Globalization.NumberStyles.HexNumber);
                int value = int.Parse(line.Remove(0, index + 1));

                this[colorValue] = value;
            }
        }
    }
}
