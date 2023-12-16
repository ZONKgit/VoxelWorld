using System;
using OpenTK;
using System.Collections.Generic;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.World
{
    public class GameWorld
    {
        public Chunk[,] Chunks;

        Player player; // Создание игрока


        public void loadChunks()
        {
            int NumChunksX = 2;
            int NumChunksZ = 2;

            Chunks = new Chunk[NumChunksX, NumChunksZ];

            Vector2 centerChunkPosition = new Vector2(0, 0);

            for (int x = 0; x < NumChunksX; x++)
            {
                for (int z = 0; z < NumChunksZ; z++)
                {
                    int offsetX = x - NumChunksX / 2;
                    int offsetZ = z - NumChunksZ / 2;

                    Vector2 chunkPosition = centerChunkPosition + new Vector2(offsetX * Chunk.ChunkSizeX, offsetZ * Chunk.ChunkSizeZ);

                    Chunks[x, z] = new Chunk();
                    Chunks[x, z].Position = new Vector2(x * Chunk.ChunkSizeX, z * Chunk.ChunkSizeZ);
                    Chunks[x, z].Ready();
                }
            }
        } // Загрузка чанков
        public void RenderChunks()
        {
            for (int x = 0; x < 2; x++)
            {
                for (int z = 0; z < 2; z++)
                {
                    Chunk currentChunk = Chunks[x, z];

                    currentChunk.RenderProcess();
                }
            }
        } // Отрисовка чанков
        public Vector3 GlobalToLocalCoords(Vector3 globalPosition) //Глобальные координаты в координаты внутри чанка
        {
            Vector3 localCoords = new Vector3(globalPosition.X%16, globalPosition.Y%16, globalPosition.Z%16);
            if (localCoords.X < 0) localCoords.X = Chunk.ChunkSizeX + localCoords.X; // Если отрицательное значние X
            if (localCoords.Z < 0) localCoords.Z = Chunk.ChunkSizeZ + localCoords.Z; // Если отрицательное значние Y

            if (globalPosition.Y > Chunk.ChunkSizeY) localCoords.Y = Chunk.ChunkSizeY-1;// Если высота выше размера чанка
            if (globalPosition.Y < 0) localCoords.Y = 0;// Если высота ниже размера чанка

            return EngineMathHelper.FloorVector3(localCoords);
        }  // Кновертирует мировые координаты в координаты внутри чанка
        public bool IsChunkValid(Vector2 chunkCoords)
        {
            return chunkCoords.X >= 0 && chunkCoords.X < Chunks.GetLength(0) &&
                   chunkCoords.Y >= 0 && chunkCoords.Y < Chunks.GetLength(1);
        } // Проверяет существует ли чанк в позиции...
        public Vector2 GlobalToChunkCoords(Vector3 globalPosition)
        {
            int chunkX = (int)globalPosition.X / Chunk.ChunkSizeX;
            int chunkZ = (int)globalPosition.Z / Chunk.ChunkSizeZ;

            return new Vector2(chunkX, chunkZ);
        } // Кновертирует мировые координаты в координаты чанка
        public void SetBlock(Vector3 Pos)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(Pos);
            Vector3 localCoords = GlobalToLocalCoords(Pos);
            Chunks[(int)chunkCoords.X, (int)chunkCoords.Y].SetBlock(localCoords);
        } // Устанавливает блок в позиции...
        public int GetBlockAtPosition(Vector3 globalPosition)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(globalPosition);
            Vector3 localCoords = GlobalToLocalCoords(globalPosition);
            Chunk chunk = Chunks[(int)chunkCoords.X, (int)chunkCoords.Y];
            if (IsChunkValid(chunkCoords)) return chunk.ChunkData[Math.Abs((int)localCoords.X), (int)localCoords.Y, Math.Abs((int)localCoords.Z)];
            else return 0;
        }// Возвращает блок в позиции...
        public bool CheckBlock(Vector3 GlobalPos)
        {
            if (IsChunkValid(GlobalToChunkCoords(GlobalPos)))
            {
                if (GetBlockAtPosition(GlobalPos) == 0) return false;
                else return true;
            }
            return false;
        } // Проверяет есть ли блок в позиции...

        public void Ready()
        {
            player = new Player(this);
            player.Ready();
            loadChunks();
        }
        public void RenderProcess()
        {
            player.RenderProcess();
            RenderChunks();
        }
        public void PhysicsProcess()
        {
            player.PhysicsProcess();

        }

        public void OnResizeWindow(EventArgs e)
        {
            player.OnResizeWindow(e);
        }
    }
}
