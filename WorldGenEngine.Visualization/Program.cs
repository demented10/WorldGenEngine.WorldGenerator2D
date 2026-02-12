using System.ComponentModel.DataAnnotations;
using WorldGenEngine.Core.GenerationMethods.Configs;
using WorldGenEngine.Core.MatrixAlgorithms.Factory;
using WorldGenEngine.Core.MatrixGeneration.Factories;
using WorldGenEngine.Visualization;

int w = 64,h = 64;


var generator = MatrixGeneratorsFactory.CreatePerlinNoiseBinaryMatrixGenerator(PerlinNoiseConfigFactory.GetDefaultConfig());
var rectFinder = RectFindersFactory.CreateRowDepthFinder();

IVisualisator visualisation = new ConsoleMatrixVisualiser(generator, w, h, rectFinder);
visualisation.Visualize();

namespace WorldGenEngine.Visualization
{
/*

Console.WriteLine("Generating Perlin Noise Map...");

var map = PerlinNoiseVisualization.GeneratePerlinNoiseMap(512, 512);

BitmapGenerator.SaveMapAsBitmap(map, "./perlin_noise_map.jpeg");

Console.WriteLine("Map size: {0}", map.Length);

Console.WriteLine("Map saved as perlin_noise_map.jpeg");

*/
}