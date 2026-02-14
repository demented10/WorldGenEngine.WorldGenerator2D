using Raylib_cs;
using System;
using System.Linq;
using System.Numerics;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.Visualization.Visualisators.RaylibVisualize
{

    interface IChunkVisualizer
    {
        void VisualizeChunk(ChunkData chunkData);
    }

    public class RaylibWorldStateVisualizer : IVisualisator, IChunkVisualizer
    {
        private bool _isInitialized = false;
        private int _chunkSize = 10; // Размер одного тайла в пикселях
        private Color _solidTileColor = Color.DarkGray;
        private Color _emptyTileColor = Color.LightGray;
        private Font _font;
        private void Init()
        {
            Raylib_cs.Raylib.InitWindow(800, 600, "Raylib Visualizer");
            Raylib_cs.Raylib.SetTargetFPS(60);

            int[] codepoints = Enumerable.Range(32, 95).Concat(Enumerable.Range(0x0400, 256)).ToArray();
            _font = Raylib.LoadFontEx("C:\\Users\\xorol.DESKTOP-AU52V92\\source\\repos\\WorldGenEngine.WorldGenerator2D\\WorldGenEngine.Visualization\\resources\\Roboto-Regular.ttf", 32, codepoints, codepoints.Length);

            // Рекомендуется включить фильтрацию для четкости (опционально)
            Raylib.SetTextureFilter(_font.Texture, TextureFilter.Bilinear);
            _isInitialized = true;
        }

        public void Visualize()
        {

            if (!_isInitialized) Init();

            while (!Raylib.WindowShouldClose())
            {
                // 1. Обновление (Update)
                Vector2 mousePos = Raylib.GetMousePosition();

                // 2. Отрисовка (Draw)
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                Raylib.DrawTextEx(_font,"Двигай мышкой!", new Vector2(10,10), 32f,2f, Color.Gray);
                Raylib.DrawCircleV(mousePos, 50, Color.Maroon);

                Raylib.EndDrawing();
            }

            // Освобождение ресурсов
            Raylib.UnloadFont(_font);
            Raylib.CloseWindow();
        }

        public void VisualizeChunk(ChunkData chunkData)
        {

            for (int y = 0; y < ChunkData.ChunkSize; y++)
            {
                for (int x = 0; x < ChunkData.ChunkSize; x++)
                {
                    TileData tile = chunkData.Tiles[y * ChunkData.ChunkSize + x];
                    Color tileColor = tile.IsSolid ? _solidTileColor : _emptyTileColor;
                    Raylib.DrawRectangle(x * _chunkSize, y * _chunkSize, _chunkSize, _chunkSize, tileColor);
                }
            }
        }
    }
}