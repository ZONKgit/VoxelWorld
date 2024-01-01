using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    public class ColorRect : GUI
    {
        float size = 0.001f;

        public ColorRect(Vector2 position, Color4 Color, float size = 0.001f)
        {
            this.size = size;
            Position = position;
        }

        public void RenderProcess()
        {
            GL.Color4(Color);
            GL.Begin(BeginMode.Quads);
                GL.Vertex3(Position.X-(size), Position.Y-(-size), -0.1f);
                GL.Vertex3(Position.X-(size), Position.Y-(size), -0.1f);
                GL.Vertex3(Position.X-(-size), Position.Y-(size), -0.1f);
                GL.Vertex3(Position.X-(-size), Position.Y-(-size), -0.1f);
            GL.End();
        }
    }
}
