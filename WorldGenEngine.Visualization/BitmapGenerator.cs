using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace WorldGenEngine.Visualization
{
    internal class BitmapGenerator
    {
        public static void SaveMapAsBitmap(bool[,] map, string filePath)
        {
            try
            {
                int width = map.GetLength(0);
                int height = map.GetLength(1);
                using (var bitmap = new Bitmap(width, height))
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            var color = map[x, y] ? Color.Black : Color.White;
                            bitmap.SetPixel(x, y, color);
                        }
                    }
                    string directory = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    bitmap.Save(filePath, ImageFormat.Jpeg);
                    Console.WriteLine($"Файл сохранен: {Path.GetFullPath(filePath)}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving bitmap: " + ex.Message);
            }
        }
    }
}

