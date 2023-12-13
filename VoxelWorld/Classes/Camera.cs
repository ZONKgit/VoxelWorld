using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace VoxelWorld.Classes
{
    public class Camera
    {
        public Vector3 position;

        public void ready()
        {
            GL.Enable(EnableCap.DepthTest); // Включение буффера глубины

            GL.LoadIdentity();
            GL.Frustum(-1, 1, -1, 1, 2, 6);

            GL.Translate(position);
        }

        public void renderProcess()
        {
            GL.ClearColor(new Color4(0.7f, 0.7f, 1f, 1.0f)); // Очищение экрана цветом...
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit); // Очищение буффера глубины

            GL.Translate(position);
        }

        public void physicsProcess()
        {

        }
    }
}
