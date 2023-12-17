// Mesh.cs

using System;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{
    class Mesh
    {

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

        public Mesh(float[] vertices, float[] colors)
        {
            this.vertices = vertices;
            this.colors = colors;
        }
    
        public void UpdateMesh(float[] newVertices, float[] newColors)
        {
            vertices = newVertices;
            colors = newColors;
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
            GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), colors, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Отключение активного буффера
        }

        
        public void Ready()
        {
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
            GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), colors, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Отключение активного буффера
        }

        public void RenderProcess()
        {
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
        }
    }
}
