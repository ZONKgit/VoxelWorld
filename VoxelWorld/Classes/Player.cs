using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Physics;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes
{
    class Player
    {
        GameWorld world;
        Camera camera = new Camera();
        HitBox hitbox = new HitBox(new Vector3(1f,2f,1f));

        public Vector3 Position = new Vector3(0, 12, 0);
        public Vector3 Rotation = new Vector3(0, 0, 0);

        public float moveSpeed = 0.1f;

        public Player(GameWorld world)
        {
            this.world = world;
            Input.OnMouseMove += HandleMouseMove;
        }

        public void Ready()
        {
            camera.Ready();
        }

        public static Vector3 FloorVector3(Vector3 vector)
        {
            float x = (float)Math.Floor(vector.X);
            float y = (float)Math.Floor(vector.Y);
            float z = (float)Math.Floor(vector.Z);

            return new Vector3(x, y, z);
        }

        public void RenderProcess()
        {
            camera.RenderProcess();

    


            // Настраиваем цвет и толщину линий
            GL.Color4(1.0f, 1.0f, 1.0f, 1.0f); // Красный цвет
            GL.LineWidth(2.0f); // Толщина линий

            // Рисуем линии hitbox'а
            GL.Begin(PrimitiveType.Lines);
            DrawBoxLines(hitbox.HitBoxSize / 2, Position);
            GL.End();









            if (world.GetBlockAtPosition(Position + new Vector3(1,0,0)+new Vector3(0, hitbox.HitBoxSize.Y/4,0 )) == 1){
                // Настраиваем цвет и толщину линий
                GL.Color4(1.0f, 0.0f, 0.0f, 1.0f); // Красный цвет
                GL.LineWidth(2.0f); // Толщина линий

                // Рисуем линии hitbox'а
                GL.Begin(PrimitiveType.Lines);
                DrawBoxLines(new Vector3(0.5f,0.5f,0.5f), FloorVector3(Position)+new Vector3(0.5f,0,0.5f) + new Vector3(1, 0, 0) + new Vector3(0, hitbox.HitBoxSize.Y / 4, 0));
                GL.End();
            }
            if (world.GetBlockAtPosition(Position + new Vector3(1, 0, 0) - new Vector3(0, hitbox.HitBoxSize.Y / 4, 0)) == 1){
                // Настраиваем цвет и толщину линий
                GL.Color4(1.0f, 0.0f, 0.0f, 1.0f); // Красный цвет
                GL.LineWidth(2.0f); // Толщина линий

                // Рисуем линии hitbox'а
                GL.Begin(PrimitiveType.Lines);
                DrawBoxLines(new Vector3(0.5f, 0.5f, 0.5f), FloorVector3(Position) + new Vector3(0.5f, 0, 0.5f) + new Vector3(1, 0, 0) - new Vector3(0, hitbox.HitBoxSize.Y / 4, 0));
                GL.End();
            }
        }

        private void DrawBoxLines(Vector3 halfSize, Vector3 Position)
        {
            float x = halfSize.X;
            float y = halfSize.Y;
            float z = halfSize.Z;

            // Рисуем линии, соединяющие вершины куба
            GL.Vertex3(-x+Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, -y + Position.Y, -z + Position.Z);
            GL.Vertex3(x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, y + Position.Y, -z + Position.Z);
            GL.Vertex3(x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, y + Position.Y, -z + Position.Z);
            GL.Vertex3(-x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, -y + Position.Y, -z + Position.Z);

            GL.Vertex3(-x + Position.X, -y + Position.Y, z + Position.Z); GL.Vertex3(x + Position.X, -y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, -y + Position.Y, z + Position.Z); GL.Vertex3(x + Position.X, y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, y + Position.Y, z + Position.Z); GL.Vertex3(-x + Position.X, y + Position.Y, z + Position.Z);
            GL.Vertex3(-x + Position.X, y + Position.Y, z + Position.Z); GL.Vertex3(-x + Position.X, -y + Position.Y, z + Position.Z);

            GL.Vertex3(-x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, -y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, -y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, y + Position.Y, z + Position.Z);
            GL.Vertex3(-x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, y + Position.Y, z + Position.Z);
        }

        public void PhysicsProcess()
        {
            Console.WriteLine(hitbox.IsOnFloor);

            camera.position = Position+camera.LocalPosition;
            camera.rotation = Rotation+camera.LocalRotation;
            camera.PhysicsProcess();

            Vector3 Dir = new Vector3(0,0,0);

            // Гравитация
            if (!hitbox.IsOnFloor)
            {
                Dir.Y -= 0.01f;
            }

            // Ходьба
            float YAngle = -Rotation.Y / 180f * (float)Math.PI;
            if (Input.IsKeyPressed(Input.KeyMovmentForward))
            {
                Dir.X -= (float)Math.Sin(YAngle) * moveSpeed;
                Dir.Z -= (float)Math.Cos(YAngle) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentRight))
            {
                Dir.X += (float)Math.Sin(YAngle + (float)Math.PI * 0.5) * moveSpeed;
                Dir.Z += (float)Math.Cos(YAngle + (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentLeft))
            {
                Dir.X += (float)Math.Sin(YAngle - (float)Math.PI * 0.5) * moveSpeed;
                Dir.Z += (float)Math.Cos(YAngle - (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentBackward))
            {
                Dir.X += (float)Math.Sin(YAngle) * moveSpeed;
                Dir.Z += (float)Math.Cos(YAngle) * moveSpeed;
            }


            // Прыжок и красться
            if (Input.IsKeyPressed(Input.KeyJump))
            {
                Dir.Y += moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyCrouch))
            {
                Dir.Y -= moveSpeed;
            }

            MoveAndCollide(Dir);
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

        private void MoveAndCollide(Vector3 Direction)
        {
               Position += Direction;
        }


        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
        }


        private Vector3 CalculateCollisionNormal(Vector3 position, Vector3 halfSize)
        {
            // Определение стороны коллизии (ближайшей поверхности блока)
            float xDistance = Math.Min(Math.Abs(position.X - halfSize.X), Math.Abs(position.X + halfSize.X));
            float yDistance = Math.Min(Math.Abs(position.Y - halfSize.Y), Math.Abs(position.Y + halfSize.Y));
            float zDistance = Math.Min(Math.Abs(position.Z - halfSize.Z), Math.Abs(position.Z + halfSize.Z));

            // Определение направления нормали
            if (xDistance < yDistance && xDistance < zDistance)
            {
                return new Vector3(Math.Sign(position.X), 0, 0);
            }
            else if (yDistance < zDistance)
            {
                return new Vector3(0, Math.Sign(position.Y), 0);
            }
            else
            {
                return new Vector3(0, 0, Math.Sign(position.Z));
            }
        }

        private Matrix4 CreateReflectionMatrix(Vector3 normal)
        {
            float reflectionMatrixData1 = 1 - 2 * normal.X * normal.X;
            float reflectionMatrixData2 = -2 * normal.X * normal.Y;
            float reflectionMatrixData3 = -2 * normal.X * normal.Z;

            float reflectionMatrixData4 = -2 * normal.Y * normal.X;
            float reflectionMatrixData5 = 1 - 2 * normal.Y * normal.Y;
            float reflectionMatrixData6 = -2 * normal.Y * normal.Z;

            float reflectionMatrixData7 = -2 * normal.Z * normal.X;
            float reflectionMatrixData8 = -2 * normal.Z * normal.Y;
            float reflectionMatrixData9 = 1 - 2 * normal.Z * normal.Z;

            return new Matrix4(
                reflectionMatrixData1, reflectionMatrixData2, reflectionMatrixData3, 0,
                reflectionMatrixData4, reflectionMatrixData5, reflectionMatrixData6, 0,
                reflectionMatrixData7, reflectionMatrixData8, reflectionMatrixData9, 0,
                0, 0, 0, 1
            );
        }



    }
}
