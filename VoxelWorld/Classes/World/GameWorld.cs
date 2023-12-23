using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.World
{
    public class GameWorld
    {
        public int FogEnd = 25;
        
        Player player; // Создание игрока
        public ChunkManager chunkManager;
        private GUIManager guiManager = new GUIManager();
        
        
        public void Ready()
        {
            Game.gameWorld = this;
            player = new Player(this);
            player.Ready();
            chunkManager = new ChunkManager(player);
            chunkManager.Ready();
            guiManager.Ready();
            
            
            
            
            GL.Enable(EnableCap.Fog);
            GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
            GL.Fog(FogParameter.FogStart, 5.0f);
            GL.Fog(FogParameter.FogEnd, FogEnd);
            GL.Fog(FogParameter.FogColor, new float[] { 0.7f, 0.7f, 1.0f, 1.0f });
            GL.Fog(FogParameter.FogDensity, 0.5f);
            GL.Hint(HintTarget.FogHint, HintMode.Nicest);
        }
        public void RenderProcess()
        {
            player.RenderProcess();
            chunkManager.RenderProcess();
            GL.Fog(FogParameter.FogDensity, 0.5f);
            guiManager.RenderProcess();
        }
        public void PhysicsProcess()
        {
            player.PhysicsProcess();
            chunkManager.PhysicsProcess();
            //guiManager.RenderProcess();
        }

        public void OnResizeWindow(EventArgs e)
        {
            player.OnResizeWindow(e);
        }
    }
}
