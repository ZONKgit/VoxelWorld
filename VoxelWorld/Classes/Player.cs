﻿using System;
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
        public Block[] Slots = new[] {Blocks.grass, Blocks.stone, Blocks.glass};
        public byte selectedSlot = 0;

        private TexturedCube handBlock = new TexturedCube();
    
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
            handBlock.texture = Game.BlocksTexture;
        }

        
        public void RenderProcess()
        {
            camera.RenderProcess();


            
            float angleX = -camera.rotation.Y;
            float angleY = -camera.rotation.X;
            float angleZ = camera.rotation.Z;
            
            float x = camera.position.X;
            float y = camera.position.Y;
            float z = camera.position.Z;

            int X,Y,Z,oldX=0,oldY=0,oldZ=0;
            float RayLenght = 0.5f;
            
            x += (float)-Math.Sin(angleX / 180 * Math.PI);
            X = (int)(x / 2.0f);
            y += (float)Math.Tan(angleY / 180 * Math.PI);
            Y = (int)(y / 2.0f);
            z += (float)-Math.Cos(angleX / 180 * Math.PI);
            Z = (int)(z / 2.0f);
            // Отображение блкоа на который наведен курсор

              float dist=0;
              while (dist < RayLenght) // радиус действия
              {
                  dist += 0.001f;
                  x += (float)-Math.Sin(angleX / 180 * Math.PI)*0.01f;
                  X = (int)Math.Round(x);
                  y += (float)Math.Tan(angleY / 180 * Math.PI)*0.01f;
                  Y = (int)Math.Round(y);
                  z += (float)-Math.Cos(angleX / 180 * Math.PI)*0.01f;
                  Z = (int)Math.Round(z);
        
                  if (check(X,Y,Z))
                  {
                      BoxEdges.DrawBoxEdges(new Vector3(1f / 2, 1f / 2, 1f / 2), new Vector3(X,Y,Z), new Color4(1.0f, 1.0f, 0.0f, 1.0f), 2.0f);
                      break;
                  }
              }
              
            handBlock.Draw(new Vector3(0,15f,0), new Vector3(1f / 2, 1f / 2, 1f / 2), 45, new Vector3(0,1,0), new []{1,4,4,4,4,2});
            // Рисование hitbox-а
            if (Game.isDrawDebugHitBox) BoxEdges.DrawBoxEdges(hitbox.HitBoxSize / 2, Position, new Color4(1.0f, 1.0f, 1.0f, 1.0f), 2.0f);  
        }

       

        public void PhysicsProcess(float delta)
        {
            float angleX = -camera.rotation.Y;
            float angleY = -camera.rotation.X;
            float angleZ = camera.rotation.Z;
            
            float x = camera.position.X;
            float y = camera.position.Y;
            float z = camera.position.Z;

            int X,Y,Z,oldX=0,oldY=0,oldZ=0;
            float RayLenght = 0.5f;
            
            x += (float)-Math.Sin(angleX / 180 * Math.PI);
            X = (int)(x / 2.0f);
            y += (float)Math.Tan(angleY / 180 * Math.PI);
            Y = (int)(y / 2.0f);
            z += (float)-Math.Cos(angleX / 180 * Math.PI);
            Z = (int)(z / 2.0f);
            // Установка блока
            if (Input.IsMouseButtonJustPressed(Input.KeyPlaceBlock))
              { 
                  float dist=0;
                 while (dist < RayLenght) // радиус действия
                 {
                     dist+=0.001f;
                
                     x += (float)-Math.Sin(angleX / 180 * Math.PI)*0.01f;
                     X = (int)Math.Round(x);
                     y += (float)Math.Tan(angleY / 180 * Math.PI)*0.01f;
                     Y = (int)Math.Round(y);
                     z += (float)-Math.Cos(angleX / 180 * Math.PI)*0.01f;
                     Z = (int)Math.Round(z);
                
                     if (check(X,Y,Z))
                     {
                         world.chunkManager.SetBlock(new Vector3(oldX,oldY,oldZ), Slots[selectedSlot]);
                         break;
                     }
                
                     oldX = X;
                     oldY = Y;
                     oldZ = Z;
                 }
            }// Ломание блока
            else if (Input.IsMouseButtonJustPressed(Input.KeyRemoveBlock))
              {
                  float dist=0;
                  while (dist < RayLenght) // радиус действия
                  {
                      dist+=0.001f;
                
                      x += (float)-Math.Sin(angleX / 180 * Math.PI)*0.01f;
                      X = (int)Math.Round(x);
                      y += (float)Math.Tan(angleY / 180 * Math.PI)*0.01f;
                      Y = (int)Math.Round(y);
                      z += (float)-Math.Cos(angleX / 180 * Math.PI)*0.01f;
                      Z = (int)Math.Round(z);
                
                      if (check(X,Y,Z))
                      {
                          world.chunkManager.RemoveBlock(new Vector3(X,Y,Z));
                          break;
                      }
                  }
              }
            
            
            // Выбор блока
            if (Input.IsJustKeyPressed(Input.Key1))
            {
                selectedSlot = 0;
            }
            else if (Input.IsJustKeyPressed(Input.Key2))
            {
                selectedSlot = 1;
            }else if (Input.IsJustKeyPressed(Input.Key3))
            {
                selectedSlot = 2;
            }

            
            
            // if (!hitbox.IsOnFloor)
            // {
                //Velocity.Y -= 0.1f;
            //}
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
                //     Velocity.Y += 1.5f;
                // }
                Velocity.Y += 0.2f;
            }
            if (Input.IsKeyPressed(Input.KeyCrouch))
            {
                Velocity.Y -= moveSpeed;
            }
            

            MoveAndCollide();
            
            // Покачивание камеры
            if (hitbox.IsOnFloor)
            {
                float CameraShakeSpeed = 1.4f; // Скорость покачивания
                float CameraShakeHeight = 0.1f; // Высота покачивания
                camera.position.Y += (float)Math.Sin((Position.X + Position.Z) * CameraShakeSpeed) * CameraShakeHeight;
            }
            
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
            // if (Velocity.X < 0) //-X
            // {
            //     if (check((int)(Position.X + Velocity.X+0.5f - (hitbox.HitBoxSize.X / 2)), (int)(Position.Y + hitbox.HitBoxSize.Y/2), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.X = 0;
            //     }
            //     if (check((int)(Position.X + Velocity.X +0.5f- (hitbox.HitBoxSize.X / 2)), (int)(Position.Y - hitbox.HitBoxSize.Y/2), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.X = 0;
            //     }
            // }
            // if (Velocity.Y > 0) // +Y
            // {
            //     if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y + (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z - hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.Y = 0;
            //     }
            //
            //     if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + Velocity.Y + (hitbox.HitBoxSize.Y / 2)), (int)(Position.Z + hitbox.HitBoxSize.Z/2)))
            //     {
            //         Velocity.Y = 0;
            //     }
            // }
            // if (Velocity.Z > 0) //+Z
            // {
            //     if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            //     if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            //     if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            //     if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z + (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
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
            //    
            // if (Velocity.Z < 0) //-Z
            // {
            //     if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z+0.5f - (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            //     if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z+0.5f - (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            //     if (check((int)(Position.X - hitbox.HitBoxSize.X/2), (int)(Position.Y + hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z+0.5f - (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            //     if (check((int)(Position.X + hitbox.HitBoxSize.X/2), (int)(Position.Y - hitbox.HitBoxSize.Z/2), (int)(Position.Z + Velocity.Z+0.5f - (hitbox.HitBoxSize.Z / 2)))) //+Z
            //     {
            //         Velocity.Z = 0;
            //     }
            // }

            Position += Velocity;
            NetWork.SendMessage(Position.ToString());
        }
        bool check(int X, int Y, int Z)
        {
            return world.chunkManager.CheckBlock(new Vector3(X,Y,Z));
        }
    }
}
