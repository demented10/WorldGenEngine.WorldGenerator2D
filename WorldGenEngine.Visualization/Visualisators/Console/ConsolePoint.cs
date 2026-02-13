using System;

namespace WorldGenEngine.Visualization
{
    struct ConsolePoint
    {
        public static int W = 2;
        public char Value {get;}

        public char GetValue()
        {
            return Value;
        }

        public ConsoleColor BackgroundColor { get; }

        public ConsolePoint(char value = 'T', ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Value = value;
            BackgroundColor = backgroundColor;
        }
    }
}