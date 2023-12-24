using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Physics;
using VoxelWorld.Classes.World;
using VoxelWorld.Classes.Render;

namespace VoxelWorld.Classes
{
    public class Player
    {
        public GameWorld world;
        Camera camera;
        public HitBox hitbox = new HitBox(new Vector3(0.5f,1.8f,0.5f));

        const float MaxFallVelocity = -2f;

        public Vector3 Position = new Vector3(0, 12, 0);    
        public Vector3 Rotation = new Vector3(0, 0, 0);
        public Vector3 Velocity = new Vector3(0, 0, 0);

        public float moveSpeed = 0.1f;
    
        public Player(GameWorld world)
        {
            this.world = world;
            Input.OnMouseMove += HandleMouseMove;
        }

        public void Ready()
        {
            Game.player = this;
            camera = new Camera(this);
            camera.Ready();
        }

        
        public void RenderProcess()
        {
            camera.RenderProcess();

            
            // Рейкаст
            // float angleX = -camera.rotation.Y;
            // float angleY = -camera.rotation.X;
            // float angleZ = camera.rotation.Z;
            //
            // float startX = camera.position.X;
            // float startY = camera.position.Y;
            // float startZ = camera.position.Z;
            //
            // float x = startX;
            // float y = startY;
            // float z = startZ;
            //
            // int X = 0, Y = 0, Z = 0;
            // int OldX = 0, OldY = 0, OldZ = 0;
            // int dist = 0;
            // int max_dist = 5;
            //
            // bool MouseRight = false;
            //
            // while (Vector3.Distance(new Vector3(x, y, z), new Vector3(startX, startY, startZ)) < max_dist * 3)
            // {
            //     dist++;
            //
            //     // Используйте углы в радианах, так как Math.Sin и Math.Cos ожидают углы в радианах
            //     x += -(float)Math.Sin(angleX / 180 * Math.PI); 
            //     y += (float)Math.Tan(angleY / 180 * Math.PI); 
            //     z += -(float)Math.Cos(angleX / 180 * Math.PI); 
            //
            //     X = (int)x / 1;
            //     Y = (int)y / 1;
            //     Z = (int)z / 1;
            //
            //     if (check(X, Y, Z))
            //     {
            //         BoxEdges.DrawBoxEdges(new Vector3(1f / 2, 1f / 2, 1f / 2), new Vector3(OldX + 0.5f, OldY + 0.5f, OldZ - 0.5f), new Color4(1.0f, 1.0f, 0.0f, 1.0f), 2.0f);
            //     }
            //
            //     if (!MouseRight)
            //     {
            //         if (Input.IsMouseButtonJustPressed(Input.KeyPlaceBlock))
            //         {
            //             MouseRight = true;
            //             Console.WriteLine("SetBlock");
            //             world.chunkManager.SetBlock(new Vector3(OldX, OldY, OldZ));
            //         }
            //     }
            //
            //     OldX = X;
            //     OldY = Y;
            //     OldZ = Z;
            // }




            // Рисование hitbox-а
            //BoxEdges.DrawBoxEdges(hitbox.HitBoxSize / 2, Position, new Color4(1.0f, 1.0f, 1.0f, 1.0f), 2.0f);  
        }

       

        public void PhysicsProcess()
        {
            // if (!hitbox.IsOnFloor)
            // {
            //     Velocity.Y -= 0.05f;
            // }
            camera.position = Position+camera.LocalPosition;
            camera.rotation = Rotation+camera.LocalRotation;
            camera.PhysicsProcess();

            // Ходьба
            float YAngle = -Rotation.Y / 180f * (float)Math.PI;
            if (Input.IsKeyPressed(Input.KeyMovmentForward))
            {
                Velocity.X -= (float)Math.Sin(YAngle) * moveSpeed;
                Velocity.Z -= (float)Math.Cos(YAngle) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentRight))
            {
                Velocity.X += (float)Math.Sin(YAngle + (float)Math.PI * 0.5) * moveSpeed;
                Velocity.Z += (float)Math.Cos(YAngle + (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentLeft))
            {
                Velocity.X += (float)Math.Sin(YAngle - (float)Math.PI * 0.5) * moveSpeed;
                Velocity.Z += (float)Math.Cos(YAngle - (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentBackward))
            {
                Velocity.X += (float)Math.Sin(YAngle) * moveSpeed;
                Velocity.Z += (float)Math.Cos(YAngle) * moveSpeed;
            }
            //Бег
            if (Input.IsKeyPressed(Input.KeyRun))
            {
                Velocity.X -= (float)Math.Sin(YAngle) * moveSpeed*10;
                Velocity.Z -= (float)Math.Cos(YAngle) * moveSpeed*10;
            }
            // Прыжок и красться
            if (Input.IsKeyPressed(Input.KeyJump))
            {
                // if (hitbox.IsOnFloor)
                // {
                //     hitbox.IsOnFloor = false;
                //     Velocity.Y += 0.2f;
                // }
                Velocity.Y += 0.2f;
            }
            if (Input.IsKeyPressed(Input.KeyCrouch))
            {
                Velocity.Y -= moveSpeed;
            }
            

            MoveAndCollide();
            Velocity.X = 0; Velocity.Z = 0;
            Velocity.Y = 0;
            if (Velocity.Y <= MaxFallVelocity) Velocity.Y = MaxFallVelocity;
        }

        private void HandleMouseMove(Vector2 mouseRelative)
        {
            // Вращение телом
            Rotation.Y += mouseRelative.X * Input.mouseSensitivity;
        }

        public void OnResizeWindow(EventArgs e)
        {
            camera.OnResizeWindow(e);
        }

        private void MoveAndCollide()
        {
            // Обработка столкновенний
            if (Velocity.X < 0) //-X
            {
                if (check((int)(Position.X + Velocity.X - (hitbox.HitBoxSize.X / 2)), (int)(Position.Y + hitbox.HitBoxSize.Y/2), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.X = 0;
                }
                if (check((int)(Position.X + Velocity.X - (hitbox.HitBoxSize.X / 2)), (int)(Position.Y - hitbox.HitBoxSize.Y/2), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.X = 0;
                }
            }
            if (Velocity.Y > 0) // +Y
            {
                if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y + (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.Y = 0;
                }
            
                if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y + (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.Y = 0;
                }
            }
            if (Velocity.Z > 0) //+Z
            {
                if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
                if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
                if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
                if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
            }
            
            if (Velocity.X > 0) //+X
            {
                if (check((int)(Position.X + Velocity.X + (hitbox.HitBoxSize.X / 2)), (int)(Position.Y + hitbox.HitBoxSize.Y/2), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.X = 0;
                }
                if (check((int)(Position.X + Velocity.X + (hitbox.HitBoxSize.X / 2)), (int)(Position.Y - hitbox.HitBoxSize.Y/2), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.X = 0;
                }
            }
            if (Velocity.Y < 0) // -Y
            {
                if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y - (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.Y = 0;
                    hitbox.IsOnFloor = true;
                }
            
                if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y - (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
                {
                    Velocity.Y = 0;
                    hitbox.IsOnFloor = true;
                }
            }
               
            if (Velocity.Z < 0) //-Z
            {
                if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z - (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
                if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z - (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
                if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z - (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
                if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z - (hitbox.HitBoxSize.Z / 2)))) //+Z
                {
                    Velocity.Z = 0;
                }
            }

            Position += Velocity;
            NetWork.SendMessage(Position.ToString());
        }
        bool check(int X, int Y, int Z)
        {
            return world.chunkManager.CheckBlock(new Vector3(X,Y,Z));
        }
    }
}
