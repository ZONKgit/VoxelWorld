using System;
using System.Collections.Generic;

namespace VoxelWorld.Classes.Engine
{
    public class Game
    {
        Dictionary<string, int[]> Blocks = new Dictionary<string, int[]>();
        public static int BlocksTexture = TextureLoader.LoadTexture("D:/Desktop/C#/VoxelWorld/Res/Textures/terrain.png");
        
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