﻿using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render
{
    public class Window : GameWindow
    {
        public static ushort WindowWidth = 1920 / 2;
        public static ushort WindowHeight = 1080 / 2;

        public byte SelectedDebugDraw = 0;
        
        private const float PhysicsUpdateInterval = 1.0f / 20.0f; // 20 раз в секунду
        private float timeSinceLastPhysicsUpdate = 0.0f;
        
        private int frameCount = 0;
        private double elapsedTime = 0.0;
        private double fps = 0.0;

        
        public Window() : base(WindowWidth, WindowHeight, GraphicsMode.Default, "Voxel World")
        {
            VSync = VSyncMode.Off;
        }

        MainTree mainTree = new MainTree(); 
        
        public enum DebugDraw { Normal, Wireframe }
        
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


            Game.window = this;
            mainTree.Ready();
        }



        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Game.delta = (float)e.Time;
            
            
            timeSinceLastPhysicsUpdate += (float)e.Time;
            if (timeSinceLastPhysicsUpdate >= PhysicsUpdateInterval)
            {
                Game.tick += 1;
                timeSinceLastPhysicsUpdate -= PhysicsUpdateInterval;
            }
            if (!CursorVisible)
            {
                mainTree.PhysicsProcess((float)e.Time);
            }
            
            // Телепортация курсора в центре окна, если он не отображаеться
            if (!CursorVisible) {
                // Рассчитываем центр окна
                float centerX = X + WindowWidth / 2.0f;
                float centerY = Y + WindowHeight / 2.0f;

                // Перемещаем курсор в центр окна
                Mouse.SetPosition(centerX, centerY);
            }
            
            // Смена режима отрисовки
            if (Input.IsJustKeyPressed(Input.KeyDebugWireframe)) {
                if (SelectedDebugDraw == 1) {
                    SetDebugDraw(DebugDraw.Normal);
                }else {
                    SetDebugDraw(DebugDraw.Wireframe);
                }
            }
            // Переключение полноэкранного режима
            if (Input.IsJustKeyPressed(Input.KeyFullscreenMode)) { ToggleFullscreen(); }
            if (Input.IsJustKeyPressed(Input.KeyPause)) {CursorVisible = !CursorVisible;}
            
            
            Input.Update();
            
            
            // Обновление счетчика кадров и расчет FPS
            frameCount++;
            elapsedTime += e.Time;

            if (elapsedTime >= 1.0) // Пересчитываем FPS каждую секунду
            {
                fps = frameCount / elapsedTime;
                Game.fps = (int)fps;

                frameCount = 0;
                elapsedTime = 0.0;
            }
        }

        public void SetDebugDraw(DebugDraw mode)
        {
            switch (mode)
            {
                case DebugDraw.Normal:
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    SelectedDebugDraw = 0;
                    break;
                case DebugDraw.Wireframe:
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    SelectedDebugDraw = 1;
                    break;
            }
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

            WindowWidth = (ushort)Width;
            WindowHeight = (ushort)Height;
            
            
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
        
    }
}

