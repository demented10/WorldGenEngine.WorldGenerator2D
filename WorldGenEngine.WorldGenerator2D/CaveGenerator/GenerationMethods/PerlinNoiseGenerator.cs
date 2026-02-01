namespace WorldGenEngine.WorldGenerator2D.CaveGenerator.GenerationMethods
{
    class PerlinNoiseGenerator : IMapGenerator
    {
        private float _scale = 0.1f; //масштаб шума (чем меньше, тем больше пещеры)
        private float _threshold = 0.0f; //порог для определения стен и проходов
        private int _octaves = 4; //количество октав
        private float _persistence = 0.5f; //влияние каждой октавы
        private float _lacunarity = 2.0f; //частота каждой октавы
        private bool _useCellularAutomation = true; //флаг для применения клеточного автомата

        public PerlinNoiseGenerator(float scale, float threshold, int octaves, float persistence, float lacunarity, bool useCellularAutomation)
        {
            _scale = scale;
            _threshold = threshold;
            _octaves = octaves;
            _persistence = persistence;
            _lacunarity = lacunarity;
            _useCellularAutomation = useCellularAutomation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private float[] GetPseudoRandomGradientVector(int x, int y)
        {
            // псевдо-случайное число от 0 до 3 которое всегда неизменно при данных x и y
            int v = (int)(x * 1836311903 ^ y * 2971215073) & 3;

            switch (v)
            {
                case 0: return new float[] { 1, 0 };
                case 1: return new float[] { -1, 0 };
                case 2: return new float[] { 0,1};
                default: return new float[] {0,-1};
            }
        }

        /// <summary>
        /// Метод для вычисления влияния градиентного вектора в узле сетки на заданную точку (x, y).
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private float CalculateGradientInfluence(int gridX, int gridY, float x, float y)
        {
            float dx = x - gridX;
            float dy = y - gridY;

            float[] gradient = GetPseudoRandomGradientVector(gridX, gridY);

            return Utils.Math.Dot(gradient, new float[] { dx, dy });
        }

        /// <summary>
        /// Метод для генерации Перлин шума в заданной точке (x, y). 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private float PerlinNoise(float x, float y)
        {
            //определяем углы квадрата, в котором находится точка
            int x0 = (int)Math.Floor(x);
            int x1 = x0 + 1;
            int y0 = (int)Math.Floor(y);
            int y1 = y0 + 1;

            //дробные части координат
            float sx = x - x0;
            float sy = y - y0;

            //Вычисляем влияние градиентов в углах квадрата
            float n0 = CalculateGradientInfluence(x0, y0, x, y);
            float n1 = CalculateGradientInfluence(x1, y0, x, y);
            float n2 = CalculateGradientInfluence(x0, y1, x, y);
            float n3 = CalculateGradientInfluence(x1, y1, x, y);

            //Интерполяция с использованием quantic curve
            float ix0 = Utils.Math.Lerp(n0, n1, Utils.Math.QuanticCurve(sx));
            float ix1 = Utils.Math.Lerp(n2, n3, Utils.Math.QuanticCurve(sx));

            return Utils.Math.Lerp(ix0, ix1, Utils.Math.QuanticCurve(sy));
        }
        /// <summary>
        /// Метод для генерации фрактального шума на основе Перлин шума. Используется для создания более сложных и детализированных текстур шума.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private float FractalNoise(float x, float y)
        {
            float total = 0;
            float frequency = 1;
            float amplitude = 1;
            float maxValue = 0; // используется для нормализации результата
            for (int i = 0; i < _octaves; i++)
            {
                total += PerlinNoise(x * frequency, y * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= _persistence;
                frequency *= _lacunarity;
            }
            return total / maxValue; // нормализованный результат

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int CountWallsAround(bool[,] map, int x, int y) 
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            int count = 0;

            for(int nx = x-1; nx<=x+1; nx++)
            {
                for(int ny = y-1; ny <=y+1; ny++)
                {
                    if(nx>=0 && nx<width && ny >= 0 && ny<height)
                    {
                        if (!map[nx, ny])
                        {
                            count++;
                        }
                        
                    }
                    else 
                    { 
                        count++;  //вне границ считается стеной
                    }

                }
            }
            return count;
        }

        private bool[,] ApplyCellularAutomation(bool[,] map, int iterations)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            bool[,] newMap = (bool[,])map.Clone();

            for(int i = 0; i<iterations; i++)
            {
                for(int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int wallCount = CountWallsAround(map, x, y);
                        newMap[x, y] = wallCount <= 4; //Делаем стеной, если вокруг 4 и больше стен
                    }
                }
                map = (bool[,])newMap.Clone();
            }
            return newMap;
            
        } 



        public bool[,] GenerateMap(int sizeX, int sizeY)
        {
            bool[,] map = new bool[sizeX, sizeY];

            for (int x=0; x < sizeX; x++)
            {
                for(int y=0; y< sizeY; y++)
                {
                    //Шум для текущей точки                    
                    float noiseValue = FractalNoise(x * _scale, y * _scale);
                    float verticalGradient = 1.0f - (float)y / sizeY; //градиент от 1 внизу до 0 вверху - придает пещерный вид
                    noiseValue+=verticalGradient * 0.5f; //усиливаем шум внизу карты

                    float normalizedValue = (noiseValue + 1) / 2; // нормализация к диапазону [0,1]

                    map[x, y] = normalizedValue > _threshold; // если значение выше порога - это стена
                }
            }
            if (_useCellularAutomation)
                map = ApplyCellularAutomation(map, 5); //применяем клеточный автомат для улучшения структуры пещер

            return map;
        }

        

    }
}
