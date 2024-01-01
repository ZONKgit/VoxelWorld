using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    public class TextureRect : GUI
    {
        float size = 1f;
        Vector4 UV;
        public int Texture;

        public TextureRect(Vector2 position,Vector4 uv, float size = 1f)
        {
            Color = new Color4(1, 0, 1, 1);
            this.size = size;
            Position = position;
            UV = uv;
        }
        
        public void RenderProcess()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, Texture);
            GL.Color4(new Color4(1f, 1f, 1f, 1f));
            GL.Begin(BeginMode.Quads);
                GL.TexCoord2(UV.X, UV.Y); GL.Vertex3(Position.X - size, Position.Y + size, -0.1f);
                GL.TexCoord2(UV.X, UV.W);GL.Vertex3(Position.X - size, Position.Y - size, -0.1f);
                GL.TexCoord2(UV.Z, UV.W); GL.Vertex3(Position.X + size, Position.Y - size, -0.1f);
                GL.TexCoord2(UV.Z, UV.Y); GL.Vertex3(Position.X + size, Position.Y + size, -0.1f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }
    }
}