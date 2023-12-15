using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{

    public class GUI
    {
        public float[] vertices = {};
        public Color4 Color = new Color4(0f, 0f, 0f, 1f);

        public void RenderProcess()
        {
            GL.LoadIdentity();
            float[] modelviewMatrix = new float[16];
            GL.GetFloat(GetPName.ModelviewMatrix, modelviewMatrix);

            float offsetX = modelviewMatrix[12];
            float offsetY = modelviewMatrix[13];
            float offsetZ = modelviewMatrix[14];

            // Создание копии массива
            float[] verticesWithOffset = new float[vertices.Length];
            Array.Copy(vertices, verticesWithOffset, vertices.Length);

            // Добавление оффсетов
            for (int i = 0; i < verticesWithOffset.Length; i += 3)
            {
                verticesWithOffset[i] += offsetX;
                verticesWithOffset[i + 1] += offsetY;
                verticesWithOffset[i + 2] += offsetZ;
            }

            GL.Color4(Color);

            GL.VertexPointer(3, VertexPointerType.Float, 0, verticesWithOffset);
            GL.EnableClientState(ArrayCap.VertexArray);
                GL.DrawArrays(BeginMode.TriangleFan, 0, 4);
            GL.DisableClientState(ArrayCap.VertexArray);
        }
    }
}
