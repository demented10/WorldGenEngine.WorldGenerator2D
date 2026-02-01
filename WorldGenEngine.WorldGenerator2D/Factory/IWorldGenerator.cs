using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.WorldInteraction;

namespace WorldGenEngine.WorldGenerator2D.Factory
{
    public interface IWorldGenerator
    {
        IWorldGetProperties GenerateWorld();
    }
}
