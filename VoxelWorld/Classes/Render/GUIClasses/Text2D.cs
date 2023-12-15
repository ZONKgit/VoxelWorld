using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    class Text2D
    {
        // Загрузка текстуры
        TextureLoader texLoader = new TextureLoader();
        int texture;

        public string Text = "Null 2D text";
        float Scale = 0.001f;
        Vector2 Position = new Vector2(-1f, -1.0f);

        public Text2D(Vector2 Pos, string Text = "Null 2D text", float Scale = 0.001f)
        {
            Position = Pos;
            this.Text = Text;
            this.Scale = Scale;
        }

        public void Ready()
        {
            texture = texLoader.LoadTexture("D:/Desktop/C#/VoxelWorld/Res/Fonts/Default.png");
        }

        public void RenderProcess()
        {
            drawText(Position, new Vector4(0f, 0f, 0f, 1f), Text);
        }


        public void drawText(Vector2 pos, Vector4 color, string text = "Null 2D text", float ind = 1)// ind Отступ у символов
        {
            int charNum = 0;

            foreach (char c in text)
            {
                charNum++;
                drawChar(pos+new Vector2(charNum * ind, 0), c, color);
            }
        }

        public void drawChar(Vector2 pos, char ch, Vector4 color)
        {
       

            GL.LoadIdentity();
            float[] modelviewMatrix = new float[16];
            GL.GetFloat(GetPName.ModelviewMatrix, modelviewMatrix);

            float scale = Scale;

            // Vertices
            float[] rectCoord = {
                0.0f*scale + pos.X*scale, 0.0f*scale + pos.Y*scale, -0.1f,
                1.0f*scale + pos.X*scale, 0.0f*scale + pos.Y*scale, -0.1f,
                1.0f*scale + pos.X*scale, 1.0f*scale + pos.Y*scale, -0.1f,
                0.0f*scale + pos.X*scale, 1.0f*scale + pos.Y*scale, -0.1f
            };


            //UV
            float[] rectTex = { 0.0f, 1.0f, -0.1f, 1.0f, -0.1f, 0.0f, 0.0f, -0.1f };

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.PushMatrix();
            GL.Color4(color.X, color.Y, color.Z, color.W);

            char c = ch;

            float charSize = 1 / 16.0f;
            int y = c >> 4;
            int x = c & 0b1111;

            float left = x * charSize;
            float right = (x + 1) * charSize;
            float top = y * charSize;
            float bottom = (y + 1) * charSize;

            rectTex[0] = rectTex[6] = left;
            rectTex[2] = rectTex[4] = right;
            rectTex[1] = rectTex[3] = bottom;
            rectTex[5] = rectTex[7] = top;



            float offsetX = modelviewMatrix[12];
            float offsetY = modelviewMatrix[13];
            float offsetZ = modelviewMatrix[14];

            // Создание копии массива
            float[] verticesWithOffset = new float[rectCoord.Length];
            Array.Copy(rectCoord, verticesWithOffset, rectCoord.Length);

            // Добавление оффсетов
            for (int i = 0; i < verticesWithOffset.Length; i += 3)
            {
                verticesWithOffset[i] += offsetX;
                verticesWithOffset[i + 1] += offsetY;
                verticesWithOffset[i + 2] += offsetZ;
            }

            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, verticesWithOffset);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, rectTex);
                GL.DrawArrays(BeginMode.TriangleFan, 0, 4);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.BindTexture(TextureTarget.Texture2D, 0);

         
        }
    }
}
