using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace WorldGenEngine.Core.GenerationMethods.Configs
{
    public interface IPerlinNoiseConfig
    {
        float Scale { get; }
        float Threshold { get; }
        int Octaves { get; }
        float Persistence { get; }
        float Lacunarity { get; }
        float VerticalBias { get; }
        int Seed { get; }
    }
}
