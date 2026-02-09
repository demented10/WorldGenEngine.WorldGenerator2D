using System.ComponentModel.DataAnnotations;
using WorldGenEngine.Core.MatrixAlgorithms.Factory;
using WorldGenEngine.Core.MatrixGeneration.Factories;
using WorldGenEngine.Visualization;

int w = 6400,h = 6400;


var generator = MatrixGeneratorsFactory.CreateDefault();
var rectFinder = RectFindersFactory.CreateRowDepthFinder();

IVisualisator visualisator = new ConsoleMatrixVisualiser(generator, w, h, rectFinder);
visualisator.Visualize();

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