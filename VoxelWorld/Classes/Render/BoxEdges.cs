using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{
    class BoxEdges
    {
        static public void DrawBoxEdges(Vector3 halfSize, Vector3 Position, Color4 Color, float Width)
        {
            float x = halfSize.X;
            float y = halfSize.Y;
            float z = halfSize.Z;

            GL.LineWidth(Width); // Толщина линий
            GL.Color4(Color); // Цвет линий
            GL.Begin(PrimitiveType.Lines);

            // Рисуем линии, соединяющие вершины куба
            GL.Vertex3(-x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, -y + Position.Y, -z + Position.Z);
            GL.Vertex3(x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, y + Position.Y, -z + Position.Z);
            GL.Vertex3(x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, y + Position.Y, -z + Position.Z);
            GL.Vertex3(-x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, -y + Position.Y, -z + Position.Z);

            GL.Vertex3(-x + Position.X, -y + Position.Y, z + Position.Z); GL.Vertex3(x + Position.X, -y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, -y + Position.Y, z + Position.Z); GL.Vertex3(x + Position.X, y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, y + Position.Y, z + Position.Z); GL.Vertex3(-x + Position.X, y + Position.Y, z + Position.Z);
            GL.Vertex3(-x + Position.X, y + Position.Y, z + Position.Z); GL.Vertex3(-x + Position.X, -y + Position.Y, z + Position.Z);

            GL.Vertex3(-x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, -y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, -y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, -y + Position.Y, z + Position.Z);
            GL.Vertex3(x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(x + Position.X, y + Position.Y, z + Position.Z);
            GL.Vertex3(-x + Position.X, y + Position.Y, -z + Position.Z); GL.Vertex3(-x + Position.X, y + Position.Y, z + Position.Z);

            GL.End();
        }
    }
}
