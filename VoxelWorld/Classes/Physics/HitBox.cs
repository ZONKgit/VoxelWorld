using System;
using OpenTK;

namespace VoxelWorld.Classes.Physics
{
    public class HitBox
    {
        public Vector3 HitBoxSize;
        public bool IsOnFloor = false;

        public HitBox(Vector3 HitBoxSize)
        {
            this.HitBoxSize = HitBoxSize;
        }
    }
}
