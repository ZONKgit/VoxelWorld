using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    // Класс от которого наследуються все 3D объекты
    public class Object : Node
    {
        public Vector3 LocalRotation;
        public Vector3 LocalPosition;
        public Vector3 LocalScale;
        
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        public void SetPosition(Vector3 newPosition) { Position = newPosition + LocalPosition; }
        public void SetRotation(Vector3 newRotation) { Rotation = newRotation + LocalRotation; }
        public void SetScale(Vector3 newScale) { Scale = newScale + LocalScale; }
        
    }
}