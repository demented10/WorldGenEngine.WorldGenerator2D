using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.WorldGenerator2D.Services.Caching;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Factories
{
    public static class CachingServiceFactory
    {
        public static IChunkCachingService CreateCachingService()
        {
            // Здесь можно добавить логику для выбора конкретной реализации кэширования
            // Например, на основе конфигурации или других факторов
            return new MemoryCachingService();
        }
    }
}
