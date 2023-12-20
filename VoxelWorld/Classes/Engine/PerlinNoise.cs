using System;
using OpenTK;
using SimplexNoise;

namespace VoxelWorld.Classes.Engine
{
    class Perlin2D
    {
        public float GetNoiseValue(Vector2 Pos)
        {   
            return SimplexNoise.Noise.CalcPixel2D((int)Pos.X, (int)Pos.Y, 0.02f)/255;
        }
    }
}
