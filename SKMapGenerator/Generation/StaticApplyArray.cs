using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Generation
{
    public class StaticApplyArray
    {
        public StaticApplyEntry[] Entries { get; set; }

        public StaticApplyArray(StaticApplyEntry[] entries)
        {
            Entries = entries;
        }

        public StaticApplyArray()
        {
        }


        public static StaticApplyArray Load(string path)
        {
            string json = System.IO.File.ReadAllText(path);
            StaticApplyArray saa = null;
            List<StaticApplyEntry> entries = new List<StaticApplyEntry>();

            foreach (string line in json.Split(Environment.NewLine))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] lineSplit = line.Replace(" ", "").Split(',');

                int x = int.Parse(lineSplit[0]);
                int y = int.Parse(lineSplit[1]);
                sbyte z = sbyte.Parse(lineSplit[2]);
                ushort staticId = ushort.Parse(lineSplit[3]);
                short hue = short.Parse(lineSplit[4]);

                entries.Add(new StaticApplyEntry(x, y, z, staticId, hue));
            }

            if (entries.Count > 0)
                saa = new StaticApplyArray(entries.ToArray());
            
            return saa;
        }
    }

    public class StaticApplyEntry
    {
        public int X { get; set; }
        public int Y { get; set; }
        public sbyte Z { get; set; }
        public short Hue { get; set; }

        public ushort StaticId { get; set; }

        public StaticApplyEntry(int x, int y, sbyte z, ushort staticId, short hue)
        {
            X = x;
            Y = y;
            Z = z;
            StaticId = staticId;
            Hue = hue;
        }

        public StaticApplyEntry()
        {
        }
    }
}
