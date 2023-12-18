using System;
using OpenTK;
using System.Collections.Generic;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.World
{
    public class GameWorld
    {
        Player player; // Создание игрока
        public ChunkManager chunkManager;

        public void Ready()
        {
            player = new Player(this);
            player.Ready();
            chunkManager = new ChunkManager(player);
            chunkManager.Ready();
        }
        public void RenderProcess()
        {
            player.RenderProcess();
            chunkManager.RenderProcess();
        }
        public void PhysicsProcess()
        {
            player.PhysicsProcess();
            chunkManager.PhysicsProcess();
        }

        public void OnResizeWindow(EventArgs e)
        {
            player.OnResizeWindow(e);
        }
    }
}
