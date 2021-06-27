using SKMapGenerator.Ultima;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Generation
{
    public class StaticGenerator
    {
        public void Generate(StaticApplyArray saa, StaticTileMatrix stm)
        {
            Static nullRefStatic = default;
            nullRefStatic.IsNullRef = true;

            for (int i = 0; i < saa.Entries.Length; i++)
            {
                StaticApplyEntry entry = saa.Entries[i];

                ref StaticBlock block = ref stm.GetStaticBlock(entry.X, entry.Y);
                Static[] statics = block.Statics;
                ref Static st = ref nullRefStatic;

                // Try to find the next free cell or extend the array by 1 cell and use the newest cell
                for (int x = 0; x < statics.Length; x++)
                {
                    ref Static sst = ref statics[x];

                    if (sst.TileId == 0 &&
                        sst.X == 0 &&
                        sst.Y == 0 &&
                        sst.Z == 0)
                    {
                        st = ref sst;
                    }
                }

                if (st.IsNullRef)
                {
                    Array.Resize(ref statics, statics.Length + 1);
                    st = ref statics[^1];
                }

                // required so we don't skip the block when saving it
                block.Lookup = 0;

                st.TileId = entry.StaticId;
                st.X = (byte)(entry.X % 8);
                st.Y = (byte)(entry.Y % 8);
                st.Z = entry.Z;

                if (entry.StaticId == 0)
                    st.WriteZeroStaticId = true;
            }
        }
    }
}
