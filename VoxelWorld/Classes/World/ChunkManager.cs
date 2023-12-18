using System;
using OpenTK;
using System.Linq;
using VoxelWorld.Classes.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoxelWorld.Classes.Render;


namespace VoxelWorld.Classes.World
{
    public class ChunkManager
    {
        public Player player;
        public int renderDistance = 4;
        private List<Chunk> Chunks = new List<Chunk>();

        private Vector2 oldPlayerChunkPos = new Vector2(999, 999);
        public List<Chunk> WaitChunks = new List<Chunk>(); // Чанки которые генерируються в другом потоке
        public Queue<Chunk> ReadyChunks = new Queue<Chunk>(); // Чанки которые уже сгенерировались в другом потоке

        public ChunkManager(Player player)
        {
            this.player = player;
        }


        public void Ready()
        {
        }

        // Отрисовка чанков
        public void RenderProcess()
        {
            // Рендерим все чанки
            foreach (var chunk in Chunks)
            {
                chunk.RenderProcess();
            }

            // Добовляем чанки которые сгенерировались в другом потоке
            if (ReadyChunks.Count > 0)
            {
                ReadyChunks.Peek().Renderer = new ChunkRenderer(ReadyChunks.Peek());
                ReadyChunks.Peek().Renderer.GenerateChunkMesh(Chunk.ChunkSizeX, Chunk.ChunkSizeY, Chunk.ChunkSizeZ, ReadyChunks.Peek().Position);
                Chunks.Add(ReadyChunks.Peek());
                ReadyChunks.Dequeue();
            }
        }


        public void PhysicsProcess()
        {
            // Загруза чанков
            if (GlobalToChunkCoords(player.Position) != oldPlayerChunkPos)
            {
                Vector2 currentPlayerChunk = GlobalToChunkCoords(player.Position);

                for (int x = (int)currentPlayerChunk.X - renderDistance; x < currentPlayerChunk.X + renderDistance; x++)
                {
                    for (int z = (int)currentPlayerChunk.Y - renderDistance;
                         z < currentPlayerChunk.Y + renderDistance;
                         z++)
                    {
                        // Если нет чанка с такой позицией
                        if (!Chunks.Any(chunk => chunk.Position == new Vector2(x * Chunk.ChunkSizeX, z * Chunk.ChunkSizeZ)))
                        {
                            Console.WriteLine(currentPlayerChunk);
                            LoadChunk(x*Chunk.ChunkSizeX, z*Chunk.ChunkSizeZ);
                        }
                    }
                }

                oldPlayerChunkPos = GlobalToChunkCoords(player.Position);
            }
            
            // Выгрузка чанков
            for (int i = 0; i < Chunks.Count; i++)
            {
                // Получаем позицию чанка
                Vector2 chunkPosition = Chunks[i].Position;
            
                // Проверяем, выходит ли чанк за пределы радиуса
                if (Math.Abs(chunkPosition.X - player.Position.X) > renderDistance*Chunk.ChunkSizeX ||
                    Math.Abs(chunkPosition.Y - player.Position.Z) > renderDistance*Chunk.ChunkSizeZ)
                {
                    Chunks.RemoveAt(i);
                    i--; // Уменьшаем i, чтобы не пропустить следующий элемент после удаления
                }
            }


        }

        public void LoadChunk(int x, int z)
        {
            // Создаем новый чанк и добавляем его в лист
            Chunk newChunk = new Chunk(new Vector2(x, z), this);
            WaitChunks.Add(newChunk);
            Task.Run(() => newChunk.GenerateChunk());
            //Chunks.Add(newChunk);
            //newChunk.Ready();
        }

        // Конвертирует мировые координаты в координаты внутри чанка
        public Vector3 GlobalToLocalCoords(Vector3 globalPosition)
        {
            Vector3 localCoords = new Vector3(globalPosition.X % 16, globalPosition.Y % 16, globalPosition.Z % 16);
            if (localCoords.X < 0) localCoords.X = Chunk.ChunkSizeX + localCoords.X; // Если отрицательное значние X
            if (localCoords.Z < 0) localCoords.Z = Chunk.ChunkSizeZ + localCoords.Z; // Если отрицательное значние Y

            if (globalPosition.Y > Chunk.ChunkSizeY)
                localCoords.Y = Chunk.ChunkSizeY - 1; // Если высота выше размера чанка
            if (globalPosition.Y < 0) localCoords.Y = 0; // Если высота ниже размера чанка

            return EngineMathHelper.FloorVector3(localCoords);
        }
        

        // Конвертирует мировые координаты в координаты чанка
        public Vector2 GlobalToChunkCoords(Vector3 globalPosition)
        {
            int chunkX = (int)(globalPosition.X / Chunk.ChunkSizeX);
            int chunkZ = (int)(globalPosition.Z / Chunk.ChunkSizeZ);

            return new Vector2(chunkX, chunkZ);
        }

        // Устанавливает блок в позиции...
        public void SetBlock(Vector3 Pos)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(Pos);
            Vector3 localCoords = GlobalToLocalCoords(Pos);

            // Проверяем, существует ли чанк
            Chunk chunk = Chunks.Cast<Chunk>().FirstOrDefault(c => c.Position == chunkCoords);

            // Если чанк существует, устанавливаем блок
            if (chunk != null)
            {
                chunk.SetBlock(localCoords);
            }
        }

        // Возвращает чанк по его позиции
        public Chunk GetChunk(Vector2 chunkCoords)
        {
            foreach (var chunk in Chunks)
            {
                if (chunk.Position == chunkCoords) return chunk;
            }

            return Chunks[0];
        }
        // Есть ли чанк в позиции...
        public bool HasChunk(Vector2 chunkCoords)
        {
            return (Chunks.Any(chunk => chunk.Position == new Vector2(chunkCoords.X, chunkCoords.Y)));
        }

        // Возвращает блок в позиции...
        public int GetBlockAtPosition(Vector3 globalPosition)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(globalPosition);
            Vector3 localCoords = GlobalToLocalCoords(globalPosition);

            return GetChunk(chunkCoords).GetBlockAtPosition(localCoords);
        }

        // Проверяет есть ли блок в позиции...
        public bool CheckBlock(Vector3 GlobalPos)
        {
            Vector3 localCoords = GlobalToLocalCoords(GlobalPos);
            Vector2 chunkCoords = GlobalToChunkCoords(GlobalPos);
            if (HasChunk(chunkCoords))
            {
                if (GetChunk(chunkCoords).GetBlockAtPosition(localCoords) != 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}