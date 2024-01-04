using System;
using System.Collections.Generic;
using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    public static class Animation
    {
        public static Vector3 SinusoidalMotion(Vector3 startPosition, float animationSpeed, float animationAmplitude, float elapsedTime)
        {
            float yOffset = (float)Math.Sin(animationSpeed * elapsedTime) * animationAmplitude;
            return startPosition + new Vector3(0, yOffset, 0);
        }

        public static Vector3 LinearInterpolation(Vector3 start, Vector3 end, float t)
        {
            return start + t * (end - start);
        }

        public static Vector3 KeyframeAnimation(List<Vector3> keyframes, float elapsedTime)
        {
            if (keyframes.Count == 0)
                throw new ArgumentException("Keyframes list must not be empty.");

            int frameCount = keyframes.Count;
            float totalAnimationTime = frameCount - 1; // Total time for the entire animation

            float t = elapsedTime % totalAnimationTime; // Modulo to ensure looping
            int frameIndex1 = (int)t;
            int frameIndex2 = (frameIndex1 + 1) % frameCount;

            float interpolationFactor = t - frameIndex1;

            return LinearInterpolation(keyframes[frameIndex1], keyframes[frameIndex2], interpolationFactor);
        }

    }
}