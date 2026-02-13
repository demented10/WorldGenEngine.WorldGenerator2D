using System;
using System.Numerics;
using Raylib_cs;
namespace WorldGenEngine.Visualization.Visualisators.RaylibVisualize
{

    public class RaylibVisualizer : IVisualisator
    {
        private bool _isInitialized = false;
        private void Init()
        {
            Raylib_cs.Raylib.InitWindow(800, 600, "Raylib Visualizer");
            Raylib_cs.Raylib.SetTargetFPS(60);
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

            Raylib.DrawText("Двигай мышкой!", 10, 10, 20, Color.Gray);
            Raylib.DrawCircleV(mousePos, 50, Color.Maroon);

            Raylib.EndDrawing();
        }

        // Закрытие окна
        Raylib.CloseWindow();
        }
    }
}