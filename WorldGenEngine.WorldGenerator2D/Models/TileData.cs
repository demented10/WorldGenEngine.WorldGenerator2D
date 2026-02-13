using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGenEngine.WorldGenerator2D.Models
{
    public struct TileData
    {
        public TileData(int tileId, bool isSolid)
        {
            TileId = tileId;
            IsSolid = isSolid;
        }

        public int TileId { get; }
        public bool IsSolid {get;set;}

    }
}