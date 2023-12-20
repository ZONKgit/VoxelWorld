using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    class Text2D
    {
        int texture;
        float Scale = 0.001f;
        Vector2 Position = new Vector2(-1f, -1.0f);

        public string Text = "Null 2D text";

        public Text2D(Vector2 Pos, string Text = "Null 2D text", float Scale = 0.001f)
        {
            Position = Pos;
            this.Text = Text;
            this.Scale = Scale;
        }

        public void Ready()
        {
            texture = Game.FontTexture;
        }

        public void RenderProcess()
        {
            drawText(Position, new Vector4(0f, 0f, 0f, 1f), Text);
        }

        public void drawText(Vector2 pos, Vector4 color, string text = "Null 2D text", float ind = 1)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Color4(color.X, color.Y, color.Z, color.W);

            float charSize = 1 / 16.0f;
            float scale = Scale;

            foreach (char c in text)
            {
                float left = (c & 0b1111) * charSize;
                float right = left + charSize;
                float top = (c >> 4) * charSize;
                float bottom = top + charSize;

                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(left, bottom);
                GL.Vertex3(0.0f * scale + pos.X * scale, 0.0f * scale + pos.Y * scale, -0.1f);
                GL.TexCoord2(right, bottom);
                GL.Vertex3(1.0f * scale + pos.X * scale, 0.0f * scale + pos.Y * scale, -0.1f);
                GL.TexCoord2(right, top);
                GL.Vertex3(1.0f * scale + pos.X * scale, 1.0f * scale + pos.Y * scale, -0.1f);
                GL.TexCoord2(left, top);
                GL.Vertex3(0.0f * scale + pos.X * scale, 1.0f * scale + pos.Y * scale, -0.1f);
                GL.End();

                pos.X += ind;
            }

            GL.Disable(EnableCap.Texture2D);
        }
    }

}
