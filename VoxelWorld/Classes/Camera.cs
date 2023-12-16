using System;
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

        //Элементы UI
        ColorRect crosshair = new ColorRect(new Color4(0f, 0f, 0f, 1f), 0.0006f);
        Text2D text = new Text2D(new Vector2(-19, 9), "Camera position", 0.01f);
        Text2D textRotation = new Text2D(new Vector2(-38, 16), "Camera rotation", 0.005f);
        Text2D textIsOnFloor = new Text2D(new Vector2(-38, 15), "Camera rotation", 0.005f);
        
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
            GL.Enable(EnableCap.DepthTest); // Включение буффера глубины

            GL.LoadIdentity();
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 600f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);


            text.Ready();
            textRotation.Ready();
            textIsOnFloor.Ready();
        }

        public void RenderProcess()
        {
            GL.ClearColor(new Color4(0.7f, 0.7f, 1f, 1.0f)); // Очищение экрана цветом...
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit); // Очищение буффера глубины

            // Рендер GUI Объектов
            crosshair.RenderProcess();
            text.RenderProcess();
            textRotation.RenderProcess();
            textIsOnFloor.RenderProcess();

            // Применение преобразовний
            GL.LoadIdentity();
            GL.Rotate(rotation.X, 1f, 0f, 0f);
            GL.Rotate(rotation.Y, 0f, 1f, 0f);
            GL.Rotate(rotation.Z, 0f, 0f, 1f);
            GL.Translate(-position);
        }

        public void PhysicsProcess()
        {
            // Вывод позиции
            text.Text = new Vector3(
                (float)Math.Round(position.X),
                (float)Math.Round(position.Y),
                (float)Math.Round(position.Z)
            ).ToString();
            // Вывод поворота
            textRotation.Text = rotation.ToString();
            textIsOnFloor.Text = player.hitbox.IsOnFloor.ToString();
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
