using System;
using System.Collections.Generic;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Engine
{
    public class Game
    {
        Dictionary<string, int[]> Blocks = new Dictionary<string, int[]>();
        public static int BlocksTexture = TextureLoader.LoadTexture("Res/textures/terrain.png");
        public static int FontTexture = TextureLoader.LoadTexture("Res/fonts/default.png");
        public static int CloudsTexture = TextureLoader.LoadTexture("Res/textures/environment/clouds.png");
        
        public static Player player;
        public static Camera camera;
        public static GameWorld gameWorld;
        
        private static Game instance;

        // Приватный конструктор, чтобы предотвратить создание экземпляров извне
        private Game()
        {
            // Инициализация экземпляра
            
            // Добавляем блоки и их данные
            
            //                            Id  Texture ID (UV)
            Blocks.Add("Grass", new int[] { 0, 0 });
            Blocks.Add("Stone", new int[] { 1, 1 });
            
        }

        // Метод для получения единственного экземпляра
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }

        // Другие члены класса

        
    }
}