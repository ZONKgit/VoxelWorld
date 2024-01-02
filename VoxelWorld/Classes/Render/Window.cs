using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Render
{
    public class Window : GameWindow
    {
        public int DebugDraw = 0;
        public static int WindowWidth = 1920 / 2;
        public static int WindowHeight = 1080 / 2;

        public Window() : base(WindowWidth, WindowHeight, GraphicsMode.Default, "Voxel World")
        {
            VSync = VSyncMode.On;
            //CursorVisible = false;
        }

        MainTree mainTree = new MainTree();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Ширина линий
            GL.LineWidth(2.0f);
            // Backface Culling
            GL.Enable(EnableCap.CullFace);  // Включить отсечение граней
            GL.CullFace(CullFaceMode.Back); // Указать, что нужно отсекать задние грани
            // Alpha канал
            GL.Enable(EnableCap.Blend);// Смешивание цветов
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha); // Настройка смешивания
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Greater, 0.0f);
            

            mainTree.Ready();
            //ToggleFullscreen(); //Фуллскрин
        }



        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            float delta = (float)e.Time;
            
            mainTree.PhysicsProcess(delta);

            // Смена режима отрисовки
            if (Input.IsJustKeyPressed(Input.KeyDebugWireframe))
            {
                if (DebugDraw == 0)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    DebugDraw = 1;
                }
                else
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    DebugDraw = 0;
                }
 
            }

            Input.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

           

            mainTree.RenderProcess();

            SwapBuffers();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            NetWork.CloseConnection();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            WindowWidth = Width;
            WindowHeight = Height;
            
            
            Game.ScreenWidth = WindowWidth;
            Game.ScreenHeight = WindowHeight;
            Game.ScreenAspect = WindowWidth / WindowHeight;

            GL.Viewport(0, 0, Width, Height);

            mainTree.OnResizeWindow(e);
        }
        
        private void ToggleFullscreen()
        {
            WindowState = WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
            WindowBorder = WindowState == WindowState.Fullscreen ? WindowBorder.Hidden : WindowBorder.Resizable;
        }

        static void Main(string[] args)
        {
            using (var game = new Window())
            {
                game.Run(60.0);
            }
        }
    }
}

