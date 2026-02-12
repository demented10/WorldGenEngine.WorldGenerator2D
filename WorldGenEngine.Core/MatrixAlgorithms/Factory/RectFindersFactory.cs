
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;


namespace WorldGenEngine.Core.MatrixAlgorithms.Factory
{
    public static class RectFindersFactory
    {
        public static IRectsFinder CreateRowDepthFinder() => new RowDepthRectFinder();
    }
}
