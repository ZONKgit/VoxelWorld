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
            GL.Color4(Color);
            GL.Begin(BeginMode.Lines);

            for (int i = 2; i < Vertices.Length; i+=3)
            {
                Console.WriteLine("x: {0}, \ny{1}, \nz{2}", Vertices[i], Vertices[i-1], Vertices[i-2]);
                GL.Vertex3(Vertices[i], Vertices[i-1], Vertices[i-2]);
            }
            
            GL.End();
        }

        public void PhysicsProcess()
        {

        }
    }
}
