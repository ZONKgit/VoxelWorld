using System;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    public class ColorRect : GUI
    {
        float size = 0.001f;

        public ColorRect(Color4 Color, float size = 0.001f)
        {
            this.size = size;
        }

        public void RenderProcess()
        {
            GL.Color4(this.Color);
            GL.Begin(BeginMode.Quads);
                GL.Vertex3(size, -size, -0.1f);
                GL.Vertex3(size, size, -0.1f);
                GL.Vertex3(-size, size, -0.1f);
                GL.Vertex3(-size, -size, -0.1f);
            GL.End();
        }
    }
}
