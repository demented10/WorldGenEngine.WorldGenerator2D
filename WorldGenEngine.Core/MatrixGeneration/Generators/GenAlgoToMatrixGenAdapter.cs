using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Algorithms;

namespace WorldGenEngine.Core.MatrixGeneration.Generators
{
    /// <summary>
    /// Adapter for the IGenerationAlgo interface to IBinaryMatrixGenerator
    /// </summary>
    internal class GenAlgoToMatrixGenAdapter : IBinaryMatrixGenerator
    {
        private readonly IGenerationAlgo _generationAlgo;

        public GenAlgoToMatrixGenAdapter(IGenerationAlgo generationAlgo)
        {
            _generationAlgo = generationAlgo;
        }

        public bool[] GenerateMatrix(int width, int height, int? seed = null)
        {
            var map = _generationAlgo.GenerateMap(width, height);
            bool[] matrix = new bool[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[y * width + x] = map[x, y];
                }
            }
            return matrix;
        }
    }
}
