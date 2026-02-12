namespace WorldGenEngine.Core.Utils
{
    public static class Math
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
        public static float QuanticCurve(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
        public static float Dot(float[] a, float[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be of the same length");
            float result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;

        }

        public static int GetHashWithSeed(int x, int y, int seed)
        {
            var hash = (x * 374761393 + y * 668265263 + seed) & 0x7fffffff;
            hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
            hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
            hash = (hash >> 16) ^ hash;
            return hash;
        }
    }
}
