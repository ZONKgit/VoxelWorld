// Mesh.cs
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vertex
    {
        public float X, Y, Z;        // Координаты вершины
        public float R, G, B, A;     // Цвет (красный, зеленый, синий, альфа)
        public float U, V;           // Координаты текстуры (UV)
    }

    class Mesh
    {
        Vertex[] vertices;
        private int verticesLength;

        int vbo;
        int vao;

        public Mesh(Vertex[] vertices)
        {
            this.vertices = vertices;
        }

        public void UpdateMesh(Vertex[] newVertices)
        {
            vertices = newVertices;

            // VBO
            vbo = GL.GenBuffer(); 
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, BlittableValueType.StrideOf(vertices) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            verticesLength = vertices.Length;
            
            vertices = null;
            
        }

        public void Ready()
        {
            UpdateMesh(vertices);
        }

        public void RenderProcess()
        {
            
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, Game.BlocksTexture);
            
            GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(vertices), IntPtr.Zero);
            GL.ColorPointer(4, ColorPointerType.Float, BlittableValueType.StrideOf(vertices), (IntPtr)(3 * sizeof(float))); // Смещение 3*sizeof(float)
            GL.TexCoordPointer(2, TexCoordPointerType.Float, BlittableValueType.StrideOf(vertices), (IntPtr)(7 * sizeof(float))); // Смещение 7*sizeof(float)

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.DrawArrays(BeginMode.Triangles, 0, verticesLength);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
        }

        public void Dispose()
        {
            vertices = null;
            GL.DeleteBuffer(vbo);
        }
    }
}
