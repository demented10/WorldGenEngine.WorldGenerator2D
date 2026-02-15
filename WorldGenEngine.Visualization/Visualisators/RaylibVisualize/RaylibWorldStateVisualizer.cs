using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using WorldGenEngine.WorldGenerator2D.Factories;
using WorldGenEngine.WorldGenerator2D.Models;
using raygui_cs;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.Visualization.Visualisators.RaylibVisualize
{


    public class RaylibWorldStateVisualizer : IVisualisator
    {
        private bool _isInitialized = false;
        private int _tileSize = 5; // Размер одного тайла в пикселях
        private Color _solidTileColor = Color.DarkGray;
        private Color _emptyTileColor = Color.LightGray;
        private Font _font;
        private IChunkGenerationServiceFactory _chunkGenerationServiceFactory;
        private ChunkData _chunkData;

        private Camera2D _camera;
        private List<ChunkState> _loadedChunks;
        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        private WorldState _worldState;

        public RaylibWorldStateVisualizer(IChunkGenerationServiceFactory chunkGenerationServiceFactory, WorldState worldState)
        {
            _chunkGenerationServiceFactory = chunkGenerationServiceFactory;
            _worldState = worldState;
            Init();
        }

        private void Init()
        {
            Raylib_cs.Raylib.InitWindow(ScreenWidth, ScreenHeight, "Raylib Visualizer");
            Raylib_cs.Raylib.SetTargetFPS(60);


            int[] codepoints = Enumerable.Range(32, 95).Concat(Enumerable.Range(0x0400, 256)).ToArray();
            _font = Raylib.LoadFontEx("C:\\Users\\xorol.DESKTOP-AU52V92\\source\\repos\\WorldGenEngine.WorldGenerator2D\\WorldGenEngine.Visualization\\resources\\Roboto-Regular.ttf", 32, codepoints, codepoints.Length);

            // Рекомендуется включить фильтрацию для четкости (опционально)
            Raylib.SetTextureFilter(_font.Texture, TextureFilter.Bilinear);
            _chunkData = _chunkGenerationServiceFactory.Create(GeneratorType.Random).GenerateChunkData(new ChunkPosition(0, 0));
            _camera = new Camera2D
            {
                Target = new Vector2(0, 0),
                Offset = new Vector2(ScreenWidth, ScreenHeight),
                Rotation = 0,
                Zoom = 1f
            };
            _isInitialized = true;
        }

        public void Visualize()
        {

            if (!_isInitialized) Init();
            


            while (!Raylib.WindowShouldClose())
            {

                if (Raylib.IsMouseButtonDown(MouseButton.Right))
                {
                    Vector2 delta = Raylib.GetMouseDelta();
                    delta = Vector2.Divide(delta, _camera.Zoom); // Корректируем скорость под зум
                    _camera.Target.X -= delta.X;
                    _camera.Target.Y -= delta.Y;
                }

                // Зум (колесо мыши)
                float wheel = Raylib.GetMouseWheelMove();
                if (wheel != 0)
                {
                    // Зум относительно позиции мыши
                    Vector2 mouseWorldPos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _camera);
                    _camera.Offset = Raylib.GetMousePosition();
                    _camera.Target = mouseWorldPos;

                    _camera.Zoom += wheel * 0.1f;
                    if (_camera.Zoom < 0.1f) _camera.Zoom = 0.1f; // Ограничение
                }

                // Отрисовка (Draw)
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);


                Raylib.BeginMode2D(_camera);

                // Рисуем "мир" (сетку или объекты)
                Raylib.DrawRectangle(-100, -100, 200, 200, Color.RayWhite);
                for (int i = -10; i < 10; i++)
                {
                    Raylib.DrawLineV(new Vector2(i * 100, -1000), new Vector2(i * 100, 1000), Color.LightGray);
                    Raylib.DrawLineV(new Vector2(-1000, i * 100), new Vector2(1000, i * 100), Color.LightGray);
                }
                Raylib.DrawTextEx(_font,"Chunk Generation", new Vector2(-80,20), 20,1, Color.Black);

                if (_loadedChunks != null && _loadedChunks.Count > 0)
                {
                    VisualizeLoadedChunks();
                }

                Raylib.EndMode2D();


                //if (Raygui.GuiButton(new Rectangle(ScreenWidth-200, ScreenHeight-60, 150, 30), "Regenerate Chunk") == 1)
               // {
                    //  _chunkData = _chunkGenerationServiceFactory.Create(GeneratorType.Random).GenerateChunkData(new ChunkPosition(0, 0));
                //}
                if (Raygui.GuiButton(new Rectangle(ScreenWidth - 200, ScreenHeight - 125, 150, 30), "Load Chunks") == 1)
                {
                    _loadedChunks = _worldState.GetChunksStates(new ChunkPosition(0, 0));
                }
                if (Raygui.GuiButton(new Rectangle(ScreenWidth - 200, ScreenHeight - 155, 150, 30), "Save Chunks") == 1)
                {
                    _worldState.SaveChunks(_loadedChunks.ToArray());
                }

                Raylib.EndDrawing();
            }

            // Освобождение ресурсов
            Raylib.UnloadFont(_font);
            Raylib.CloseWindow();
        }

        public void VisualizeLoadedChunks()
        {
            foreach (var chunkState in _loadedChunks)
            {
                VisualizeChunk(chunkState.Data, chunkState.Position);
            }
        }

        public void VisualizeChunk(ChunkData chunkData, ChunkPosition chunkPosition)
        {
            
            for (int y = 0; y < ChunkData.ChunkSize; y++)
            {
                for (int x = 0; x < ChunkData.ChunkSize; x++)
                {
                    TileData tile = chunkData.Tiles[y * ChunkData.ChunkSize + x];
                    Color tileColor = tile.IsSolid ? _solidTileColor : _emptyTileColor;
                    Raylib.DrawRectangle((_tileSize*x) + (chunkPosition.ChunkXPos * ChunkData.ChunkSize), (_tileSize * y) + (chunkPosition.ChunkYPos * ChunkData.ChunkSize), _tileSize, _tileSize, tileColor);
                }
            }
        }
    }
}