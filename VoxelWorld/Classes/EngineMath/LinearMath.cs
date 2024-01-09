
using System;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.EngineMath
{
    public class LinearMath
    {
        public static float Clamp(float value, float min, float max) { return Math.Max(min, Math.Min(value, max)); }
        public static float Interpolation(float start, float end, float speed) { return start + (end - start) * Game.time*speed; }
    }
}