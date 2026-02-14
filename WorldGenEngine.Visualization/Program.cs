using WorldGenEngine.Core.GenerationMethods.Configs;
using WorldGenEngine.Core.MatrixAlgorithms.Factory;
using WorldGenEngine.Core.MatrixGeneration.Factories;
using WorldGenEngine.Visualization;
using WorldGenEngine.Visualization.Visualisators.RaylibVisualize;


class Program
{
    static int w = 640, h = 640;


    public  static void  Main(string[] args)
    {
        var generator = MatrixGeneratorsFactory.CreatePerlinNoiseBinaryMatrixGenerator(PerlinNoiseConfigFactory.GetDefaultConfig());
        var rectFinder = RectFindersFactory.CreateRowDepthFinder();

        IVisualisator visualisation = new RaylibWorldStateVisualizer();
        visualisation.Visualize();

    }
}