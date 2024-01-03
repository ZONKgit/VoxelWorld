using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render
{
    class TexturedCube
    {
        public int texture = 0;
        public int[] TextureFaces { get; set; } // ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN
        
        //                                                                      ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN
        public void Draw(Vector3 position, Vector3 size, float rotationAngle, Vector3 rotationAxis, int[] textureFaces)
        {
            GL.Color4(new Color4(1f, 1f, 1f, 1f));
            GL.Enable(EnableCap.Texture2D); 
            GL.BindTexture(TextureTarget.Texture2D, texture);
            
            GL.PushMatrix(); // Save the current matrix state

            // Apply translation
            GL.Translate(position);

            // Apply rotation
            GL.Rotate(rotationAngle, rotationAxis);
            
            GL.Begin(PrimitiveType.Quads);

            //Front
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[2]).Z, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, -size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[2]).X, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(-size.X, -size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[2]).X, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[2]).Z, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(size.X, size.Y, -size.Z);
            //Back
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[4]).Z, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[4]).Z, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(-size.X, -size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[4]).X, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, -size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[4]).X, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(size.X, size.Y, size.Z);
            //Down
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[5]).X, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(-size.X, -size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[5]).X, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, -size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[5]).Z, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(size.X, -size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[5]).Z, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, -size.Y, size.Z);
            //Up
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[0]).X, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[0]).X, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[0]).Z, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[0]).Z, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, size.Y, size.Z);
            //Left
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[1]).Z, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[1]).Z, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(-size.X, -size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[1]).X, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(-size.X, -size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[1]).X, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(-size.X, size.Y, size.Z);
            
            //Right
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[3]).Z, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, -size.Y, size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[3]).X, TextureHelper.IDToUVCoords(0).W); GL.Vertex3(size.X, -size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[3]).X, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(size.X, size.Y, -size.Z);
            GL.TexCoord2(TextureHelper.IDToUVCoords(textureFaces[3]).Z, TextureHelper.IDToUVCoords(0).Y); GL.Vertex3(size.X, size.Y, size.Z);


            GL.End();
            GL.PopMatrix(); // Restore the previous matrix state
        }
    }
}
