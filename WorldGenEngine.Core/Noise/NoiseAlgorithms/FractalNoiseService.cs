using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using WorldGenEngine.Core.Noise.Config;

namespace WorldGenEngine.Core.Noise
{
    public class FractalNoiseService
    {
        private readonly INoise _noise;
        private readonly FractalNoiseServiceConfig _config;

        public FractalNoiseService(INoise noise, FractalNoiseServiceConfig config)
        {
            _noise = noise;
            _config = config;
        }


        [ActivatorUtilitiesConstructor]
        public FractalNoiseService(INoise noise, IOptions<FractalNoiseServiceConfig> config)
        {
            _noise = noise;
            _config = config.Value;
        }

        public float FractalNoise(float fx, float fy, int seed)
        {
            float amplitude = _config.Lacunarity; // сила применения шума к общей картине, будет уменьшаться с "мельчанием" шума
            // как сильно уменьшаться - регулирует persistence
            float max = 0; // необходимо для нормализации результата
            float result = 0; // накопитель результата

            var octaves = _config.Octaves;

            while (octaves-- > 0)
            {
                max += amplitude;
                result += _noise.GenerateNoise2D(fx, fy, seed) * amplitude;
                amplitude *= _config.Persistence;
                fx *= 2; // удваиваем частоту шума (делаем его более мелким) с каждой октавой
                fy *= 2;
            }

            return result / max;
        }
    }
}
