using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Algorithms;

namespace WorldGenEngine.Core.MatrixGeneration.Generators
{
    internal class PerlinNoiseMatrixGenerator : IBinaryMatrixGenerator, IHeightMapMatrixGenerator
    {

        private readonly IGenerationAlgo _perlinNoiseGenerator;

        public PerlinNoiseMatrixGenerator(IGenerationAlgo perlinNoiseGenerator)
        {
            _perlinNoiseGenerator = perlinNoiseGenerator;
        }


        public bool[] GenerateMatrix(int width, int height, int? seed = null)
        {
            seed ??= 0;
            var matrix = new bool[width * height];
            var values = _perlinNoiseGenerator.GenerateMap(width, height,seed: seed.Value);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[y * width + x] = values[x,y];
                }
            }

            return matrix;
        }

        public float[] GenerateHeightMap(int width, int height, int? seed = null)
        {
            seed ??= 0;
            var matrix = new float[width * height];
            var values = _perlinNoiseGenerator.GenerateHeightMap(width, height, seed: seed.Value);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[y * width + x] = values[x, y];
                }
            }

            return matrix;
        }
    }
}
