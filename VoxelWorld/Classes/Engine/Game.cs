using System;
using System.Collections.Generic;
using System.Threading;
using OpenTK.Input;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.World;
using System.Timers;
using Timer = System.Timers.Timer;

namespace VoxelWorld.Classes.Engine
{
    public class Game
    {
        public static int BlocksTexture = TextureLoader.LoadTexture("Res/textures/terrain.png");
        public static int FontTexture = TextureLoader.LoadTexture("Res/fonts/default.png");
        public static int CloudsTexture = TextureLoader.LoadTexture("Res/textures/environment/clouds.png");
        public static int GUITexture = TextureLoader.LoadTexture("Res/textures/gui/gui.png");

        public static Random random = new Random();
        public static Window window;
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

        public static float delta = 0; // Время прошедшее с момента отрисовки прошлого до текущего кадра
        public static int fps = 0; // Текущая частота отрисовки кадров
        public static int time = 0; // Прошедшее время с момента запуска игры в ms
        public static int tick = 0;// Прошедшее кол-во тиков с момента запуска игры
        
        // Приватный конструктор, чтобы предотвратить создание экземпляров извне
        private Game()
        {
            
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
            time++;
            if (Input.IsJustKeyPressed(Input.KeyDebugPanel)) isDrawDebug = !isDrawDebug;
            if (Input.IsJustKeyPressed(Input.KeyDebugHitBox)) isDrawDebugHitBox = !isDrawDebugHitBox;
        }
    }
}