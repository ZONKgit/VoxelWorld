using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    public static class TextureHelper
    {
        public static Vector4 IDToUVCoords(int id)
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
    }
}