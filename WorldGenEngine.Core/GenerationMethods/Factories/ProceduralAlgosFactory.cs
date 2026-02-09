using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Algorithms;

namespace WorldGenEngine.Core.GenerationMethods.Factories
{
    public static class ProceduralAlgosFactory
    {
        public static IGenerationAlgo CreateDefault() => new PerlinNoiseGenerator();
    }
}
