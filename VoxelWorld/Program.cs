using System;
using VoxelWorld.Classes.Render;

namespace VoxelWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Window())
            {
                game.Run(60.0);
            }
        }
    }
}
