using System;
using OpenTK.Graphics;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    public class ColorRect : GUI
    {
        float size = 0.001f;

        public ColorRect(Color4 Color, float size = 0.001f)
        {
            this.Color = Color;

            this.size = size;
            vertices = new float[]
            {
            size, -size, -0.1f,
            size, size, -0.1f,
            -size, size, -0.1f,
            -size, -size, -0.1f
            };
        }
    }
}
