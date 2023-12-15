using System;
using OpenTK;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes
{
    class Player
    {
        Camera camera = new Camera();
        public Vector3 Position = new Vector3(0, 0, 0);
        public Vector3 Rotation = new Vector3(0, 0, 0);

        public float moveSpeed = 0.1f;

        public Player()
        {
            Input.OnMouseMove += HandleMouseMove;
        }

        public void Ready()
        {
            camera.Ready();
        }
        
        public void RenderProcess()
        {
            camera.RenderProcess();
        }

        public void PhysicsProcess()
        {
            camera.position = Position+camera.LocalPosition;
            camera.rotation = Rotation+camera.LocalRotation;
            camera.PhysicsProcess();

            // Ходьба
            float YAngle = -Rotation.Y / 180f * (float)Math.PI;
            if (Input.IsKeyPressed(Input.KeyMovmentForward))
            {
                Position.X -= (float)Math.Sin(YAngle) * moveSpeed;
                Position.Z -= (float)Math.Cos(YAngle) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentRight))
            {
                Position.X += (float)Math.Sin(YAngle + (float)Math.PI * 0.5) * moveSpeed;
                Position.Z += (float)Math.Cos(YAngle + (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentLeft))
            {
                Position.X += (float)Math.Sin(YAngle - (float)Math.PI * 0.5) * moveSpeed;
                Position.Z += (float)Math.Cos(YAngle - (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentBackward))
            {
                Position.X += (float)Math.Sin(YAngle) * moveSpeed;
                Position.Z += (float)Math.Cos(YAngle) * moveSpeed;
            }
            // Прыжок и красться
            if (Input.IsKeyPressed(Input.KeyJump))
            {
                Position.Y += moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyCrouch))
            {
                Position.Y -= moveSpeed;
            }
        }

        private void HandleMouseMove(Vector2 mouseRelative)
        {
            // Вращение камерой
            Rotation.Y += mouseRelative.X * Input.mouseSensitivity;
        }

        public void OnResizeWindow(EventArgs e)
        {
            camera.OnResizeWindow(e);
        }
    }
}
