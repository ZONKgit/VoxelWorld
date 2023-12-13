// Mesh.cs
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{
    class Mesh
    {
        private int vertexBufferObject;
        private int vertexArrayObject;

        public Mesh()
        {
            // Создаем VAO для Mesh
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            // Используем VBO, созданный в Window.cs
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            // Устанавливаем указатель атрибута позиции
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Отвязываем VAO и VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Render()
        {
            // Используем VAO для рендеринга Mesh
            GL.BindVertexArray(vertexArrayObject);

            // Рендеринг Mesh (например, вызов GL.DrawArrays или GL.DrawElements)

            // Отвязываем VAO
            GL.BindVertexArray(0);
        }
    }
}
