using WorldGenEngine.Core.MatrixGeneration.Generators;

namespace WorldGenEngine.Core.GenerationMethods.Algorithms
{
    internal class PerlinNoiseGenerator : IGenerationAlgo
    {
        private readonly float _scale = 0.1f; //масштаб шума (чем меньше, тем больше пещеры)
        private readonly float _threshold = 0.0f; //порог для определения стен и проходов
        private readonly int _octaves = 4; //количество октав
        private readonly float _persistence = 0.5f; //влияние каждой октавы
        private readonly float _lacunarity = 2.0f; //частота каждой октавы
        private readonly float _verticalBias = 0.3f; //вертикальный градиент для сужения пещер к верху
        private readonly int _seed = 1337; //семя для генерации шума


        private const int GRADIENT_TABLE_SIZE = 256;
        private int[]? _permutationTable;
        private float[,]? _gradients;


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
        /// <param name="cellularAutomationIterations"></param>
        /// <param name="seed">семя для генерации шума</param>
        /// <param name="lacunarity">частота каждой октавы</param>
        /// <param name="verticalBias"></param>
        /// <param name="useCellularAutomation">флаг для применения клеточного автомата</param>
        public PerlinNoiseGenerator(float scale, float threshold,
            int octaves, float persistence,
            float lacunarity, int cellularAutomationIterations,
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
            _verticalBias = verticalBias;
            InitializeGradientTable(_seed);
        }

        private void InitializeGradientTable(int seed)
        {
            _permutationTable = new int[GRADIENT_TABLE_SIZE * 2];
            _gradients = new float[GRADIENT_TABLE_SIZE * 2, 2];

            var random = new Random(seed);

            // Создаем случайные градиентные векторы
            for (var i = 0; i < GRADIENT_TABLE_SIZE; i++)
            {
                var angle = (float)(random.NextDouble() * Math.PI * 2);
                _gradients[i, 0] = (float)Math.Cos(angle);
                _gradients[i, 1] = (float)Math.Sin(angle);

                _gradients[i + GRADIENT_TABLE_SIZE, 0] = _gradients[i, 0];
                _gradients[i + GRADIENT_TABLE_SIZE, 1] = _gradients[i, 1];
            }

            // Инициализируем таблицу перестановок
            for (var i = 0; i < GRADIENT_TABLE_SIZE; i++)
            {
                _permutationTable[i] = i;
            }

            // Перемешиваем таблицу перестановок
            for (var i = 0; i < GRADIENT_TABLE_SIZE; i++)
            {
                var swapIndex = random.Next(GRADIENT_TABLE_SIZE);
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
            var hash = Utils.Math.GetHashWithSeed(x, y, _seed);

            var index = hash & (GRADIENT_TABLE_SIZE - 1);

            return
            [
                _gradients![index, 0],
                _gradients[index, 1]
            ];
        }


        private float PerlinNoise(float fx, float fy)
        {
            var left = (int)Math.Floor(fx);
            var top = (int)Math.Floor(fy);

            var pointInQuadX = fx - left;
            var pointInQuadY = fy - top;

            var topLeftGradient = GetPseudoRandomGradientVector(left, top);
            var topRightGradient = GetPseudoRandomGradientVector(left + 1, top);
            var bottomLeftGradient = GetPseudoRandomGradientVector(left, top + 1);
            var bottomRightGradient = GetPseudoRandomGradientVector(left + 1, top + 1);

            float[] distanceToTopLeft = [pointInQuadX, pointInQuadY];
            float[] distanceToTopRight = [pointInQuadX - 1, pointInQuadY];
            float[] distanceToBottomLeft = [pointInQuadX, pointInQuadY - 1];
            float[] distanceToBottomRight = [pointInQuadX - 1, pointInQuadY - 1];

            var tx1 = Utils.Math.Dot(distanceToTopLeft, topLeftGradient);
            var tx2 = Utils.Math.Dot(distanceToTopRight, topRightGradient);
            var bx1 = Utils.Math.Dot(distanceToBottomLeft, bottomLeftGradient);
            var bx2 = Utils.Math.Dot(distanceToBottomRight, bottomRightGradient);

            pointInQuadX = Utils.Math.QuanticCurve(pointInQuadX);
            pointInQuadY = Utils.Math.QuanticCurve(pointInQuadY);

            var tx = Utils.Math.Lerp(tx1, tx2, pointInQuadX);
            var bx = Utils.Math.Lerp(bx1, bx2, pointInQuadX);
            var tb = Utils.Math.Lerp(tx, bx, pointInQuadY);

            return tb;
        }

        /// <summary>
        /// Метод для генерации фрактального шума на основе Перлин шума. Используется для создания более сложных и детализированных текстур шума.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private float FractalNoise(float fx, float fy)
        {
            float amplitude = 1; // сила применения шума к общей картине, будет уменьшаться с "мельчанием" шума
            // как сильно уменьшаться - регулирует persistence
            float max = 0; // необходимо для нормализации результата
            float result = 0; // накопитель результата

            var octaves = _octaves;

            while (octaves-- > 0)
            {
                max += amplitude;
                result += PerlinNoise(fx, fy) * amplitude;
                amplitude *= _persistence;
                fx *= 2; // удваиваем частоту шума (делаем его более мелким) с каждой октавой
                fy *= 2;
            }

            return result / max;
        }

        /// <summary>
        /// Метод для генерации карты пещер на основе фрактального Перлин шума.
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns>Массив bool где True - стена, False - пустота  </returns>
        public bool[,] GenerateMap(int sizeX, int sizeY)
        {
            var map = new bool[sizeX, sizeY];

            var noiseMap = new float[sizeX, sizeY];
            var min = float.MaxValue;
            var max = float.MinValue;

            //Заполняем карту шумом
            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    var noiseValue = FractalNoise(x * _scale, y * _scale);
                    noiseMap[x, y] = noiseValue;

                    if (noiseValue < min) min = noiseValue;
                    if (noiseValue > max) max = noiseValue;
                }
            }

            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    var normalized = (noiseMap[x, y] - min) / (max - min);

                    var verticalBias = 1.0f - (float)y / sizeY;

                    // добавляем вертикальный градиент для создания пещер, которые сужаются к верху
                    normalized += verticalBias * _verticalBias;

                    // если значение выше порога - это стена
                    map[x, y] = normalized > _threshold;
                }
            }

            return map;
        }

    }
}