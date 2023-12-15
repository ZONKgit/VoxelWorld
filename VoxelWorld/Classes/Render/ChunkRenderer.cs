using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Render
{
    class ChunkRenderer
    {
        Mesh mesh;
        Random random = new Random(); // Нужен для генерации блоков разных цветов

        List<float> vertices = new List<float>();
        List<float> colors = new List<float>();

        public void GenerateChunkMesh(int ChunkSizeX, int ChunkSizeY, int ChunkSizeZ, int[,,] ChunkData)
        {
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        if (ChunkData[x, y, z] == 1)
                        {
                            float randomR = (float)random.NextDouble();
                            float randomB = (float)random.NextDouble();

                            //Front side
                            if (Chunk.GetBlockAtPosition(new Vector3(x, y, z-1), ChunkData) == 0) GenerateFrontSide(new Vector3(x, y, z), randomR, randomB);
                            //Back side
                            if (Chunk.GetBlockAtPosition(new Vector3(x, y , z+1), ChunkData) == 0) GenerateBackSide(new Vector3(x, y, z), randomR, randomB);
                            //Right side
                            if (Chunk.GetBlockAtPosition(new Vector3(x-1, y, z), ChunkData) == 0) GenerateRightSide(new Vector3(x, y, z), randomR, randomB);
                            //Left side
                            if (Chunk.GetBlockAtPosition(new Vector3(x+1, y, z), ChunkData) == 0) GenerateLeftSide(new Vector3(x, y, z), randomR, randomB);
                            //Top side
                            if (Chunk.GetBlockAtPosition(new Vector3(x, y+1, z), ChunkData) == 0) GenerateTopSide(new Vector3(x, y, z), randomR, randomB);
                            //Bottom side
                            if (Chunk.GetBlockAtPosition(new Vector3(x, y-1, z), ChunkData) == 0) GenerateBottomSide(new Vector3(x, y, z), randomR, randomB);
                        }
                    }
                }
            }

            mesh = new Mesh(vertices.ToArray(), colors.ToArray());
            mesh.ready();
        }

        private void GenerateFrontSide(Vector3 Pos, float colorR, float colorB)
        {
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);

            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
        }
        private void GenerateBackSide(Vector3 Pos, float colorR, float colorB)
        {
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);

            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
        }
        private void GenerateRightSide(Vector3 Pos, float colorR, float colorB)
        {
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);

            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
        }
        private void GenerateLeftSide(Vector3 Pos, float colorR, float colorB)
        {
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);

            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
        }
        private void GenerateTopSide(Vector3 Pos, float colorR, float colorB)
        {
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);

            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 1f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
        }
        private void GenerateBottomSide(Vector3 Pos, float colorR, float colorB)
        {
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);

            vertices.Add(Pos.X + 1f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 1f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
            vertices.Add(Pos.X + 0f); vertices.Add(Pos.Y + 0f); vertices.Add(Pos.Z + 0f); colors.Add(colorR); colors.Add(1); colors.Add(colorB);
        }

        public void renderProcess()
        {
            if (mesh != null)
            {
                mesh.renderProcess();
            } 
        }

    }
}
