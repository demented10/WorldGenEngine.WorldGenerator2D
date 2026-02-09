using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.Factory;
using WorldGenEngine.WorldGenerator2D.Position;
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
        private readonly IGenerationConfig _generationConfig;
        private readonly Size2i _size;

        public WorldGenerator(IGenerationConfig generationConfig, IOresGenerator oresGenerator, ICaveGenerator caveGenerator, Size2i size)
        {
            _generationConfig = generationConfig;
            _oresGenerator = oresGenerator;
            _caveGenerator = caveGenerator;
            _size = size;
        }
        /// <summary>
        /// Генерация мира. Тут происходит объединение всех сгенерированных карт. И преобразование к свойствам мира
        /// </summary>
        /// <returns></returns>
        public WorldProperties GenerateWorld()
        {
            var pregeneratedHeatMap = _caveGenerator.WorldHeatMap(_size.Width, _size.Height);
            var oreMaps = _oresGenerator.GenerateOresMap(_generationConfig.GetGenerationMetadata);
            return new WorldProperties();
        }
    }
}
