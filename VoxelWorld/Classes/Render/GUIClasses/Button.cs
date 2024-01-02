using OpenTK;
using VoxelWorld.Classes.Engine;

namespace VoxelWorld.Classes.Render.GUIClasses
{
    public class Button
    {
        public TextureRect Texture = new TextureRect(new Vector2(0,0), new Vector4(0,0,1,1),new Vector2(1,1));
        //                                              Normal                         Pressed
        public Vector4[] UVs = new []{new Vector4(0,0.2578125f,0.22265625f*1.777777777777778f,0.33984375f), new Vector4(0,0,0.5f,0.5f)};
        public bool isPressed = false;

        public Button(Vector2 size)
        {
            Texture.size = size;
        }
        
        public void RenderProcess()
        {
            Texture.RenderProcess();
        }
        
        public void PhysicsProcess()
        {
            if (isPressed) Texture.UV = UVs[1]; else Texture.UV = UVs[0];
        }
    }
}