using System;
using ImGuiNET;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.Render
{
    public class GUIManager : Node
    {
        ColorRect crosshair = new ColorRect(new Vector2(0, 0), new Color4(0f, 0f, 0f, 1f), 0.01f);
        TextureRect TextureInventoryBar = new TextureRect(new Vector2(0, -0.74f), new Vector4(0,0,0.7109375f,0.0859375f), new Vector2(0.7109375f*0.7f, 0.0859375f*0.7f));
        TextureRect TextureInventoryBarSelected = new TextureRect(new Vector2(0, -0.745f), new Vector4(0,0.09375f,0.09375f,0.0859375f+0.09375f), new Vector2(0.09375f*0.7f, 0.09375f*0.7f));
        Text2D nullText = new Text2D(new Vector2(0f, 0), "", 1f);
        Text2D text = new Text2D(new Vector2(-45f, 25.5f), "Camera position", 0.03f);
        Text2D textRotation = new Text2D(new Vector2(-45f , 24.3f), "Camera rotation", 0.03f);
        Text2D textIsOnFloor = new Text2D(new Vector2(-45f, 23.1f), "IsOnFloor", 0.03f);
        Text2D textBlockId = new Text2D(new Vector2(-45f, 21.8f), "textBlockId", 0.03f);
        Text2D textParticelsCount = new Text2D(new Vector2(-45f, 19.5f), "Particels:", 0.03f);
        Text2D textFPS = new Text2D(new Vector2(-45f, 18.2f), "FPS:", 0.03f);
        

        private TexturedCube[] blockSlots = new[]
        {
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube(),
            new TexturedCube()
        };

        public void Ready()
        {
            nullText.Ready();
            text.Ready();
            textRotation.Ready();
            textIsOnFloor.Ready();
            textBlockId.Ready();
            textParticelsCount.Ready();
            textFPS.Ready();
            TextureInventoryBar.Texture = Game.GUITexture;
            TextureInventoryBarSelected.Texture = Game.GUITexture;

            for (int i = 0; i < blockSlots.Length; i++)
            {
                blockSlots[i].texture = Game.BlocksTexture;
            }
        }
        public void RenderProcess()
        {
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(-1,1,-1,1,0,1);
            GL.Disable(EnableCap.DepthTest);
            
            // Рендер тут
            nullText.RenderProcess();
            if (Game.isDrawDebug)
            {
                text.RenderProcess();
                textRotation.RenderProcess();
                textIsOnFloor.RenderProcess();
                textBlockId.RenderProcess();
                textParticelsCount.RenderProcess();
                textFPS.RenderProcess();
            }
            else
            {
                crosshair.RenderProcess();
            }
            TextureInventoryBar.RenderProcess();
            TextureInventoryBarSelected.RenderProcess();
            TextureInventoryBarSelected.Position.X = (Game.player.selectedSlot * 0.77f / 7)-(4*0.77f/7);
            
            GL.PushMatrix();
            GL.LoadIdentity();
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 150f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            
            for (int i = 0; i < 6; i++)
            {
                blockSlots[i].Draw(
                    new Vector3(0, 0, 0),
                    new Vector3((i * 3.4f)-(4*3.4f), -23.1f, -25),
                    new Vector3(0.5f,0.5f,0.5f),
                    new Vector3(5,45,15),
                    Game.player.Slots[i].TextureFaces,
                    1);
            }
            
            GL.PopMatrix();




            // Вывод позиции
            text.Text = "Pos: "+new Vector3(
                (int)Game.camera.Position.X,
                (int)Game.camera.Position.Y,
                (int)Game.camera.Position.Z
            )+"Chunk: "+Game.gameWorld.chunkManager.GlobalToChunkCoords(Game.camera.Position)+" Local: "+Game.gameWorld.chunkManager.GlobalToLocalCoords(Game.camera.Position);
            // Вывод поворота
            textRotation.Text = "Rot: "+Game.camera.Rotation.ToString();
            // Вывод OnFloor
            textIsOnFloor.Text = "On floor: " + Game.player.hitbox.IsOnFloor.ToString();
            // Вывод блока в камере
            textBlockId.Text = "Block id in camera position: " + Game.gameWorld.chunkManager.GetBlockAtPosition(Game.camera.Position).Id;
            // Вывод кол-ва частиц в мире
            textParticelsCount.Text = "Particels: " + Game.gameWorld.Particles.Count + " / 4000";
            // Вывод FPS
            textFPS.Text = "FPS: " + Game.fps;
            
            GL.Enable(EnableCap.DepthTest);
            GL.PopMatrix();
        }

        
        public void PhysicsProcess()
        {
            
        }
    }
}