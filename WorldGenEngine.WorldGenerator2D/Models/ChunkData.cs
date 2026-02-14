using System;

namespace WorldGenEngine.WorldGenerator2D.Models
{
    [Serializable]
    public class ChunkData
    {
        public TileData[] Tiles {get;}
        public const int ChunkSize = 64;
        public ChunkData()
        {
            Tiles = new TileData[ChunkSize * ChunkSize];
        }

        public void SetTileByPos(int xLocalPos, int yLocalPos, TileData tileData)
        {
            Tiles[yLocalPos * ChunkSize + xLocalPos] = tileData;
        }
        public void SetTileByIndex(int index, TileData tileData)
        {
            Tiles[index] = tileData;
        }

        public ChunkData(TileData[] tiles)
        {
            Tiles = tiles;
        }
    }
}