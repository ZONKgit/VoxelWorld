using System;
using OpenTK;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.World
{
    public class Chunk
    {
        Perlin2D Noise = new Perlin2D();
        ChunkRenderer Renderer = new ChunkRenderer();

        // Размеры чанка
        public const int ChunkSizeX = 16;
        public const int ChunkSizeY = 256;
        public const int ChunkSizeZ = 16;

        public Vector2 Position = new Vector2(0, 0); //Позиция чанка по X и Z (Y всегда 0) Кратно размеру чанка

        // Данные чанка
        public int[,,] ChunkData;

        public void GenerateChunk()
        {
            // Инициализация массива ChunkData
            ChunkData = new int[ChunkSizeX, ChunkSizeY, ChunkSizeZ];

            // Заполнение данных
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        float noiseValue = Noise.GetNoiseValue(new Vector2(x, z)) * 6 + 3;
                        int _y = (int)noiseValue;
                        if (y <= _y) ChunkData[x, y, z] = 1;
                    }
                }
            }
            Renderer.GenerateChunkMesh(ChunkSizeX, ChunkSizeY, ChunkSizeZ, ChunkData, Position);
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

        public void Ready()
        {
            GenerateChunk();
        }

        public void RenderProcess()
        {
            Renderer.renderProcess();
        }

        public void SetBlock(Vector3 Pos)
        {
            ChunkData[(int)Pos.X, (int)Pos.Y, (int)Pos.Z] = 1;
            Renderer.GenerateChunkMesh(ChunkSizeX, ChunkSizeY, ChunkSizeZ, ChunkData, Position);
        }

        public void RemoveBlock(Vector3 Pos)
        {
            ChunkData[(int)Pos.X, (int)Pos.Y, (int)Pos.Z] = 0;
            Renderer.GenerateChunkMesh(ChunkSizeX, ChunkSizeY, ChunkSizeZ, ChunkData, Position);
        }
    }
}
