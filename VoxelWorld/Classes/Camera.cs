using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes
{
    public class Camera
    {
        Player player;
        
        public Camera(Player player)
        {
            this.player = player;
            Input.OnMouseMove += HandleMouseMove;
        }

        public Vector3 LocalRotation = new Vector3(0, 0, 0);
        public Vector3 LocalPosition = new Vector3(0, 0.65f, 0);

        public Vector3 rotation = new Vector3(0, 0, 0);
        public Vector3 position = new Vector3(0, 0, 0);

        public void Ready()
        {
            Game.camera = this;
            
            GL.Enable(EnableCap.DepthTest); // Включение буффера глубины

            GL.LoadIdentity();
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 600f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void RenderProcess()
        {
            GL.ClearColor(new Color4(0.7f, 0.7f, 1f, 1.0f)); // Очищение экрана цветом...
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit); // Очищение буффера глубины
            
            
            // Применение преобразовний
            GL.LoadIdentity();
            GL.Rotate(rotation.X, 1f, 0f, 0f);
            GL.Rotate(rotation.Y, 0f, 1f, 0f);
            GL.Rotate(rotation.Z, 0f, 0f, 1f);
            GL.Translate(-position);
            
            
            // Отладочный прицел
            if(Game.isDrawDebug) DebugDrawCrosshair();
        }

        void DebugDrawCrosshair()
        {
            float angleX = -rotation.Y;
            float angleY = -rotation.X;
            float angleZ = rotation.Z;
            
            float x1 = position.X;
            float y1 = position.Y;
            float z1 = position.Z;

            float dist = 0.1f;
   
            float x2 = x1 + (float)-Math.Sin(angleX / 180 * Math.PI) * dist;
            float y2 = y1 + (float)Math.Tan(angleY / 180 * Math.PI) * dist;
            float z2 = z1 + (float)-Math.Cos(angleX / 180 * Math.PI) * dist;
            
            GL.LineWidth(2); // Толщина линий
            GL.Begin(PrimitiveType.Lines);
            
            GL.Color4(Color.Red);
            GL.Vertex3(0 + x2, -0 + y2, 0 + z2);
            GL.Vertex3(0.01 + x2, -0 + y2, 0 + z2);
            GL.Color4(Color.Green);
            GL.Vertex3(0 + x2, 0 + y2, 0 + z2);
            GL.Vertex3(0 + x2, 0.01 + y2, 0 + z2);
            GL.Color4(Color.Blue);
            GL.Vertex3(0 + x2, 0 + y2, 0 + z2);
            GL.Vertex3(0 + x2, 0 + y2, 0.01 + z2);
           

            GL.End();
        }
        public void PhysicsProcess()
        {

        }
        private void HandleMouseMove(Vector2 mouseRelative)
        {
            // Вращение камерой
            LocalRotation.X += mouseRelative.Y * Input.mouseSensitivity;
            if (LocalRotation.X > 90.0f) LocalRotation.X = 90.0f; // Ограничение от -90 до 90 градусов
            else if (LocalRotation.X < -90.0f) LocalRotation.X = -90.0f;
        }
        public void OnResizeWindow(EventArgs e)
        {
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 1000f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
