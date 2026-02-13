using System;
using WorldGenEngine.Core.MatrixAlgorithms.Models;
using WorldGenEngine.Core.MatrixGeneration.Models;

namespace WorldGenEngine.Visualization
{
    /// <summary>
    /// Класс для отрисовки изображения в консоли
    /// </summary>
    class ConsoleDraw
    {

        private ConsolePoint[] buffer;

        public int WindowWidth { get; }
        public int WindowHeight { get; }


        public ConsoleDraw(int windowWidth, int windowHeight)
        {
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;

            buffer = new ConsolePoint[windowHeight * windowWidth];
        }

        public ConsoleDraw(int windowWidth, int windowHeight, Rect[] rects)
        {
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;

            buffer = new ConsolePoint[windowHeight * windowWidth];
            FillBuffer(ConsoleColor.Black, 'F');
            InitByRects(rects);
        }

        public ConsoleDraw(IReadonlyMatrix matrix)
        {
            WindowHeight = matrix.Height;
            WindowWidth = matrix.Width;
            buffer = new ConsolePoint[matrix.Height * matrix.Width];
            InitByBinaryFlatMatrix(matrix);
        }


        private void InitByBinaryFlatMatrix(IReadonlyMatrix matrix)
        {
            for (var i = 0; i < matrix.Height; i++)
            {
                for (var j = 0; j < matrix.Width; j++)
                {
                    if (matrix.GetValue(j,i))
                    {
                        SetPoint(j, i, new ConsolePoint('T', ConsoleColor.DarkGreen));
                    }
                    else
                    {
                        SetPoint(j, i, new ConsolePoint('F', ConsoleColor.DarkRed));
                    }
                }
            }
        }

        private void FillBuffer(ConsoleColor color, char symbol)
        {
            for (int i = 0; i < WindowHeight; i++)
            {
                for (int j = 0; j < WindowWidth; j++)
                {
                    SetPoint(i, j, new ConsolePoint(symbol, color));
                }
            }
        }

        private void InitByRects(Rect[] rects)
        {
            string chars = "ABCDEGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            Random rand = new Random();
            foreach (var rect in rects)
            {
                var rndChar = chars[rand.Next(chars.Length)];
                var rndColor = colors[rand.Next(colors.Length)];
                for (int yI = 0; yI < rect.Height; yI++)
                {
                    for (int xI = 0; xI < rect.Width; xI++)
                    {

                        SetPoint(xI + rect.X, yI + rect.Y, new ConsolePoint(rndChar, rndColor));
                    }
                }
            }
        }

        public void SetPoint(int x, int y, ConsolePoint consolePoint)
        {
            var indexPosition = WindowWidth * y + x;

            buffer[indexPosition] = consolePoint;

        }

        public void UpdateGraphics()
        {
            for (int i = 0; i < WindowWidth; i++)
            {
                for (int j = 0; j < WindowHeight; j++)
                {
                    var point = buffer[WindowWidth * i + j];
                    Console.BackgroundColor = point.BackgroundColor;

                    var charMiddlePoint = (int)(ConsolePoint.W / 2);
                    for (var k = 0; k <= ConsolePoint.W; k++)
                    {
                        if (k == charMiddlePoint) Console.Write(point.GetValue());
                        else Console.Write(' ');

                    }
                }

                Console.Write("\n");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

    }
}