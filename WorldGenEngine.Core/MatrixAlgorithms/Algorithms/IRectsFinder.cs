using WorldGenEngine.Core.MatrixAlgorithms.Models;
using WorldGenEngine.Core.MatrixGeneration.Models;

namespace WorldGenEngine.Core.MatrixAlgorithms.Algorithms
{
    /// <summary>
    /// Defines an interface for binary matrix partitioning algorithms
    /// </summary>
    public interface IRectsFinder
    {
        List<Rect> FindRects(IMatrix matrix);
        // IEnumerable<Rect> FindRects(IMatrix matrix);
        List<Rect> FindRects(bool[,] matrix);
    }
    
}