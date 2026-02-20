namespace WorldGenEngine.Core.Noise.NoiseAlgorithms;

internal class PerlinNoise : INoise
{
    private const int GRADIENT_TABLE_SIZE = 256;
    private int[]? _permutationTable;
    private float[,]? _gradients;

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

    private float[] GetPseudoRandomGradientVector(int x, int y, int seed)
    {

        var hash = Utils.Math.GetHashWithSeed(x, y, seed);

        var index = hash & (GRADIENT_TABLE_SIZE - 1);

        return new float[] { _gradients![index, 0], _gradients[index, 1] };

    }

    public float GenerateNoise2D(float x, float y, int seed)
    {

        if (Equals(_gradients, null))
        {
            InitializeGradientTable(seed);
        }

        var left = (int)Math.Floor(x);
        var top = (int)Math.Floor(y);

        var pointInQuadX = x - left;
        var pointInQuadY = y - top;

        var topLeftGradient = GetPseudoRandomGradientVector(left, top, seed);
        var topRightGradient = GetPseudoRandomGradientVector(left + 1, top, seed);
        var bottomLeftGradient = GetPseudoRandomGradientVector(left, top + 1, seed);
        var bottomRightGradient = GetPseudoRandomGradientVector(left + 1, top + 1, seed);


        float[] distanceToTopLeft = new float[] { pointInQuadX, pointInQuadY };
        float[] distanceToTopRight = new float[] { pointInQuadX - 1, pointInQuadY };
        float[] distanceToBottomLeft = new float[] { pointInQuadX, pointInQuadY - 1 };
        float[] distanceToBottomRight = new float[] { pointInQuadX - 1, pointInQuadY - 1 };

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
}