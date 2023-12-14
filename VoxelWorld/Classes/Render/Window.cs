using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.Render
{
    public class Window : GameWindow
    {
        public static int WindowWidth = 1920 / 2;
        public static int WindowHeight = 1080 / 2;

        public Window() : base(WindowWidth, WindowHeight, GraphicsMode.Default, "Voxel World")
        {
            VSync = VSyncMode.On;
        }


        Camera camera = new Camera(); // Создание камеры
        //Mesh mesh = new Mesh(new float[] {
        //        0,0,1,
        //        1,0,1,
        //        1,1,1,

        //        1,0,0,
        //        0,1,0,
        //        1,1,0,
        //    }, new float[] {
        //        1f,0f,0f,
        //        0f,1f,0f,
        //        0f,0f,1f,

        //        0f,1f,1f,
        //        1f,0f,1f,
        //        1f,1f,0f,
        //    });
        ColorRect crosshair = new ColorRect(0.0003f);
        ChunkRenderer chunk;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            camera.ready();
            //mesh.ready();
            chunk = new ChunkRenderer();
        }



        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            camera.physicsProcess();
            Input.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            camera.renderProcess();
            //mesh.renderProcess();
            chunk.renderProcess();
            crosshair.renderProcess();
            


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

            camera.onWindowResize(e);
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

