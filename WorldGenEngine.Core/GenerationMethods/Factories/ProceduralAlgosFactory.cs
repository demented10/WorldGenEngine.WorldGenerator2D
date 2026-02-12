using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Algorithms;
using WorldGenEngine.Core.GenerationMethods.Configs;

namespace WorldGenEngine.Core.GenerationMethods.Factories
{
    public static class ProceduralAlgosFactory
    {
        public static IGenerationAlgo CreateDefault() => new PerlinNoiseGenerator(PerlinNoiseConfigFactory.GetDefaultConfig());
        public static IGenerationAlgo CreatePerlinNoiseAlgo(IPerlinNoiseConfig config) => new PerlinNoiseGenerator(config);
    }
}
