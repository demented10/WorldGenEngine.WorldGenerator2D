using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces
{
    public record OrePlacementData(bool[,] map, IOre ore);
    public interface IOresGenerator
    {
        List<OrePlacementData> GenerateOresMap(List<OreGenerationMetadata> oreGenerationConfig);
    }
}
