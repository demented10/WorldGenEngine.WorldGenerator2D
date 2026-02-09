using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Algorithms;
using WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces;

namespace WorldGenEngine.WorldGenerator2D.LocationGenerator.CaveGeneration
{
    /// <summary>
    /// Класс для генерации пещер с использованием заданного алгоритма генерации
    /// </summary>
    internal class CaveGenerator(IGenerationAlgo generationAlgo) : ICaveGenerator
    {
        /// <summary>
        /// Метод для генерации тепловой карты мира с пещерами
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns></returns>
        public bool[,] WorldHeatMap(int sizeX, int sizeY)
        {
            return generationAlgo.GenerateMap(sizeX, sizeY);
        }

    }
}
