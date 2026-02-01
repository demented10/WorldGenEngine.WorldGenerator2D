using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces;
using WorldGenEngine.WorldGenerator2D;

namespace WorldGenEngine.WorldGenerator2D.CaveGenerator
{
    internal class CaveGenerator : ICaveGenerator
    {
        private readonly IMapGenerator _mapGenerator;

        public CaveGenerator(IMapGenerator mapGenerator)
        {
            _mapGenerator = mapGenerator;
        }

        public bool[,] worldHeatMap(int sizeX, int sizeY)
        {
            return _mapGenerator.GenerateMap(sizeX, sizeY);
        }
    }
}
