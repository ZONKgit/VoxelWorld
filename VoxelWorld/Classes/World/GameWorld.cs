using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.World
{
    public class GameWorld: Node
    {
        public int FogEnd = 25;
        
        Player player; // Создание игрока
        public ChunkManager chunkManager;
        private GUIManager guiManager = new GUIManager();
        private Clouds clouds = new Clouds();
        
        
        public override  void Ready()
        {
            Game.gameWorld = this;
            player = new Player(this);
            player.Ready();
            chunkManager = new ChunkManager(player);
            chunkManager.Ready();
            clouds.Ready();
            guiManager.Ready();
            
            
            // Настройки тумана
            // GL.Enable(EnableCap.Fog);
            // GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
            // GL.Fog(FogParameter.FogStart, 10);
            // GL.Fog(FogParameter.FogEnd, FogEnd);
            // GL.Fog(FogParameter.FogColor, new float[] { 0.7f, 0.7f, 1.0f, 1.0f });
            // GL.Fog(FogParameter.FogDensity, 0.1f);
            // GL.Hint(HintTarget.FogHint, HintMode.Nicest);
        }
        public override  void RenderProcess()
        {
            player.RenderProcess();
            
            clouds.RenderProcess();
            chunkManager.RenderProcess();
            
            
            guiManager.RenderProcess();
            
        }   
        public override void PhysicsProcess()
        {
            Game.PhysicsProcess();
            player.PhysicsProcess();
            chunkManager.PhysicsProcess();
            clouds.PhysicsProcess();
        }

        public override  void OnResizeWindow(EventArgs e)
        {
            player.OnResizeWindow(e);
        }
    }
}
