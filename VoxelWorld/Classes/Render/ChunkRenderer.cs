using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.World;

namespace VoxelWorld.Classes.Render
{
    public class ChunkRenderer
    {
        public const float blockSize = 1;
        public const float UVBlockSize = 256/16/16; // Размер текстуры/Размер блока
        
        Mesh mesh;
        public Chunk chunk;

        List<Vertex> vertices = new List<Vertex>();

        public ChunkRenderer(Chunk chunk)
        {
            this.chunk = chunk;
        }

        public Vector4 IDToUVCoords(int id)
        {
            int atlasSize = 256; // Размер атласа
            int textureSize = 16; // Размер одной текстуры
            int texturesPerRow = atlasSize / textureSize; // Количество текстур в одной строке атласа

            int atlasX = id % texturesPerRow; // Координата X в атласе
            int atlasY = id / texturesPerRow; // Координата Y в атласе

            float uMin = (float)atlasX * (float)textureSize / (float)atlasSize;
            float uMax = (float)atlasY * (float)textureSize / (float)atlasSize;
            float vMin = (float)(atlasX+1) * (float)textureSize / (float)atlasSize;
            float vMax = (float)(atlasY+1) * (float)textureSize / (float)atlasSize;


            return new Vector4(uMin, uMax, vMin, vMax); // 0,0,0.0625f,0.0625f 
        }


        
        public void GenerateChunkMesh(int ChunkSizeX, int ChunkSizeY, int ChunkSizeZ, Vector2 ChunkPos)
        {
            for (int x = 0; x < ChunkSizeX; x++)
            {
                for (int y = 0; y < ChunkSizeY; y++)
                {
                    for (int z = 0; z < ChunkSizeZ; z++)
                    {
                        if (chunk.ChunkData[x, y, z] == 1)
                        {
                            float colorR = 1f;
                            float colorG = 1f;
                            float colorB = 1f;
                            float colorA = 1f;

                            //Front side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y, z-1)) == 0) GenerateFrontSide(new Vector3(ChunkPos.X + x, y, ChunkPos.Y + z), colorR, colorG, colorB, colorA, IDToUVCoords(1));
                            //Back side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y , z+1)) == 0) GenerateBackSide(new Vector3(ChunkPos.X + x, y, ChunkPos.Y + z), colorR-0.5f, colorG-0.5f, colorB-0.1f, colorA, IDToUVCoords(1));
                            //Right side
                            if (chunk.GetBlockAtPosition(new Vector3(x-1, y, z)) == 0) GenerateRightSide(new Vector3(ChunkPos.X + x, y, ChunkPos.Y + z), colorR-0.5f, colorG-0.5f, colorB-0.1f, colorA, IDToUVCoords(1));
                            //Left side
                            if (chunk.GetBlockAtPosition(new Vector3(x+1, y, z)) == 0) GenerateLeftSide(new Vector3(ChunkPos.X + x, y, ChunkPos.Y + z), colorR, colorG, colorB, colorA, IDToUVCoords(1));
                            //Top side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y+1, z)) == 0) GenerateTopSide(new Vector3(ChunkPos.X + x, y, ChunkPos.Y + z), colorR, colorG, colorB, colorA, IDToUVCoords(1));
                            //Bottom side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y-1, z)) == 0) GenerateBottomSide(new Vector3(ChunkPos.X + x, y, ChunkPos.Y + z), colorR-0.5f, colorG-0.5f, colorB-0.1f, colorA, IDToUVCoords(1));
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
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });

    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
}

private void GenerateBackSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
{
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });

    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });

    
}

private void GenerateRightSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
{
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });

    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
}

private void GenerateLeftSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
{
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });

    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
    vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
}




    private void GenerateTopSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });

        vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 1f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
    }





    private void GenerateBottomSide(Vector3 Pos, float colorR, float colorG, float colorB, float colorA, Vector4 UV)
    {
        vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.W });
        vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        

        vertices.Add(new Vertex() { X = Pos.X + 1f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.Z, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 1f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.Y });
        vertices.Add(new Vertex() { X = Pos.X + 0f, Y = Pos.Y + 0f, Z = Pos.Z + 0f, R = colorR, G = colorG, B = colorB, A = colorA, U = UV.X, V = UV.W });
       
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
