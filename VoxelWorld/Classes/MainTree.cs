using System;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Render
{
    public class MainTree : Node
    {
        private MainMenu mainMenu = new MainMenu();
        private GameWorld gameWorld = new GameWorld();
        public byte selectedScene = 1;


        public override void Ready()
        {
            Game.mainTree = this;
            mainMenu.Ready();
            gameWorld.Ready();
        }
        
        public override  void RenderProcess()
        {
            if (selectedScene == 0)
            {
                mainMenu.RenderProcess();
            } else if (selectedScene == 1)
            {
                gameWorld.RenderProcess();
            }  
        }

        public override void PhysicsProcess(float delta)
        {
            if (selectedScene == 0)
            {
                mainMenu.PhysicsProcess();
            } else if (selectedScene == 1)
            {
                gameWorld.PhysicsProcess(delta);
            }
        }
        public override  void OnResizeWindow(EventArgs e)
        {
            if (selectedScene == 0)
            {
                mainMenu.OnResizeWindow(e);
            } else if (selectedScene == 1)
            {
                gameWorld.OnResizeWindow(e);;
            }
        }
    }
}