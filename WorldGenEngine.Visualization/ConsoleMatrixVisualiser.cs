using System.Diagnostics;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;
using WorldGenEngine.Core.MatrixAlgorithms.Models;
using WorldGenEngine.Core.MatrixGeneration.Generators;
using WorldGenEngine.Core.MatrixGeneration.Models;

namespace WorldGenEngine.Visualization;

class ConsoleMatrixVisualiser : ConsoleVisualiser, IVisualisator
{

    private readonly IBinaryMatrixGenerator _binaryMatrixGenerator;
    private readonly IRectsFinder _rectsFinder;
    private int MatrixWidth { get; set; }
    private int MatrixHeight { get; set; }

    public ConsoleMatrixVisualiser(IBinaryMatrixGenerator binaryMatrixGenerator, int matrixWidth, int matrixHeight, IRectsFinder rectsFinder)
    {
        _binaryMatrixGenerator = binaryMatrixGenerator;
        MatrixWidth = matrixWidth;
        MatrixHeight = matrixHeight;
        _rectsFinder = rectsFinder;
    }

    private BinaryMatrix GetMatrix()
    {
        var matrix = _binaryMatrixGenerator.GenerateMatrix(MatrixWidth, MatrixHeight);
        return new BinaryMatrix(MatrixWidth, MatrixHeight, matrix);
    }

    private ConsoleDraw GetMatrixDrawer(BinaryMatrix matrix)
    {
        return new ConsoleDraw(matrix);
    }

    private ConsoleDraw GetRectsDrawer(Rect[] rects)
    {
        return new ConsoleDraw(MatrixWidth, MatrixHeight, rects);
    }

    private void VisualizeBinaryMatrix(BinaryMatrix matrix)
    {
        var matrixDrawer = GetMatrixDrawer(matrix);
        Console.WriteLine("Исходная рандомная матрица:");
        matrixDrawer.UpdateGraphics();

    }

    private void VisualizeRects(Rect[] rects)
    {
        var rectsDrawer = GetRectsDrawer(rects);
        Console.WriteLine("Разметка областей в матрице:");
        rectsDrawer.UpdateGraphics();
    }

    protected override int GetLineLength()
    {
        return MatrixWidth * ConsolePoint.W;
    }

    public void Visualize()
    {

        long start = Stopwatch.GetTimestamp();
        var matrix = GetMatrix();
        TimeSpan elapsedGenMatrix = Stopwatch.GetElapsedTime(start);
        VisualizeBinaryMatrix(matrix);

        start = Stopwatch.GetTimestamp();
        var rects = _rectsFinder.FindRects(matrix);
        TimeSpan elapsedFindAlgo = Stopwatch.GetElapsedTime(start);


            DrawLine('-');
            VisualizeRects(rects.ToArray());
        


        /* Console.WriteLine("Список площадей:");
         foreach (var s in rects)
         {
             Console.WriteLine(s);
         }*/

        Console.WriteLine($"Скорость генерации матрицы: {elapsedGenMatrix.ToString()}");
        Console.WriteLine($"Скорость алгоритма: {elapsedFindAlgo.ToString()}");
    }


}