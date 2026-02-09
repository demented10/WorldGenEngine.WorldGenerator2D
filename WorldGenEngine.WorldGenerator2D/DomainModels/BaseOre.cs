using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGenEngine.WorldGenerator2D.DomainModels
{
    internal abstract class BaseOre
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Type { get; }
    }

}
