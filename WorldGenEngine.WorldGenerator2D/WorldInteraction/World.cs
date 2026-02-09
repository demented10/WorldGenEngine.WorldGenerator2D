using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.Position;
using WorldGenEngine.WorldGenerator2D.WorldInteraction.Entity;

namespace WorldGenEngine.WorldGenerator2D.WorldInteraction
{
    internal class World //: IWorldGetProperties
    {
        BlockWorld[] BlockWorlds { get; set; }

        public BlockWorld GetBlock(IPoint position)
        {
            throw new NotImplementedException();
        }

        public BlockWorld[] GetBlockWorlds()
        {
            throw new NotImplementedException();
        }
    }
}
