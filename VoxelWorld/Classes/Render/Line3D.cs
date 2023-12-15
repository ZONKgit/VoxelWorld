using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace VoxelWorld.Classes.Render
{
    public class Line3D
    {
        public float[] Vertices;
        public Color4 Color;

        public Line3D(float[] Vertices, Color4 Color)
        {
            this.Vertices = Vertices;
            this.Color = Color;
        }

        public void Ready()
        {
        }

        public void RenderProcess()
        {
            GL.VertexPointer(3, VertexPointerType.Float, 0, Vertices);
            GL.EnableClientState(ArrayCap.VertexArray);// Разрешение испрользования массива вершин
                GL.Color4(Color);
                GL.DrawArrays(BeginMode.LineStrip, 0, Vertices.Length);
            GL.DisableClientState(ArrayCap.VertexArray);// Выключение отображеия по массиву вершин

        }

        public void PhysicsProcess()
        {

        }
    }
}
