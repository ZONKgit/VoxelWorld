using System;
using OpenTK;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.World
{
    class GameWorld
    {
        Chunk[,] Chunks;

        Camera camera = new Camera(); // Создание камеры
        

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
                    Chunks[x, z].Position = chunkPosition;
                    Chunks[x, z].Ready();
                }
            }
        }




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
        }

        public static Vector3 GlobalToLocalCoords(Vector3 globalPosition)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(globalPosition);

            int localX = (int)(globalPosition.X - chunkCoords.X * Chunk.ChunkSizeX);
            int localY = (int)globalPosition.Y;
            int localZ = (int)(globalPosition.Z - chunkCoords.Y * Chunk.ChunkSizeZ);

            // Обработка отрицательных координат внутри чанка
            if (localX < 0)
                localX += Chunk.ChunkSizeX;

            if (localZ < 0)
                localZ += Chunk.ChunkSizeZ;

            return new Vector3(localX-1, localY, localZ-1);
        }


        public bool IsChunkValid(Vector2 chunkCoords)
        {
            return chunkCoords.X >= 0 && chunkCoords.X < Chunks.GetLength(0) &&
                   chunkCoords.Y >= 0 && chunkCoords.Y < Chunks.GetLength(1);
        }

        public static Vector2 GlobalToChunkCoords(Vector3 globalPosition)
        {
            int chunkX = (int)Math.Floor(globalPosition.X / Chunk.ChunkSizeX);
            int chunkZ = (int)Math.Floor(globalPosition.Z / Chunk.ChunkSizeZ);

            return new Vector2(chunkX+1, chunkZ+1);
        }

        public void Ready()
        {
            camera.Ready();
            loadChunks();
        }
        public void RenderProcess()
        {
            camera.RenderProcess();
            RenderChunks();
        }
        public void PhysicsProcess()
        {
            camera.PhysicsProcess();

            Vector3 globalCoords = camera.position;
            Vector2 chunkCoords = GlobalToChunkCoords(globalCoords);

            //Console.WriteLine($"Pos {globalCoords} ChunkPos {chunkCoords}");
            //Console.WriteLine($"ChunkPos {chunkCoords} ChunkBlocks {GlobalToLocalCoords(globalCoords)}");

            //Console.WriteLine(Chunks[(int)chunkCoords.X, (int)chunkCoords.Y].Position);

            if (Input.IsKeyJustPressed(Input.KeyF))
            {
                Vector3 localCoords = GlobalToLocalCoords(globalCoords);
                Chunks[(int)chunkCoords.X, (int)chunkCoords.Y].SetBlock(localCoords);
            }
        }
        public void OnResizeWindow(EventArgs e)
        {
            camera.OnResizeWindow(e);
        }
    }
}
