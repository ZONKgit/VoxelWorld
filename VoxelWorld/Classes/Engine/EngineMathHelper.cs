using System;
using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    class EngineMathHelper
    {
        public static Vector3 FloorVector3(Vector3 vector)
        {
            float x = (float)Math.Floor(vector.X);
            float y = (float)Math.Floor(vector.Y);
            float z = (float)Math.Floor(vector.Z);

            return new Vector3(x, y, z);
        }
    }
}
