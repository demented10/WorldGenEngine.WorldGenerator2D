using WorldGenEngine.Visualization;

Console.WriteLine("Generating Perlin Noise Map...");

var map = PerlinNoiseVisualization.GeneratePerlinNoiseMap(512, 512);

BitmapGenerator.SaveMapAsBitmap(map, "./perlin_noise_map.jpeg");

Console.WriteLine("Map size: {0}", map.Length);

Console.WriteLine("Map saved as perlin_noise_map.jpeg");

