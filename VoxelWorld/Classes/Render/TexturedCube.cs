using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{
    class TexturedCube
    {
        public int texture;

        public TexturedCube(int textureId)
        {
            texture = textureId;
        }

        public void Draw(Vector3 position, Vector3 size)
        {
            GL.Color4(new Color4(1f, 1f, 1f, 1f));
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0); GL.Vertex3(position.X - size.X, position.Y - size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 0); GL.Vertex3(position.X + size.X, position.Y - size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 1); GL.Vertex3(position.X + size.X, position.Y + size.Y, position.Z - size.Z);
            GL.TexCoord2(0, 1); GL.Vertex3(position.X - size.X, position.Y + size.Y, position.Z - size.Z);

            GL.TexCoord2(0, 0); GL.Vertex3(position.X - size.X, position.Y - size.Y, position.Z + size.Z);
            GL.TexCoord2(1, 0); GL.Vertex3(position.X + size.X, position.Y - size.Y, position.Z + size.Z);
            GL.TexCoord2(1, 1); GL.Vertex3(position.X + size.X, position.Y + size.Y, position.Z + size.Z);
            GL.TexCoord2(0, 1); GL.Vertex3(position.X - size.X, position.Y + size.Y, position.Z + size.Z);

            GL.TexCoord2(0, 0); GL.Vertex3(position.X - size.X, position.Y - size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 0); GL.Vertex3(position.X + size.X, position.Y - size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 1); GL.Vertex3(position.X + size.X, position.Y - size.Y, position.Z + size.Z);
            GL.TexCoord2(0, 1); GL.Vertex3(position.X - size.X, position.Y - size.Y, position.Z + size.Z);

            GL.TexCoord2(0, 0); GL.Vertex3(position.X - size.X, position.Y + size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 0); GL.Vertex3(position.X + size.X, position.Y + size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 1); GL.Vertex3(position.X + size.X, position.Y + size.Y, position.Z + size.Z);
            GL.TexCoord2(0, 1); GL.Vertex3(position.X - size.X, position.Y + size.Y, position.Z + size.Z);

            GL.TexCoord2(0, 0); GL.Vertex3(position.X - size.X, position.Y - size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 0); GL.Vertex3(position.X - size.X, position.Y + size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 1); GL.Vertex3(position.X - size.X, position.Y + size.Y, position.Z + size.Z);
            GL.TexCoord2(0, 1); GL.Vertex3(position.X - size.X, position.Y - size.Y, position.Z + size.Z);

            GL.TexCoord2(0, 0); GL.Vertex3(position.X + size.X, position.Y - size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 0); GL.Vertex3(position.X + size.X, position.Y + size.Y, position.Z - size.Z);
            GL.TexCoord2(1, 1); GL.Vertex3(position.X + size.X, position.Y + size.Y, position.Z + size.Z);
            GL.TexCoord2(0, 1); GL.Vertex3(position.X + size.X, position.Y - size.Y, position.Z + size.Z);

            GL.End();
        }
    }
}
