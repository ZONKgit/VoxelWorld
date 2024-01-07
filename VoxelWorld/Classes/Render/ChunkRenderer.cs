using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.World;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render
{
    public class ChunkRenderer
    {
        public const float blockSize = 1;
        public const float UVBlockSize = 256/16/16; // Размер текстуры/Размер блока
        
        Mesh mesh;
        public Chunk chunk;

        List<Vertex> vertices;

        public ChunkRenderer(Chunk chunk)
        {
            this.chunk = chunk;
        }

 


        
        public void GenerateChunkMesh(int ChunkSizeX, int ChunkSizeY, int ChunkSizeZ, Vector2 ChunkPos)
        {
            vertices = new List<Vertex>();
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        if (chunk.ChunkData[x, y, z].Id != 0)
                        {
                            float colorR = 1f;
                            float colorG = 1f;
                            float colorB = 1f;
                            float colorA = 1f;
                            
                            
                            //Front side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y, z-1)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x, y, z-1)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateFrontSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[2]));
                            //Back side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y , z+1)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x, y, z+1)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateBackSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[4]));
                            //Right side
                            if (chunk.GetBlockAtPosition(new Vector3(x-1, y, z)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x-1, y, z)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateRightSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[3]));
                            //Left side
                            if (chunk.GetBlockAtPosition(new Vector3(x+1, y, z)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x+1, y, z)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateLeftSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[1]));
                            //Top side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y+1, z)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x, y+1, z)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateTopSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[0]));
                            //Bottom side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y-1, z)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x, y-1, z)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateBottomSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB-0.1f, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[5]));
                        }
                    }
                }
            }

            if (mesh != null)
            {
                mesh.UpdateMesh(vertices.ToArray());
            }
            else
            {
                mesh = new Mesh(vertices.ToArray());
                mesh.Ready();
            }
            vertices = null;
        }

    private void GenerateFrontSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV) //-Z
    {
        float a = GetAmbientOcclusionFactor(Pos, new Vector3(-1,-1,-1));  // -X; -Y
        float b = GetAmbientOcclusionFactor(Pos, new Vector3(+1,+1,-1));  // +X; +Y
        float c = GetAmbientOcclusionFactor(Pos, new Vector3(+1,-1,-1));  // +X; -Y
        float d = GetAmbientOcclusionFactor(Pos, new Vector3(-1,+1,-1));  // -X: +Y

        float e = GetAmbientOcclusionFactor(Pos, new Vector3(+1, 0, -1)); // +X
        float f = GetAmbientOcclusionFactor(Pos, new Vector3(-1, 0, -1)); // -X
        float g = GetAmbientOcclusionFactor(Pos, new Vector3(0, +1, -1)); // +Y
        float h = GetAmbientOcclusionFactor(Pos, new Vector3(0, -1, -1)); // -Y
        
        if (e != 1) { b = e; c = e; }
        if (f != 1) { a = f; d = f; }
        if (g != 1) { b = g; d = g; }
        if (h != 1) { a = h; c = h; }


        vertices.Add(new Vertex() {X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() {X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f,R = colorR * b, G = colorG * b, B = colorB * b, A = colorA,U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() {X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f,R = colorR * c, G = colorG * c, B = colorB * c, A = colorA,U = UV.Z, V = UV.W });

        vertices.Add(new Vertex() {X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f,R = colorR * b, G = colorG * b, B = colorB * b, A = colorA,U = UV.Z, V = UV.Y});
        vertices.Add(new Vertex() {X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f,R = colorR * a, G = colorG * a, B = colorB * a, A = colorA,U = UV.X, V = UV.W });
        vertices.Add(new Vertex() {X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f,R = colorR * d, G = colorG * d, B = colorB * d, A = colorA,U = UV.X, V = UV.Y });
    }

    private void GenerateBackSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        float a = GetAmbientOcclusionFactor(Pos, new Vector3(-1,-1,+1));  // -X; -Y
        float b = GetAmbientOcclusionFactor(Pos, new Vector3(+1,+1,+1));  // +X; +Y
        float c = GetAmbientOcclusionFactor(Pos, new Vector3(+1,-1,+1));  // +X; -Y
        float d = GetAmbientOcclusionFactor(Pos, new Vector3(-1,+1,+1));  // -X: +Y

        float e = GetAmbientOcclusionFactor(Pos, new Vector3(+1, 0, +1)); // +X
        float f = GetAmbientOcclusionFactor(Pos, new Vector3(-1, 0, +1)); // -X
        float g = GetAmbientOcclusionFactor(Pos, new Vector3(0, +1, +1)); // +Y
        float h = GetAmbientOcclusionFactor(Pos, new Vector3(0, -1, +1)); // -Y
        
        if (e != 1) { b = e; c = e; }
        if (f != 1) { a = f; d = f; }
        if (g != 1) { b = g; d = g; }
        if (h != 1) { a = h; c = h; }
        
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * c, G = colorG * c, B = colorB* c, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * d, G = colorG * d, B = colorB * d, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.W });

        
    }

    private void GenerateRightSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        float a = GetAmbientOcclusionFactor(Pos, new Vector3(-1,-1,-1));  // -Y; -Z
        float b = GetAmbientOcclusionFactor(Pos, new Vector3(-1,+1,+1));  // +Y; +Z
        float c = GetAmbientOcclusionFactor(Pos, new Vector3(-1,-1,+1));  // -Y; +Z
        float d = GetAmbientOcclusionFactor(Pos, new Vector3(-1,+1,-1));  // +Y: -Z

        float e = GetAmbientOcclusionFactor(Pos, new Vector3(-1, +1, 0)); // +Y
        float f = GetAmbientOcclusionFactor(Pos, new Vector3(-1, -1, 0)); // -Y
        float g = GetAmbientOcclusionFactor(Pos, new Vector3(-1, 0, +1)); // +Z
        float h = GetAmbientOcclusionFactor(Pos, new Vector3(-1, 0, -1)); // -Z
        
        if (e != 1) { b = e; d = e; }
        if (f != 1) { a = f; c = f; }
        if (g != 1) { b = g; c = g; }
        if (h != 1) { a = h; d = h; }
        
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z - 0.5f, R = colorR * d, G = colorG * d, B = colorB * d, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * c, G = colorG * c, B = colorB * c, A = colorA, U = UV.X, V = UV.W });

    }

    private void GenerateLeftSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        float a = GetAmbientOcclusionFactor(Pos, new Vector3(+1,-1,-1));  // -Y; -Z
        float b = GetAmbientOcclusionFactor(Pos, new Vector3(+1,+1,+1));  // +Y; +Z
        float c = GetAmbientOcclusionFactor(Pos, new Vector3(+1,-1,+1));  // -Y; +Z
        float d = GetAmbientOcclusionFactor(Pos, new Vector3(+1,+1,-1));  // +Y: -Z

        float e = GetAmbientOcclusionFactor(Pos, new Vector3(+1, +1, 0)); // +Y
        float f = GetAmbientOcclusionFactor(Pos, new Vector3(+1, -1, 0)); // -Y
        float g = GetAmbientOcclusionFactor(Pos, new Vector3(+1, 0, +1)); // +Z
        float h = GetAmbientOcclusionFactor(Pos, new Vector3(+1, 0, -1)); // -Z
        
        if (e != 1) { b = e; d = e; }
        if (f != 1) { a = f; c = f; }
        if (g != 1) { b = g; c = g; }
        if (h != 1) { a = h; d = h; }
        
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z - 0.5f, R = colorR * d, G = colorG * d, B = colorB * d, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.X, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * c, G = colorG * c, B = colorB * c, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.Z, V = UV.W });
    }
    
    
    private void GenerateTopSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV) //+Y
    {
        float a = GetAmbientOcclusionFactor(Pos, new Vector3(-1,1,-1)); //-X; -Z
        float b = GetAmbientOcclusionFactor(Pos, new Vector3(+1,1,+1)); //+X; +Z
        float c = GetAmbientOcclusionFactor(Pos, new Vector3(+1,1,-1)); //+X; -Z
        float d = GetAmbientOcclusionFactor(Pos, new Vector3(-1,1,+1)); //-X; +Z

        float e = GetAmbientOcclusionFactor(Pos, new Vector3(+1, 1, 0)); //+X
        float f = GetAmbientOcclusionFactor(Pos, new Vector3(-1, 1, 0)); //-X
        float g = GetAmbientOcclusionFactor(Pos, new Vector3(0, 1, +1)); //+Z
        float h = GetAmbientOcclusionFactor(Pos, new Vector3(0, 1, -1)); //-Z

        if (e != 1) { b = e; c = e; }
        if (f != 1) { a = f; d = f; }
        if (g != 1) { b = g; d = g; }
        if (h != 1) { a = h; c = h; }

        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR * c, G = colorG * c, B = colorB * c, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR * d, G = colorG * d, B = colorB * d, A = colorA, U = UV.X, V = UV.W });
    }
    
    private void GenerateBottomSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        float a = GetAmbientOcclusionFactor(Pos, new Vector3(-1,-1,-1)); //-X; -Z
        float b = GetAmbientOcclusionFactor(Pos, new Vector3(+1,-1,+1)); //+X; +Z
        float c = GetAmbientOcclusionFactor(Pos, new Vector3(+1,-1,-1)); //+X; -Z
        float d = GetAmbientOcclusionFactor(Pos, new Vector3(-1,-1,+1)); //-X; +Z

        float e = GetAmbientOcclusionFactor(Pos, new Vector3(+1, -1, 0)); //+X
        float f = GetAmbientOcclusionFactor(Pos, new Vector3(-1, -1, 0)); //-X
        float g = GetAmbientOcclusionFactor(Pos, new Vector3(0, -1, +1)); //+Z
        float h = GetAmbientOcclusionFactor(Pos, new Vector3(0, -1, -1)); //-Z

        if (e != 1) { b = e; c = e; }
        if (f != 1) { a = f; d = f; }
        if (g != 1) { b = g; d = g; }
        if (h != 1) { a = h; c = h; }
        
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * c, G = colorG * c, B = colorB * c, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.Z, V = UV.Y });
        

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * b, G = colorG * b, B = colorB * b, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z + 0.5f, R = colorR * d, G = colorG * d, B = colorB * d, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X - 0.5f, Y = Pos.Y - 0.5f, Z = Pos.Z - 0.5f, R = colorR * a, G = colorG * a, B = colorB * a, A = colorA, U = UV.X, V = UV.W });
       
    } 

private float GetAmbientOcclusionFactor(Vector3 pos, Vector3 offset)
{
    Vector3 neighborPos = new Vector3(
        pos.X + offset.X,
        pos.Y + offset.Y,
        pos.Z + offset.Z
        );

    if (Game.gameWorld.chunkManager.GetBlockAtPosition(neighborPos).IsTransparent)
    {
        return 1.0f; 
    }

    return 0.8f;
}








        public void renderProcess()
        {
            if (mesh != null)
            {
                mesh.RenderProcess();
            } 
            
        }

        public void Dispose()
        {
            mesh.Dispose();
            vertices = null;
        }
        
    }
}
