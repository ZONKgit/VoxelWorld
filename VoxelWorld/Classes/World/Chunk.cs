using System;
using OpenTK;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Engine;
using SimplexNoise;

namespace VoxelWorld.Classes.World
{
    public class Chunk
    {
        public ChunkRenderer Renderer;
        ChunkManager Manager;

        // Размеры чанка
        public const int ChunkSizeX = 16;
        public const int ChunkSizeY = 256;
        public const int ChunkSizeZ = 16;

        private const int HeightSeed = 32;
        private const int MoutainSeed = 35;
        private const int HumiditySeed = 34;
        private const int TempiratureSeed = 33;

        public Vector2 Position { get; set; } //Позиция чанка по X и Z (Y всегда 0)

        // Данные чанка
        public Block[,,] ChunkData;

        public Chunk(Vector2 Pos, ChunkManager Manager)
        {
            Position = Pos;
            this.Manager = Manager;
        }
        
        public void GenerateChunk() // Эта функция выполняеться в отдельном потоке
        {
            // Инициализация массива ChunkData
            ChunkData = new Block[ChunkSizeX, ChunkSizeY, ChunkSizeZ];

            // Заполнение данных
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        ChunkData[x, y, z] = Blocks.air; }
                }
            }
            
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        //float mountainNoiseValue = Noise.GetNoiseValue(new Vector2(Position.X*ChunkSizeX+x, Position.Y*ChunkSizeZ+z)) * 10;
                        float heightNoiseValue = (Noise.CalcPixel2D((int)(Position.X * ChunkSizeX + x), (int)(Position.Y * ChunkSizeZ + z), 0.02f) / 255) * 16 + 3;
                        if (y < 13) ChunkData[x, y, z] = Blocks.water;// Вода
                        if (y < (int)heightNoiseValue) ChunkData[x, y, z] = Blocks.dirt; // Земля
                        if (y == (int)heightNoiseValue) ChunkData[x, y, z] = Blocks.grass; // Дерн
                        if (y < 14 && y > 10 && y == (int)heightNoiseValue) ChunkData[x, y, z] = Blocks.sand; // Песок
                    }
                }
            }

            Manager.WaitChunks.Remove(this);
            Manager.ReadyChunks.Enqueue(this);
        }

        public Block GetBlockAtPosition(Vector3 Pos)
        {
            // Приведем координаты к целочисленному формату
            int x = (int)Pos.X;
            int y = (int)Pos.Y;
            int z = (int)Pos.Z;

            // Проверим, находятся ли координаты в пределах чанка
            if (x < 0 || x > ChunkSizeX-1 || y < 0 || y > ChunkSizeY-1 || z < 0 || z > ChunkSizeZ-1)
            {
                // Координаты находятся за пределами чанка, вернем 0
                return Blocks.air;
            }

            // Вернем значение из массива ChunkData
            return ChunkData[x, y, z];
        }

        public void Ready()
        {
            Renderer = new ChunkRenderer(this);
            GenerateChunk();
            Renderer.GenerateChunkMesh(ChunkSizeX, ChunkSizeY, ChunkSizeZ, Position);
        }

        public void RenderProcess()
        {
            if (Renderer != null){Renderer.renderProcess();}
        }

        public void UpdateMesh()
        {
            Renderer.GenerateChunkMesh(ChunkSizeX, ChunkSizeY, ChunkSizeZ, Position);
        }
        
        public void SetBlock(Vector3 Pos, Block block)
        {
            Console.WriteLine(Pos);
            if (ChunkData[(int)Pos.X, (int)Pos.Y, (int)Pos.Z].Id != block.Id)
            {
                ChunkData[(int)Pos.X, (int)Pos.Y, (int)Pos.Z] = block;
                UpdateMesh();
            }
        }

        public void RemoveBlock(Vector3 Pos)
        {
            ChunkData[(int)Pos.X, (int)Pos.Y, (int)Pos.Z] = Blocks.air;
            UpdateMesh();
        }

        public void Dispose()// Очищение данных чанка
        {
            Renderer.Dispose();
            Renderer = null;
            ChunkData = null;
        }
        
    }
}
