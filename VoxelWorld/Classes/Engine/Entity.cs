using OpenTK;
using VoxelWorld.Classes.Physics;

namespace VoxelWorld.Classes.Engine
{
    public class Entity : Object
    {
        public HitBox hitbox;

        public Vector3 Velocity;

        private bool CheckBlockCollision(float x, float y, float z, float width, float height)
        {
            int minX = (int)(x - width / 2);
            int minY = (int)(y - height / 2);
            int maxX = (int)(x + width / 2);
            int maxY = (int)(y + height / 2);

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if (Game.gameWorld.chunkManager.CheckBlock(new Vector3(i, j, (int)z)))
                    {
                        return true; // Столкновение с блоком
                    }
                }
            }

            return false; // Нет столкновения с блоком
        }


        public void MoveAndCollide()
        {
            // Обработка столкновенний
            
            
            Position += Velocity;
            NetWork.SendMessage(Position.ToString());
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