namespace WorldGenEngine.WorldGenerator2D.CaveGenerator.GenerationMethods
{
    public class PerlinNoiseGenerator : IMapGenerator
    {
        private float _scale = 0.1f; //масштаб шума (чем меньше, тем больше пещеры)
        private float _threshold = 0.0f; //порог для определения стен и проходов
        private int _octaves = 4; //количество октав
        private float _persistence = 0.5f; //влияние каждой октавы
        private float _lacunarity = 2.0f; //частота каждой октавы
        private float _verticalBias = 0.3f; //вертикальный градиент для сужения пещер к верху
        private int _seed = 1337; //семя для генерации шума
        private int _cellularAutomationIterations = 5; //количество итераций клеточного автомата
        private bool _useCellularAutomation = true; //флаг для применения клеточного автомата
        

        private const int GRADIENT_TABLE_SIZE = 256;
        private int[] _permutationTable;
        private float[,] _gradients;


        public PerlinNoiseGenerator()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale">масштаб шума (чем меньше, тем больше пещеры)</param>
        /// <param name="threshold">порог для определения стен и проходов</param>
        /// <param name="octaves">количество октав</param>
        /// <param name="persistence">влияние каждой октавы</param>
        /// <param name="seed">семя для генерации шума</param>
        /// <param name="seed">семя для генерации шума</param>
        /// <param name="lacunarity">частота каждой октавы</param>
        /// <param name="useCellularAutomation">флаг для применения клеточного автомата</param>
        public PerlinNoiseGenerator(float scale, float threshold, 
            int octaves, float persistence, 
            float lacunarity,int cellularAutomationIterations,
            int seed,
            float verticalBias,
            bool useCellularAutomation)
        {
            _scale = scale;
            _threshold = threshold;
            _octaves = octaves;
            _persistence = persistence;
            _lacunarity = lacunarity;
            _seed = seed;
            _cellularAutomationIterations = cellularAutomationIterations;
            _useCellularAutomation = useCellularAutomation;
            _verticalBias = verticalBias;
            InitializeGradientTable(_seed);
        }
        private void InitializeGradientTable(int seed)
        {
            _permutationTable = new int[GRADIENT_TABLE_SIZE * 2];
            _gradients = new float[GRADIENT_TABLE_SIZE * 2, 2];

            var random = new Random(seed);

            // Создаем случайные градиентные векторы
            for (int i = 0; i < GRADIENT_TABLE_SIZE; i++)
            {
                float angle = (float)(random.NextDouble() * Math.PI * 2);
                _gradients[i, 0] = (float)Math.Cos(angle);
                _gradients[i, 1] = (float)Math.Sin(angle);

                _gradients[i + GRADIENT_TABLE_SIZE, 0] = _gradients[i, 0];
                _gradients[i + GRADIENT_TABLE_SIZE, 1] = _gradients[i, 1];
            }

            // Инициализируем таблицу перестановок
            for (int i = 0; i < GRADIENT_TABLE_SIZE; i++)
            {
                _permutationTable[i] = i;
            }

            // Перемешиваем таблицу перестановок
            for (int i = 0; i < GRADIENT_TABLE_SIZE; i++)
            {
                int swapIndex = random.Next(GRADIENT_TABLE_SIZE);
                (_permutationTable[i], _permutationTable[swapIndex]) =
                    (_permutationTable[swapIndex], _permutationTable[i]);

                _permutationTable[i + GRADIENT_TABLE_SIZE] = _permutationTable[i];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private float[] GetPseudoRandomGradientVector(int x, int y)
        {
            // Используем улучшенную хеш-функцию
            int hash = (x * 374761393 + y * 668265263 + _seed) & 0x7fffffff;
            hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
            hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
            hash = (hash >> 16) ^ hash;

            int index = hash & (GRADIENT_TABLE_SIZE - 1);

            return new float[] {
            _gradients[index, 0],
            _gradients[index, 1]
        };
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

            return Utils.Math.Dot(gradient, [dx,dy]);
        }


        private float PerlinNoiseV2(float fx, float fy)
        {
            int left = (int)Math.Floor(fx);
            int top = (int)Math.Floor(fy);

            float pointInQuadX = fx - left;
            float pointInQuadY = fy - top;

            float[] topLeftGradient = GetPseudoRandomGradientVector(left, top);
            float[] topRightGradient = GetPseudoRandomGradientVector(left + 1, top);
            float[] bottomLeftGradient = GetPseudoRandomGradientVector(left, top + 1);
            float[] bottomRightGradient = GetPseudoRandomGradientVector(left + 1, top + 1);

            float[] distanceToTopLeft = [pointInQuadX, pointInQuadY];
            float[] distanceToTopRight = [pointInQuadX - 1, pointInQuadY];
            float[] distanceToBottomLeft = [pointInQuadX, pointInQuadY - 1];
            float[] distanceToBottomRight = [pointInQuadX - 1, pointInQuadY - 1];

            float tx1 = Utils.Math.Dot(distanceToTopLeft, topLeftGradient);
            float tx2 = Utils.Math.Dot(distanceToTopRight, topRightGradient);
            float bx1 = Utils.Math.Dot(distanceToBottomLeft, bottomLeftGradient);
            float bx2 = Utils.Math.Dot(distanceToBottomRight, bottomRightGradient);

            pointInQuadX = Utils.Math.QuanticCurve(pointInQuadX);
            pointInQuadY = Utils.Math.QuanticCurve(pointInQuadY);

            float tx = Utils.Math.Lerp(tx1, tx2, pointInQuadX);
            float bx = Utils.Math.Lerp(bx1, bx2, pointInQuadX);
            float tb = Utils.Math.Lerp(tx, bx, pointInQuadY);

            return tb;
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

        private float FractalNoiseV2(float fx, float fy)
        {
            float amplitude = 1; // сила применения шума к общей картине, будет уменьшаться с "мельчанием" шума
                                 // как сильно уменьшаться - регулирует persistence
            float max = 0; // необходимо для нормализации результата
            float result = 0; // накопитель результата

            var octaves = _octaves; 

            while (octaves-- > 0)
            {
                max += amplitude;
                result += PerlinNoiseV2(fx, fy) * amplitude;
                amplitude *= _persistence;
                fx *= 2; // удваиваем частоту шума (делаем его более мелким) с каждой октавой
                fy *= 2;
            }

            return result / max;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int CountWallsAround(bool[,] map, int x, int y, bool countSelf = false) 
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            int count = 0;

            for(int nx = x-1; nx<=x+1; nx++)
            {
                for(int ny = y-1; ny <=y+1; ny++)
                {
                    if (nx == 0 && ny == 0 && !countSelf)
                        continue;

                    int checkX = x + nx;
                    int checkY = y + ny;

                    if (checkX>=0 && checkX<width && checkY >= 0 && checkY<height)
                    {
                        if (!map[checkX, checkY])
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
                        int wallCount = CountWallsAround(map, x, y, true);
                        if (!map[x, y] && wallCount < 4)
                        {
                            newMap[x, y] = true; //Делаем стеной, если вокруг 4 и больше стен
                        }
                        else if (map[x, y] && wallCount > 5)
                        {
                            newMap[x, y] = false; //Делаем проходом, если вокруг больше 5 стен
                        }
                    }
                }
                map = (bool[,])newMap.Clone();
            }
            return newMap;
            
        } 



        public bool[,] GenerateMap(int sizeX, int sizeY)
        {
            bool[,] map = new bool[sizeX, sizeY];

            float[,] noiseMap = new float[sizeX, sizeY];
            float min = float.MaxValue;
            float max = float.MinValue;

            //Заполняем карту шумом
            for(int x = 0; x < sizeX; x++)
            {
                for(int y = 0; y< sizeY; y++)
                {
                    float noiseValue = FractalNoiseV2(x * _scale, y * _scale);
                    noiseMap[x, y] = noiseValue;

                    if(noiseValue < min) min = noiseValue;
                    if(noiseValue > max) max = noiseValue;
                }
            }
            
            for (int x=0; x < sizeX; x++)
            {
                for(int y=0; y< sizeY; y++)
                {
                    float normalized = (noiseMap[x,y]-min)/(max-min);

                    float verticalBias = 1.0f - (float)y / sizeY;

                    // добавляем вертикальный градиент для создания пещер, которые сужаются к верху
                    normalized += verticalBias * _verticalBias;

                    // если значение выше порога - это стена
                    map[x, y] = normalized > _threshold; 
                }
            }
            if (_useCellularAutomation)
                map = ApplyCellularAutomation(map, 5); //применяем клеточный автомат для улучшения структуры пещер

            return map;
        }

        

    }

    

}
