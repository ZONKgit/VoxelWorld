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
    public class ChunkManager : Node
    {
        public Player player;
        public int renderDistance = 4;
        private List<Chunk> Chunks = new List<Chunk>();

        private Vector2 oldPlayerChunkPos = new Vector2(999, 999);
        public List<Chunk> WaitChunks = new List<Chunk>(); // Чанки которые генерируються в другом потоке
        public Queue<Chunk> ReadyChunks = new Queue<Chunk>(); // Чанки которые уже сгенерировались в другом потоке
        private object chunksLock = new object();
        
        private bool useMultiThreading = false;
        
        public ChunkManager(Player player)
        {
            this.player = player;
        }
        

        public override  void Ready()
        {
            Game.gameWorld.FogEnd = renderDistance * (Chunk.ChunkSizeZ / 2);
        }

        // Отрисовка чанков
        public override  void RenderProcess()
        {
            // Рендерим все чанки
            foreach (var chunk in Chunks)
            {
                if (chunk != null)
                {
                    chunk.RenderProcess();
                }
            }
            
            if (useMultiThreading)
            {
                // Добавляем чанки, которые сгенерировались в другом потоке
                if (ReadyChunks.Count > 0 && ReadyChunks.Peek() != null)
                {
                    ReadyChunks.Peek().Renderer = new ChunkRenderer(ReadyChunks.Peek());
                    ReadyChunks.Peek().Renderer.GenerateChunkMesh(Chunk.ChunkSizeX, Chunk.ChunkSizeY, Chunk.ChunkSizeZ, ReadyChunks.Peek().Position);
                    Chunks.Add(ReadyChunks.Peek());
                    ReadyChunks.Dequeue();
                }
            }
        }


        public override  void PhysicsProcess(float delta)
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
                        if (!Chunks.Any(chunk => chunk.Position == new Vector2(x, z)))
                        {
                            LoadChunk(x, z);
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
                if (Math.Abs(chunkPosition.X - GlobalToChunkCoords(player.Position).X) > renderDistance+1 ||
                    Math.Abs(chunkPosition.Y - GlobalToChunkCoords(player.Position).Y) > renderDistance+1)
                {
                    Chunks.RemoveAt(i);
                    i--; // Уменьшаем i, чтобы не пропустить следующий элемент после удаления
                }
            }


        }

        public void LoadChunk(int x, int z)
        {
            Chunk newChunk = new Chunk(new Vector2(x, z), this);
            if (useMultiThreading)
            {
                lock (chunksLock)
                {
                    // Проверяем, нет ли уже чанка с такой позицией
                    if (!Chunks.Any(chunk => chunk.Position == new Vector2(x * Chunk.ChunkSizeX, z * Chunk.ChunkSizeZ)))
                    {
                        WaitChunks.Add(newChunk);
                        Task.Run(() => newChunk.GenerateChunk());
                    }
                }
            }
            else
            {
                Chunks.Add(newChunk);
                newChunk.Ready();
            }
        }

        // Конвертирует мировые координаты в координаты внутри чанка
        public Vector3 GlobalToLocalCoords(Vector3 globalPosition)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(globalPosition);
            

            return new Vector3((int)Math.Round(globalPosition.X - chunkCoords.X * Chunk.ChunkSizeX), (int)Math.Round(
                globalPosition.Y),(int)Math.Round(globalPosition.Z - chunkCoords.Y * Chunk.ChunkSizeZ));
        }
        

        // Конвертирует мировые координаты в координаты чанка
        public Vector2 GlobalToChunkCoords(Vector3 globalPosition)
        {
            int chunkX = (int)Math.Floor(globalPosition.X / Chunk.ChunkSizeX);
            int chunkZ = (int)Math.Floor(globalPosition.Z / Chunk.ChunkSizeZ);

            return new Vector2(chunkX, chunkZ);
        }



        // Устанавливает блок в позиции...
        public void SetBlock(Vector3 Pos, Block block)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(Pos);
            Vector3 localCoords = GlobalToLocalCoords(Pos);

            // Проверяем, существует ли чанк
            if (HasChunk(chunkCoords))
            {
                Chunk chunk = GetChunk(chunkCoords);
                chunk.SetBlock(localCoords, block);
            }
            else
            {
                Console.WriteLine("Non chunk");
            }
        }
        
        // Уберает блок в позиции...
        public void RemoveBlock(Vector3 Pos)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(Pos);
            Vector3 localCoords = GlobalToLocalCoords(Pos);

            // Проверяем, существует ли чанк
            if (HasChunk(chunkCoords))
            {
                Chunk chunk = GetChunk(chunkCoords);
                chunk.RemoveBlock(localCoords);
            }
            else
            {
                Console.WriteLine("Non chunk");
            }
        }

        // Возвращает чанк по его позиции
        public Chunk GetChunk(Vector2 chunkCoords)
        {
            return Chunks.FirstOrDefault(chunk => chunk.Position == chunkCoords);
        }
        // Есть ли чанк в позиции...
        public bool HasChunk(Vector2 chunkCoords)
        {
            return Chunks.Any(chunk => chunk.Position == chunkCoords);
        }

        // Возвращает блок в позиции...
        public Block GetBlockAtPosition(Vector3 globalPosition)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(globalPosition);
            Vector3 localCoords = GlobalToLocalCoords(globalPosition);
            
            
            if (HasChunk(chunkCoords))
            {
                foreach (var chunk in Chunks)
                {
                    if (chunk.Position == chunkCoords)
                    {
                        break;
                    }
                }
                return GetChunk(chunkCoords).GetBlockAtPosition(localCoords);
            }

            return Blocks.air;
        }
        
        // Проверяет есть ли блок твердый в позиции...
        public bool HasSolidBlock(Vector3 GlobalPos)
        {
            Vector2 chunkCoords = GlobalToChunkCoords(GlobalPos);
            if (HasChunk(chunkCoords))
            {
                Vector3 localCoords = GlobalToLocalCoords(GlobalPos);
                if (GetChunk(chunkCoords).GetBlockAtPosition(localCoords).IsSolid)
                {
                    return true;// Блок есть
                }
                return false;// Блока нету
            }
            return true; // Чанка нету
        }
    }
}