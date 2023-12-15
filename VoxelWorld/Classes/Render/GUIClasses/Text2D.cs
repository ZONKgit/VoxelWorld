using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    class Text2D
    {
        TextureLoader texLoader = new TextureLoader();
        int texture;

        // Загрузка текстуры
        public void Ready()
        {
            texture = texLoader.LoadTexture("D:/Desktop/C#/VoxelWorld/Res/Fonts/Default.png");
        }

        public void RenderProcess()
        {
            drawText(new Vector3(0f, -1f, 0f), new Vector4(0,1,1,1));
        }

        public void drawText(Vector3 pos, Vector4 color, string text = "Null text", float ind = 1)// ind Отступ у символов
        {
            int charNum = 0;

            foreach (char c in text)
            {
                charNum++;
                drawChar(new Vector2(charNum * ind, 0), c, color);
            }
        }

        public void drawChar(Vector2 pos, char ch, Vector4 color)
        {
            float[] rectCoord = {
                0.0f +pos.X, 0.0f + pos.Y,
                1.0f + pos.X, 0.0f + pos.Y,
                1.0f + pos.X, 1.0f + pos.Y,
                0.0f + pos.X, 1.0f + pos.Y };


            float[] rectTex = { 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f };

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

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.VertexPointer(2, VertexPointerType.Float, 0, rectCoord);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, rectTex);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, 4);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);

            GL.PopMatrix();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
