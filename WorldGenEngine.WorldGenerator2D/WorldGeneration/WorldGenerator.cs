using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.Factory;
using WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces;
using WorldGenEngine.WorldGenerator2D.WorldInteraction;

namespace WorldGenEngine.WorldGenerator2D.WorldGeneration
{
    /// <summary>
    /// Основной класс для генерации мира
    /// </summary>
    internal class WorldGenerator : IWorldGenerator
    {
        private readonly IOresGenerator _oresGenerator;
        private readonly ICaveGenerator _caveGenerator;
        
        public WorldGenerator(IOresGenerator oresGenerator, ICaveGenerator caveGenerator)
        {
            _oresGenerator = oresGenerator;
            _caveGenerator = caveGenerator;
        }

        public IWorldGetProperties GenerateWorld()
        {
            throw new NotImplementedException();
        }
    }
}
