using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Graphics;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes.World
{
    public class GameWorld: Node
    {
        public int FogEnd = 25;
        public List<Particle> Particles = new List<Particle>();
        
        Player player; // Создание игрока
        public ChunkManager chunkManager;
        private GUIManager guiManager = new GUIManager();
        private Clouds clouds = new Clouds();
        
        
        public override  void Ready()
        {
            Game.gameWorld = this;
            player = new Player(this);
            player.Ready();
            chunkManager = new ChunkManager(player);
            chunkManager.Ready();
            clouds.Ready();
            guiManager.Ready();

            // Настройки тумана
            // GL.Enable(EnableCap.Fog);
            // GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
            // GL.Fog(FogParameter.FogStart, 10);
            // GL.Fog(FogParameter.FogEnd, FogEnd);
            // GL.Fog(FogParameter.FogColor, new float[] { 0.7f, 0.7f, 1.0f, 1.0f });
            // GL.Fog(FogParameter.FogDensity, 0.1f);
            // GL.Hint(HintTarget.FogHint, HintMode.Nicest);
        }
        
        public void AddParticle(Vector3 pos, Vector3 vel, Vector3 grav, byte texId) {
            if (Particles.Count < 4000) Particles.Add(new Particle(pos, vel, grav, texId));
        }
        public void RemoveParticle(Particle particle) {
            Particles.Remove(particle);
        }
        
        public override  void RenderProcess()
        {
            player.RenderProcess();
            clouds.RenderProcess();

            // BoxEdges.DrawBoxEdges(new Vector3(0.5f,0.5f,0.5f), new Vector3(NetWork.networkData[0], NetWork.networkData[1], NetWork.networkData[2]), new Color4(0f,1f,0f,1f), 10f);
            
            foreach (var particle in Particles)
            {
                particle.RenderProcess();   
            }
            chunkManager.RenderProcess();
            
            
            guiManager.RenderProcess();
        }   
        public override void PhysicsProcess(float delta)
        {
            Game.PhysicsProcess();
            player.PhysicsProcess(delta);
            chunkManager.PhysicsProcess(delta);
            clouds.PhysicsProcess(delta);
            
            List<Particle> particlesCopy = new List<Particle>(Particles);
            foreach (var particle in particlesCopy)
            {
                particle.PhysicsProcess(delta);   
            }
        }

        public override  void OnResizeWindow(EventArgs e)
        {
            player.OnResizeWindow(e);
        }
    }
}
