using System;
using System.Collections.Generic;
using OpenTK.Input;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Engine
{
    public class Game
    {
        Dictionary<string, int[]> Blocks = new Dictionary<string, int[]>();
        public static int BlocksTexture = TextureLoader.LoadTexture("Res/textures/terrain.png");
        public static int FontTexture = TextureLoader.LoadTexture("Res/fonts/default.png");
        public static int CloudsTexture = TextureLoader.LoadTexture("Res/textures/environment/clouds.png");
        public static int GUITexture = TextureLoader.LoadTexture("Res/textures/gui/gui.png");

        public static MainTree mainTree;
        public static Player player;
        public static Camera camera;
        public static GameWorld gameWorld;

        public static bool isDrawDebug = false;
        public static bool isDrawDebugHitBox = false;
        
        private static Game instance;

        public static int ScreenWidth = 1920 / 2;
        public static int ScreenHeight = 1080 / 2;
        public static float ScreenAspect = 1.777777777777778f;

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

        public static void PhysicsProcess()
        {
            if (Input.IsJustKeyPressed(Input.KeyDebugDraw)) isDrawDebug = !isDrawDebug;
        }
    }
}