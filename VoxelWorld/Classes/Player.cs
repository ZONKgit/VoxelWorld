using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Physics;
using VoxelWorld.Classes.World;
using VoxelWorld.Classes.Render;
using Object = VoxelWorld.Classes.Engine.Object;

namespace VoxelWorld.Classes
{
    public class Player: Entity
    {
        Camera camera;

        const float MaxFallVelocity = -2f;
        
        public Vector3 Rotation = new Vector3(0, 0, 0);

        public float moveSpeed = 0.1f;
        public Block[] Slots = new[] {Blocks.grass, Blocks.stone, Blocks.glass, Blocks.oak_log, Blocks.oak_planks, Blocks.oak_leaves};
        public byte selectedSlot = 0;

        private TexturedCube handBlock = new TexturedCube();
        private Vector3 handPos = new Vector3(0.5f, -0.5f, -0.5f);
        private Vector3 handRot = -new Vector3(0,0,0);
        
        
        private Vector3 blockPlacementStart;
        private Vector3 blockPlacementEnd = new Vector3(-5f, 33f, -10.5f);
        private float blockPlacementAnimationTime;
        private float blockPlacementAnimationDuration = 0.2f; // You can adjust the duration as needed
        private bool isBlockPlacementAnimating;

    
        public Player(GameWorld world)
        {
            hitbox = new HitBox(new Vector3(0.5f,1.8f,0.5f));
            Input.OnMouseMove += HandleMouseMove;
        }

        public void Ready()
        {
            Position = new Vector3(0, 17, 0);
            Game.player = this;
            camera = new Camera(this);
            camera.Ready();
            handBlock.texture = Game.BlocksTexture;
        }


        public override void RenderProcess()
        {
            camera.RenderProcess();
            
            // Рисование обводки наведенного блока
            DrawCursorBlock();
            // Рисование блока в руке
            handBlock.Draw(handPos, camera.Position, new Vector3(0.15f, 0.15f, 0.15f), -new Vector3(camera.Rotation.X, Rotation.Y, camera.Rotation.Z)+handRot, Slots[selectedSlot].TextureFaces, 1);
            // Рисование hitbox-а
            if (Game.isDrawDebugHitBox) BoxEdges.DrawBoxEdges(hitbox.HitBoxSize / 2, camera.Position-camera.LocalPosition, new Color4(1.0f, 1.0f, 1.0f, 1.0f), 2.0f);  
        }

       

        public override void PhysicsProcess()
        {
            // Установка и ломание блока
            if (Input.IsMouseButtonJustPressed(Input.KeyPlaceBlock)) PlaceBlock();
            else if (Input.IsMouseButtonJustPressed(Input.KeyRemoveBlock)) RemoveBlock();
            
            
            // Выбор блока
            if (Input.IsJustKeyPressed(Input.Key1)) selectedSlot = 0;
            else if (Input.IsJustKeyPressed(Input.Key2)) selectedSlot = 1;
            else if (Input.IsJustKeyPressed(Input.Key3)) selectedSlot = 2;
            else if (Input.IsJustKeyPressed(Input.Key4)) selectedSlot = 3;
            else if (Input.IsJustKeyPressed(Input.Key5)) selectedSlot = 4;
            else if (Input.IsJustKeyPressed(Input.Key6)) selectedSlot = 5;

            
            
            // if (!hitbox.IsOnFloor)
            // {
                //Velocity.Y -= 0.1f;
            //}
            // camera.SetPosition(Position);
            // camera.SetRotation(Rotation);
            camera.Position = Position+camera.LocalPosition;
            camera.Rotation = Rotation+camera.LocalRotation;
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
                camera.Position.Y += (float)Math.Sin((Position.X + Position.Z) * CameraShakeSpeed) * CameraShakeHeight;
            }

            if (Velocity.X != 0 || Velocity.Z != 0)
            {
                handPos = Animation.SinusoidalMotion(new Vector3(0.5f, -0.5f, -0.5f), 0.08f, 0.05f, Game.time);
            }
            else if (Velocity.X == 0 && Velocity.Z == 0 && handPos != new Vector3(0.5f, -0.5f, -0.5f))
            {
                List<Vector3> handKeyFrames = new List<Vector3>{handPos, new Vector3(0.5f, -0.5f, -0.5f)};
                handPos = Animation.KeyframeAnimation(handKeyFrames, Game.time*0.0001f);
            }
            
            Velocity.X = 0; Velocity.Z = 0;
            Velocity.Y = 0;
            if (Velocity.Y <= MaxFallVelocity) Velocity.Y = MaxFallVelocity;
        }

        private void HandleMouseMove(Vector2 mouseRelative)
        {
            if (!Game.window.CursorVisible) {
                // Вращение телом
                Rotation.Y += mouseRelative.X * Input.mouseSensitivity;
            }
        }

        public override void  OnResizeWindow(EventArgs e)
        {
            camera.OnResizeWindow(e);
        }

        private void DrawCursorBlock()
        {
            if (RayCast()[0] != RayCast()[1]) BoxEdges.DrawBoxEdges(new Vector3(0.51f, 0.51f, 0.51f), RayCast()[0],
                new Color4(0.0f, 0.0f, 0.0f, 1.0f), 3.5f);
        }
        
        private void PlaceBlock() {
            if (RayCast()[0] != RayCast()[1]) Game.gameWorld.chunkManager.SetBlock(RayCast()[1], Slots[selectedSlot]);
        }

        private void RemoveBlock(){
            if (RayCast()[0] != RayCast()[1]) Game.gameWorld.chunkManager.RemoveBlock(RayCast()[0]);
        }

        private Vector3[] RayCast()
        {
            float angleX = -camera.Rotation.Y;
            float angleY = -camera.Rotation.X;
            float angleZ = camera.Rotation.Z;
            
            float x = camera.Position.X;
            float y = camera.Position.Y;
            float z = camera.Position.Z;

            int X,Y,Z,oldX=0,oldY=0,oldZ=0;
            float RayLenght = 0.5f;

            Vector3 NormalizedXYZ = new Vector3(
                (float)-Math.Sin(angleX / 180 * Math.PI),
                (float)Math.Tan(angleY / 180 * Math.PI),
                (float)-Math.Cos(angleX / 180 * Math.PI)
            );
            NormalizedXYZ.Normalize();
            
            x += NormalizedXYZ.X;
            y += NormalizedXYZ.Y;
            z += NormalizedXYZ.Z;
            
            float dist=0;
            while (dist < RayLenght) // радиус действия
            {
                dist+=0.001f;
                
                
               NormalizedXYZ = new Vector3(
                    (float)-Math.Sin(angleX / 180 * Math.PI),
                    (float)Math.Tan(angleY / 180 * Math.PI),
                    (float)-Math.Cos(angleX / 180 * Math.PI)
                );
                NormalizedXYZ.Normalize();
            
                x += NormalizedXYZ.X*0.01f;
                y += NormalizedXYZ.Y*0.01f;
                z += NormalizedXYZ.Z*0.01f;
                
                X = (int)Math.Round(x);
                Y = (int)Math.Round(y);
                Z = (int)Math.Round(z);
                
                if (Game.gameWorld.chunkManager.HasSolidBlock(new Vector3(X,Y,Z)))
                {
                    return new Vector3[] {new Vector3(X,Y,Z), new Vector3(oldX,oldY,oldZ)};
                    break;
                }
                oldX = X;
                oldY = Y;
                oldZ = Z;
            }
            return new Vector3[] {new Vector3(0,0,0), new Vector3(0,0,0)};
        }
        
    }
}
