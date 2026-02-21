
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WorldGenEngine.Core.Noise;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Generation;

namespace WorldGenEngine.WorldGenerator2D.Algorithms
{
    internal class PerlinNoiseChunkGenerator : IChunkGenerationAlgo
    {

        private readonly FractalNoiseService _fractalNoiseService;
        private readonly NoiseChunkGeneratorConfig _config;


        public PerlinNoiseChunkGenerator(FractalNoiseService fractalNoiseService, NoiseChunkGeneratorConfig config)
        {
            _fractalNoiseService = fractalNoiseService;
            _config = config;
        }
        [ActivatorUtilitiesConstructor]
        public PerlinNoiseChunkGenerator(FractalNoiseService fractalNoiseService, IOptions<NoiseChunkGeneratorConfig> config)
        {
            _fractalNoiseService = fractalNoiseService;
            _config = config.Value;
        }


        private TileData generateTileData(int x, int y, int seed)
        {

            float value = _fractalNoiseService.FractalNoise(x*_config.Scale, y*_config.Scale, seed);

            TileData tileData = new TileData(1, value > _config.Threshold);

            return tileData;
        }

        public ChunkData GenerateChunk(ChunkPosition position, int seed = 0)
        {
            var tiles = new TileData[ChunkData.ChunkSize * ChunkData.ChunkSize];

            for (int y = 0; y < ChunkData.ChunkSize; y++)
            {
                for (int x = 0; x < ChunkData.ChunkSize; x++)
                {
                    int globalX = position.ChunkXPos * ChunkData.ChunkSize + x;
                    int globalY = position.ChunkYPos * ChunkData.ChunkSize + y;
                    tiles[y * ChunkData.ChunkSize + x] = generateTileData(globalX,globalY, seed);
                }
            }

            var data = new ChunkData(tiles);
            return data;
        }
    }
}