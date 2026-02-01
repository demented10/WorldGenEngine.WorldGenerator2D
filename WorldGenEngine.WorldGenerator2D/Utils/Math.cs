using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGenEngine.WorldGenerator2D.Utils
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
    }
}
