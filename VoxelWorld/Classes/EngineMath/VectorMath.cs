using System;
using OpenTK;

namespace VoxelWorld.Classes.EngineMath
{
    public class VectorMath
    {
        public static float CalculateVector2Distance(Vector2 point1, Vector2 point2) { return (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2)); }
    }
}