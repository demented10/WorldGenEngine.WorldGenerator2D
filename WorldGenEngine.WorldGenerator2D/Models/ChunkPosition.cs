using System;

namespace WorldGenEngine.WorldGenerator2D.Models
{
    public readonly struct ChunkPosition : IEquatable<ChunkPosition>
    {
        public int ChunkXPos {get;}
        public int ChunkYPos {get;}

        public ChunkPosition(int x, int y)
        {
            ChunkXPos = x;
            ChunkYPos = y;
        }

        public bool Equals(ChunkPosition other)
        {
            return ChunkXPos == other.ChunkXPos && ChunkYPos == other.ChunkYPos;
        }

        public override bool Equals(object? obj)
        {
            return obj is ChunkPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ChunkXPos, ChunkYPos);
        }
    }

}