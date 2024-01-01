using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.World;
using Vector3 = OpenTK.Vector3;

namespace VoxelWorld.Classes.Render
{
    public class Clouds
    {
        public Vector2 Position = new Vector2(0, 0); // Позиция по XZ
        public Vector2 Offset = new Vector2(0,0); // Смещение UV по XZ
        public Vector2 VelocityOffset = new Vector2(0,0); // Смещение UV по XZ
        private Vector2 Velocity = new Vector2(0.00001f, 0.00001f); // Скорость смещения облаков
        private int Height = 128; // Высота
        private float UVScale = 0.1f;
        
        public void Ready()
        {
            
        }
        public void RenderProcess()
        {
            GL.Color4(new Color4(1f, 1f, 1f, 1f));
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, Game.CloudsTexture);
            GL.Begin(BeginMode.TriangleStrip);
            // Нижняя часть облаков
            GL.TexCoord2(0*UVScale+Offset.X,0*UVScale+Offset.Y); GL.Vertex3(Position.X-100, Height, Position.Y+100);
            GL.TexCoord2(1*UVScale+Offset.X, 0*UVScale+Offset.Y); GL.Vertex3(Position.X-100, Height, Position.Y-100);
            GL.TexCoord2(0*UVScale+Offset.X, 1*UVScale+Offset.Y); GL.Vertex3(Position.X+100, Height, Position.Y+100);
            GL.TexCoord2(1*UVScale+Offset.X, 1*UVScale+Offset.Y);GL.Vertex3(Position.X+100, Height, Position.Y-100);
            // Верхняя часть облаков
            GL.TexCoord2(1*UVScale+Offset.X, 0*UVScale+Offset.Y); GL.Vertex3(Position.X-100, Height, Position.Y-100);
            GL.TexCoord2(0*UVScale+Offset.X,0*UVScale+Offset.Y);GL.Vertex3(Position.X-100, Height, Position.Y+100);
            GL.TexCoord2(1*UVScale+Offset.X, 1*UVScale+Offset.Y);GL.Vertex3(Position.X+100, Height, Position.Y-100);
            GL.TexCoord2(0*UVScale+Offset.X, 1*UVScale+Offset.Y);GL.Vertex3(Position.X+100, Height, Position.Y+100);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        public void PhysicsProcess(float delta)
        {
            VelocityOffset.X += Velocity.X;
            VelocityOffset.Y += Velocity.Y;
            Offset.X = VelocityOffset.X - Game.player.Position.Z/256;//Деление на размер текстуры облаков
            Offset.Y = VelocityOffset.Y + Game.player.Position.X/256;
            Position = new Vector2(Game.player.Position.X, Game.player.Position.Z);
        }
    }
}