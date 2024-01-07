using System;
using OpenTK;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Physics
{
    public class HitBox : Entity
    {
        public Vector3 HitBoxSize;
        public bool IsOnFloor = false;

        public HitBox(Vector3 HitBoxSize)
        {
            this.HitBoxSize = HitBoxSize;
        }
    }
}
