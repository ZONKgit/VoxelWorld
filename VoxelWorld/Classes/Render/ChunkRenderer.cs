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

        List<Vertex> vertices;

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
                            if (chunk.GetBlockAtPosition(new Vector3(x, y, z-1)).isTransparent) GenerateFrontSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[2]));
                            //Back side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y , z+1)).isTransparent) GenerateBackSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[4]));
                            //Right side
                            if (chunk.GetBlockAtPosition(new Vector3(x-1, y, z)).isTransparent) GenerateRightSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[3]));
                            //Left side
                            if (chunk.GetBlockAtPosition(new Vector3(x+1, y, z)).isTransparent) GenerateLeftSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[1]));
                            //Top side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y+1, z)).isTransparent) GenerateTopSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB, colorA, IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[0]));
                            //Bottom side
                            if (chunk.GetBlockAtPosition(new Vector3(x, y-1, z)).isTransparent) GenerateBottomSide(new Vector3(ChunkPos.X * Chunk.ChunkSizeX + x, y, ChunkPos.Y* Chunk.ChunkSizeZ + z), colorR, colorG, colorB-0.1f, colorA, IDToUVCoords(chunk.ChunkData[x, y, z].TextureFaces[5]));
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
