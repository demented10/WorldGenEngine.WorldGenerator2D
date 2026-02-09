using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;
using WorldGenEngine.Core.MatrixGeneration.Models;

namespace WorldGenEngine.Core.MatrixAlgorithms.Factory
{
    public static class RectFindersFactory
    {
        public static IRectsFinder CreateRowDepthFinder() => new PolygonFinder();
    }
}
