using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SKMapGenerator
{
    /// <summary>
    /// Use this class inside of servuo to write a .saa file
    /// </summary>
    public class ServUOStaticsWriter
    {
        public string SavePath
        {
            get => _path;
            set => _path = value;
        }

        Dictionary<(int, int, int), (ushort, short)> _statics;
        string _path;

        /// <summary>
        /// Use this class inside of servuo to write a .saa file
        /// </summary>
        public ServUOStaticsWriter(string path)
        {
            _path = path;
            _statics = new Dictionary<(int, int, int), (ushort, short)>();
        }

        public void AddStatic(int x, int y, int z, ushort tileId, short hue = 0)
        {
            UpdateStatic(x, y, z, tileId, hue);
        }

        public void DeleteStatic(int x, int y, int z)
        {
            _statics.Remove((x, y, z));
        }

        public void UpdateStatic(int x, int y, int z, ushort tileId, short hue)
        {
            _statics[(x, y, z)] = (tileId, hue);
        }

        public void Reset()
        {
            _statics = null;
        }

        public void Save()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var pair in _statics)
            {
                sb.AppendLine($"{pair.Key.Item1},{pair.Key.Item2},{pair.Key.Item3},{pair.Value.Item1},{pair.Value.Item2}");
            }

            File.WriteAllText(_path, sb.ToString());
        }
    }
}
