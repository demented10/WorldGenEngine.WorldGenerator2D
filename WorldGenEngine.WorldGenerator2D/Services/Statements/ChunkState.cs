using System;
using System.Collections.Generic;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    [Serializable]
    internal class ChunkState : IEquatable<ChunkState>
    {
        public readonly ChunkPosition Position;
        public readonly ChunkData Data;

        public List<TileData> TilesMetadata;

        public ChunkState(ChunkPosition position, ChunkData data)
        {
            Position = position;
            Data = data;
            TilesMetadata = new List<TileData>(ChunkData.ChunkSize*ChunkData.ChunkSize);
        }

        public bool Equals(ChunkState? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Position.Equals(other.Position);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ChunkState)obj);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }


}