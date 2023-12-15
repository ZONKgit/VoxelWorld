using System;
using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    class Perlin2D
    {
        public float GetNoiseValue(Vector2 Pos)
        {
            // Инициализация генератора случайных чисел
            Random random = new Random();

            // Генерация случайного числа от 0 до 1
            double randomNumber = random.NextDouble();
            return (float)randomNumber;
        }
    }
}
