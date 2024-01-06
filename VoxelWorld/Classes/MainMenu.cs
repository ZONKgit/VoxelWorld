using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using Button = VoxelWorld.Classes.Render.GUIClasses.Button;

namespace VoxelWorld.Classes
{
    public class MainMenu : Node
    {
        Button StartButton = new Button(new Vector2(0.625f, 0.125f));
        
        
        public void Ready()
        {
            Game.mainTree.selectedScene = 1; // Убрать эту строку кода!
            StartButton.Texture.Texture = Game.GUITexture;
        }

        public void RenderProcess()
        {
            GL.LoadIdentity();
            GL.Ortho(-1,1,-1,1,0,1);
            GL.Disable(EnableCap.DepthTest);
            
            StartButton.RenderProcess();
            
            GL.Enable(EnableCap.DepthTest);
        }

        public void PhysicsProcess()
        {
            StartButton.PhysicsProcess();
        }
    }
}