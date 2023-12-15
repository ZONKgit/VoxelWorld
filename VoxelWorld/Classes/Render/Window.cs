using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render.GUIClasses;
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
        }

        GameWorld world = new GameWorld();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Ширина линий
            GL.LineWidth(2.0f);
            // Backface Culling
            GL.Enable(EnableCap.CullFace);  // Включить отсечение граней
            GL.CullFace(CullFaceMode.Back); // Указать, что нужно отсекать задние грани
            // Alpha канал
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            world.Ready();
        }



        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            world.PhysicsProcess();

            // Смена режима отрисовки
            if (Input.IsKeyJustPressed(Input.KeyDebugWireframe))
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

           

            world.RenderProcess();

            SwapBuffers();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            WindowWidth = Width;
            WindowHeight = Height;

            GL.Viewport(0, 0, Width, Height);

            world.OnResizeWindow(e);
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

