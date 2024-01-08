using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using Button = VoxelWorld.Classes.Render.GUIClasses.Button;

namespace VoxelWorld.Classes
{
    public class MainMenu : Node
    {
        Button StartButton = new Button(new Vector2(1f, 1f));
        
        
        public void Ready()
        {
            StartButton.Texture.Texture = Game.GUITexture;
        }

        public override void RenderProcess()
        {
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0,Game.ScreenWidth,Game.ScreenHeight,0,-1,1);
    
            StartButton.RenderProcess();
    
            GL.PopMatrix();
        }


        public void PhysicsProcess()
        {
            StartButton.PhysicsProcess();
        }
    }
}