using System;
using ImGuiNET;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.Render
{
    public class GUIManager
    {
        ColorRect crosshair = new ColorRect(new Vector2(0, 0), new Color4(0f, 0f, 0f, 1f), 0.01f);
        TextureRect TextureInventoryBar = new TextureRect(new Vector2(0, 0), new Vector4(0,0,0.5f,0.5f), 0.8f);
        Text2D text = new Text2D(new Vector2(-49f, 25.5f), "Camera position", 0.03f);
        Text2D textRotation = new Text2D(new Vector2(-49f , 24.3f), "Camera rotation", 0.03f);
        Text2D textIsOnFloor = new Text2D(new Vector2(-49f, 23.1f), "IsOnFloor", 0.03f);
        Text2D textBlockId = new Text2D(new Vector2(-49f, 21.8f), "textBlockId", 0.03f);

        public void Ready()
        {
            text.Ready();
            textRotation.Ready();
            textIsOnFloor.Ready();
            textBlockId.Ready();
            TextureInventoryBar.Texture = Game.GUITexture;
        }
        
        public void RenderProcess()
        {
            GL.LoadIdentity();
            GL.Ortho(-1,1,-1,1,0,1);
            GL.Disable(EnableCap.DepthTest);
            
            // Рендер тут
            text.RenderProcess();
            textRotation.RenderProcess();
            textIsOnFloor.RenderProcess();
            textBlockId.RenderProcess();
            
            crosshair.RenderProcess();
            TextureInventoryBar.RenderProcess();
            
            // GL.Begin(BeginMode.Triangles);
            // GL.Color4(new Color4(0f,1f,1f,1f));GL.Vertex3(0,1,-0.1f);
            // GL.Color3(0,1,0);GL.Vertex3(-1,-1,-0.1f);
            // GL.Color3(0,0,1);GL.Vertex3(1,-1,-0.1f);
            // GL.End();
            
            

            
            // Вывод позиции
            text.Text = "Pos: "+new Vector3(
                (int)Game.camera.position.X,
                (int)Game.camera.position.Y,
                (int)Game.camera.position.Z
            ).ToString()+"Chunk: "+Game.player.world.chunkManager.GlobalToChunkCoords(Game.camera.position).ToString();
            // Вывод поворота
            textRotation.Text = "Rot: "+Game.camera.rotation.ToString();
            // Вывод OnFloor
            textIsOnFloor.Text = "On floor: " + Game.player.hitbox.IsOnFloor.ToString();
            // Вывод блока в камере
            textBlockId.Text = "Block id in camera position: " + Game.gameWorld.chunkManager.GetBlockAtPosition(Game.camera.position).ToString();
            
            GL.Enable(EnableCap.DepthTest);
        }

        
        public void PhysicsProcess()
        {
            
        }
    }
}