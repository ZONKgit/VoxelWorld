using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.EngineMath;
using Object = VoxelWorld.Classes.Engine.Object;

namespace VoxelWorld.Classes
{
    public class Camera: Object
    {
        public Camera(Player player)
        {
            Input.OnMouseMove += HandleMouseMove;
        }

        private float FOV = 90;
        

        public override void Ready()
        {
            LocalPosition = new Vector3(0, 0.65f, 0);
            Game.camera = this;
            
            GL.Enable(EnableCap.DepthTest); // Включение буффера глубины

            GL.LoadIdentity();
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 600f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public override void RenderProcess()
        {
            GL.ClearColor(new Color4(0.7f, 0.7f, 1f, 1.0f)); // Очищение экрана цветом...
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit); // Очищение буффера глубины
            
            
            // Применение преобразовний
            GL.LoadIdentity();
            GL.Rotate(Rotation.X, 1f, 0f, 0f);
            GL.Rotate(Rotation.Y, 0f, 1f, 0f);
            GL.Rotate(Rotation.Z, 0f, 0f, 1f);
            GL.Translate(-Position);

            // Зум камеры
            if (Input.IsKeyPressed(Input.KeyCameraZoom)) { FOV = Linear.Interpolation(FOV, 35, 0.001f); }
            else { FOV = Linear.Interpolation(FOV, 90, 0.001f); }
            FOV = Linear.Clamp(FOV, 35.0f, 90.0f);

            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), aspectRatio, 0.1f, 600f);
        
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            
            // Отладочный прицел
            if(Game.isDrawDebug) DebugDrawCrosshair();
        }

        void DebugDrawCrosshair()
        {
            float angleX = -Rotation.Y;
            float angleY = -Rotation.X;
            float angleZ = Rotation.Z;
            
            float x1 = Position.X;
            float y1 = Position.Y;
            float z1 = Position.Z;

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
        public override void PhysicsProcess()
        {

        }
        private void HandleMouseMove(Vector2 mouseRelative)
        {
            if (!Game.window.CursorVisible) {
                // Вращение камерой
                LocalRotation.X += mouseRelative.Y * Input.mouseSensitivity;
                if (LocalRotation.X > 90.0f) LocalRotation.X = 90.0f; // Ограничение от -90 до 90 градусов
                else if (LocalRotation.X < -90.0f) LocalRotation.X = -90.0f;
            }
        }
        public override void OnResizeWindow(EventArgs e)
        {
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 1000f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
