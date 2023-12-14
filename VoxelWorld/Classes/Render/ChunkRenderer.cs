using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace VoxelWorld.Classes.Render
{
    class ChunkRenderer
    {
        // Размеры чанка
        private const int ChunkSizeX = 16;
        private const int ChunkSizeY = 256;
        private const int ChunkSizeZ = 16;

        // Данные чанка
        private int[,,] ChunkData;

        Mesh mesh;

        public ChunkRenderer()
        {
            GenerateChunk();
        }

        public void GenerateChunk()
        {
            // Инициализация массива ChunkData
            ChunkData = new int[ChunkSizeX, ChunkSizeY, ChunkSizeZ];

            // Заполнение данных
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        // Заполняем значениями от 0 до 4 включительно по последней координате
                        ChunkData[x, y, z] = 1;
                    }
                }
            }
            GenerateChunkMesh();
        }

        private void GenerateChunkMesh()
        {
            List<float> vertices = new List<float>();
            List<float> colors = new List<float>();

            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        if (ChunkData[x, y, z] == 1)
                        {
                            vertices.Add(x + 0f); vertices.Add(y + 0f); vertices.Add(z + 0f);
                            colors.Add(x); colors.Add(y); colors.Add(z);

                            vertices.Add(x + 0f); vertices.Add(y + 1f); vertices.Add(z + 0f);
                            colors.Add(x);colors.Add(y); colors.Add(z);

                            vertices.Add(x + 1f); vertices.Add(y + 1f); vertices.Add(z + 0f);
                            colors.Add(x); colors.Add(y); colors.Add(z);
                        }
                    }
                }
            }

            mesh = new Mesh(vertices.ToArray(), colors.ToArray());
            mesh.ready();
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
