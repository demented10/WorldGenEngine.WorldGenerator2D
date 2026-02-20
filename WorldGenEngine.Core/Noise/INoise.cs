

namespace WorldGenEngine.Core.Noise
{
    public interface INoise
    {
        /// <summary>
        /// Generate two dim perlin noise based on x and y coordinates. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="seed">optional param. if null, method will use seed from configuration</param>
        /// <returns></returns>
        float GenerateNoise2D(float x, float y, int seed);
    }
}
