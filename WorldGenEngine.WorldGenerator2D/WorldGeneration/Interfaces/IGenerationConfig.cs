using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces
{
    public interface IGenerationConfig
    {
        public List<OreGenerationMetadata> GetGenerationMetadata { get; }
    }
}
