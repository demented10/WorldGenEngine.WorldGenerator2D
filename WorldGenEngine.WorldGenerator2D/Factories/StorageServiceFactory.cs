using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using WorldGenEngine.WorldGenerator2D.Services.Statements;
using WorldGenEngine.WorldGenerator2D.Services.Storage;

namespace WorldGenEngine.WorldGenerator2D.Factories
{
    public static class StorageServiceFactory
    {
        public static IChunkStorageService CreateStorageService()
        {
            // Здесь можно добавить логику для выбора конкретной реализации хранилища
            // Например, на основе конфигурации или других факторов
            return new InMemoryChunkStorage();
        }
    }
}
