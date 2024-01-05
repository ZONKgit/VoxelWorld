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
                            //&& chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup == chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup

                            
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
                            if (chunk.GetBlockAtPosition(new Vector3(x, y-1, z)).IsTransparent && chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup != chunk.GetBlockAtPosition(new Vector3(x, y, z)).DrawGroup) GenerateBottomSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB-0.1f, colorA, TextureHelper.IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[5]));
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

    private void GenerateFrontSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
    }

    private void GenerateBackSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });

        
    }

    private void GenerateRightSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });

    }

    private void GenerateLeftSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
    }




    private void GenerateTopSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + 0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    }





    private void GenerateBottomSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        

        vertices.Add(new Vertex() { X = Pos.X + 0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + 0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + -0.5f, Y = Pos.Y + -0.5f, Z = Pos.Z + -0.5f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
       
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
