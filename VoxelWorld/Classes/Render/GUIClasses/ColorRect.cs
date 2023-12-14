using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    public class ColorRect : GUI
    {
        float size = 0.001f;

        public ColorRect(float size = 0.001f)
        {
            this.size = size;
            vertices = new float[]
            {
            -size, -size, -0.1f,
            -size, size, -0.1f,
            size, size, -0.1f,
            size, -size, -0.1f
            };
        }
    }
}
