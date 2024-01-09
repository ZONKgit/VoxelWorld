using OpenTK;
using VoxelWorld.Classes.Physics;

namespace VoxelWorld.Classes.Engine
{
    public class Entity : Object
    {
        public HitBox hitbox;
        public Vector3 Velocity;
        public Vector3 Gravity;

        public void MoveAndCollide()
        {
            // Обработка столкновенний
            Position += Velocity*Game.delta;
        }
    }
}


            // if (Velocity.X < 0) //-X
            // {
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3(
            //             (int)(Position.X + Velocity.X + 0.5f - (hitbox.HitBoxSize.X / 2)),
            //             (int)(Position.Y + hitbox.HitBoxSize.Y / 2),
            //             (int)(Position.Z + hitbox.HitBoxSize.Z / 2))))
            //     {
            //         Velocity.X = 0;
            //     }
            //
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3(
            //             (int)(Position.X + Velocity.X + 0.5f - (hitbox.HitBoxSize.X / 2)),
            //             (int)(Position.Y - hitbox.HitBoxSize.Y / 2), (int)(Position.Z - hitbox.HitBoxSize.Z / 2)))
            // )
            //
            // {
            //         Velocity.X = 0;
            //     }
            // }
            // if (Velocity.Y > 0) // +Y
            // {
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y + (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
            //     ){
            //         Velocity.Y = 0;
            //     }
            //
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y + (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
            //     ){
            //         Velocity.Y = 0;
            //     }
            // }
            // if (Velocity.Z > 0) //+Z
            // {
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     ){
            //         Velocity.Z = 0;
            //     }
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     ){
            //         Velocity.Z = 0;
            //     }
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     ){
            //         Velocity.Z = 0;
            //     }
            //     if (Game.gameWorld.chunkManager.CheckBlock(new Vector3((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     ){
            //         Velocity.Z = 0;
            //     }
            // }
            //
            // if (Velocity.X > 0) //+X
            // {
            //     if (check((int)(Position.X + Velocity.X + (hitbox.HitBoxSize.X / 2)), (int)(Position.Y + hitbox.HitBoxSize.Y/2), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.X = 0;
            //     }
            //     if (check((int)(Position.X + Velocity.X + (hitbox.HitBoxSize.X / 2)), (int)(Position.Y - hitbox.HitBoxSize.Y/2), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.X = 0;
            //     }
            // }
            // if (Velocity.Y < 0) // -Y
            // {
            //     if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y - (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.Y = 0;
            //         hitbox.IsOnFloor = true;
            //     }
            //     
            // }
            // if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y - (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
            // {
            //     Velocity.Y = 0;
            //     hitbox.IsOnFloor = true;
            // }