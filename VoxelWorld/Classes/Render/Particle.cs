using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render
{
    public class Particle : Entity
    {
        public TexturedCube Mesh;
        public byte Lifetime = 5; //До 255 (в тиках)
        public byte TextureID;

        public Particle(Vector3 pos, Vector3 vel, Vector3 grav, byte texId)
        {
            Position = pos;
            Velocity = vel;
            Gravity = grav;
            TextureID = texId;
            Mesh = new TexturedCube();
            Mesh.texture = Game.BlocksTexture;
        }
        
        public override void RenderProcess()
        {
           Mesh.Draw(new Vector3(0,0,0), Position, new Vector3(0.1f,0.1f,0.1f), Rotation, new []{(int)TextureID,(int)TextureID,(int)TextureID,(int)TextureID,(int)TextureID,(int)TextureID}, 4);
        }

        public override void PhysicsProcess()
        {
            Velocity += Gravity;
            Position += Velocity;
        }
    }
}