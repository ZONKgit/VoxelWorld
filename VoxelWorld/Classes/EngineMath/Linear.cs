
using System;

namespace VoxelWorld.Classes.EngineMath
{
    public class Linear
    {
        public static float Clamp(float value, float min, float max) { return Math.Max(min, Math.Min(value, max)); }
    }
}