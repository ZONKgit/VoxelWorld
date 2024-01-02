using System;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Render
{
    public class MainTree
    {
        private MainMenu mainMenu = new MainMenu();
        private GameWorld gameWorld = new GameWorld();
        public byte selectedScene = 0;


        public void Ready()
        {
            Game.mainTree = this;
            mainMenu.Ready();
            gameWorld.Ready();
        }
        
        public void RenderProcess()
        {
            if (selectedScene == 0)
            {
                mainMenu.RenderProcess();
            } else if (selectedScene == 1)
            {
                gameWorld.RenderProcess();
            }  
        }

        public void PhysicsProcess(float delta)
        {
            if (selectedScene == 0)
            {
                mainMenu.PhysicsProcess();
            } else if (selectedScene == 1)
            {
                gameWorld.PhysicsProcess(delta);
            }
        }
        public void OnResizeWindow(EventArgs e)
        {
            if (selectedScene == 0)
            {
                mainMenu.PhysicsProcess();
            } else if (selectedScene == 1)
            {
                gameWorld.OnResizeWindow(e);;
            }
        }
    }
}