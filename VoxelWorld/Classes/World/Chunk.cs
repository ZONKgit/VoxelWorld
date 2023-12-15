using System;
using OpenTK;
using VoxelWorld.Classes.Render;

namespace VoxelWorld.Classes.World
{
    public class Chunk
    {
        ChunkRenderer Renderer = new ChunkRenderer();

        // Размеры чанка
        public const int ChunkSizeX = 16;
        public const int ChunkSizeY = 256;
        public const int ChunkSizeZ = 16;

        // Данные чанка
        public int[,,] ChunkData;

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
            Renderer.GenerateChunkMesh(ChunkSizeX, ChunkSizeY, ChunkSizeZ, ChunkData);
        }

        static public int GetBlockAtPosition(Vector3 Pos, int[,,] ChunkData)
        {
            // Приведем координаты к целочисленному формату
            int x = (int)Pos.X;
            int y = (int)Pos.Y;
            int z = (int)Pos.Z;

            // Проверим, находятся ли координаты в пределах чанка
            if (x < 0 || x > ChunkSizeX-1 || y < 0 || y > ChunkSizeY-1 || z < 0 || z > ChunkSizeZ-1)
            {
                // Координаты находятся за пределами чанка, вернем 0
                return 0;
            }

            // Вернем значение из массива ChunkData
            return ChunkData[x, y, z];
        }

        public void ready()
        {
            GenerateChunk();
        }

        public void renderProcess()
        {
            Renderer.renderProcess();
        }

    }
}
