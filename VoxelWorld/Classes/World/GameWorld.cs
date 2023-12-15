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
        Chunk chunk = new Chunk();
        Text2D text = new Text2D();

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

        public void Ready()
        {
            camera.Ready();
            chunk.Ready();
            text.Ready();
            loadChunks();
        }

        public void RenderProcess()
        {
            camera.RenderProcess();
            chunk.RenderProcess();
            text.RenderProcess();
            RenderChunks();
        }

        public void PhysicsProcess()
        {
            camera.PhysicsProcess();
        }

        public void OnResizeWindow(EventArgs e)
        {
            camera.OnResizeWindow(e);
        }
    }
}
