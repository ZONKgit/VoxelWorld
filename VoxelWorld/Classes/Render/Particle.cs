using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render
{
    public class Particle : Entity
    {
        public TexturedCube Mesh;
        public int TickStart;
        public byte LifeTime = 100; //До 255 (в тиках)
        public byte TextureID;

        public Particle(Vector3 pos, Vector3 vel, Vector3 grav, byte texId)
        {
            TickStart = Game.tick;
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

        public override void PhysicsProcess(float delta)
        {
            if ((Game.tick - TickStart)*delta >= LifeTime)
            {
                Game.gameWorld.RemoveParticle(this);
                Console.WriteLine("Destroy");
            }
            Velocity += Gravity*delta;
            MoveAndCollide();
        }
    }
}