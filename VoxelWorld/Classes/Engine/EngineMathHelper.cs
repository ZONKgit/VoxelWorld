using System;
using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    class EngineMathHelper
    {
        public static Vector3 FloorVector3(Vector3 vector)
        {
            float x = (int)vector.X;
            float y = (int)vector.Y;
            float z = (int)vector.Z;

            return new Vector3(x, y, z);
        }
    }
}
