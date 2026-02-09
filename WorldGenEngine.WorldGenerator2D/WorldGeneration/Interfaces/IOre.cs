using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces
{
    public interface IOre
    {
        string Name { get; }
        string Description { get; }
        string Type { get; }
    }
}
