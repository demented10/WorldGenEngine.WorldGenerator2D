using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.Noise.Config;
using WorldGenEngine.WorldGenerator2D;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D.Factories;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.Tests
{
    public class WorldStateFactoryTest
    {
        [Fact]
        public void WorldState_ObjectTypeOf_TestFactory()
        {
            var noiseConfig = new NoiseChunkGeneratorConfig
            {
                FractalNoiseConfig = new FractalNoiseServiceConfig(),
            };
            var stateOptions = new WorldStateOptions
            {
               
            };
            var generationOptions = new WorldGenerationOptions()
            {

            };


            var worldstate = WorldGenerator2D.Factories.WorldStateFactory.CreateWorldState(
                generationAlgo: WorldGenerator2D.Factories.ChunkGenerationFactory.CreateNoiseChunkGenerator(
                    noiseConfig),
                chunkStorageService: WorldGenerator2D.Factories.StorageServiceFactory.CreateStorageService(),
                options: stateOptions,
                generationOptions: generationOptions,
                chunkCachingService: WorldGenerator2D.Factories.CachingServiceFactory.CreateCachingService());

            Assert.IsType<WorldState>(worldstate);
        }
    }
}
