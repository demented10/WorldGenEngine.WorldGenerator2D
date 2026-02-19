
using WorldGenEngine.Core.GenerationMethods.Algorithms;
using WorldGenEngine.Core.GenerationMethods.Configs;
using WorldGenEngine.Core.MatrixGeneration.Generators;

namespace WorldGenEngine.Core.MatrixGeneration.Factories
{
    /// <summary>
    /// Factory for creating instances of binary matrix generators
    /// </summary>
    public static class MatrixGeneratorsFactory
    {
        /// <summary>
        /// Create default binary matrix generator as Random Matrix Generator with default args
        /// </summary>
        /// <returns></returns>
        public static IBinaryMatrixGenerator CreateDefault() => new RandomMatrixGenerator();
        /// <summary>
        /// Create Perlin Noise based binary matrix generator
        /// </summary>
        /// <returns></returns>
       
    }
}
