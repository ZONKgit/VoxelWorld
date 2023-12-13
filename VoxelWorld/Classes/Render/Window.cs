using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{
    public class Window : GameWindow
    {

        public Window() : base(800, 600, GraphicsMode.Default, "OpenTK Window")
        {
            VSync = VSyncMode.On;
        }


        float[] vertices =
            {
                0,0,1,
                1,0,1,
                1,1,1,

                1,0,0,
                0,1,0,
                1,1,0,
            };

        float[] colors =
            {
                1f,0f,0f,
                0f,1f,0f,
                0f,0f,1f,

                0f,1f,1f,
                1f,0f,1f,
                1f,1f,0f,
            };


        int vertexVBO;
        int colorVBO;

        Camera camera = new Camera(); // Создание камеры

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            camera.ready();

            // VBO
            vertexVBO = GL.GenBuffer(); // Запись VBO в переменную
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO); // Активация VBO
                // Занасенее данных в буффер
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Отключение активного буффера

            //ColorVBO Тоже самое что и с VBO
            colorVBO = GL.GenBuffer(); // Запись VBO в переменную
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorVBO); // Активация VBO
                                                                // Занасенее данных в буффер
            GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Отключение активного буффера

        }



        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            camera.physicsProcess();
            // Дополнительная логика обновления
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            camera.renderProcess();

            // Подключение VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
                GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);


            // Подключение colorVBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorVBO);
                GL.ColorPointer(3, ColorPointerType.Float, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);


            GL.EnableClientState(ArrayCap.VertexArray);// Разрешение испрользования массива вершин
            GL.EnableClientState(ArrayCap.ColorArray);
                GL.DrawArrays(BeginMode.Triangles, 0, vertices.Length); 
            GL.DisableClientState(ArrayCap.VertexArray);// Выключение отображеия по массиву вершин
            GL.DisableClientState(ArrayCap.ColorArray);
     

            SwapBuffers();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        static void Main(string[] args)
        {
            using (var game = new Window())
            {
                game.Run(60.0);
            }
        }
    }
}

