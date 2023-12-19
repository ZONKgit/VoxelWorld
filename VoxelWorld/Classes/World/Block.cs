namespace VoxelWorld.Classes.World
{
    public class Block
    {
        public int Id { get; set; } // ID Блока
        public int[] TextureFaces { get; set; } // ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN

        // Конструктор для удобства создания блока
        public Block(int id, int[] textureFaces)
        {
            Id = id;
            TextureFaces = textureFaces;
        }
    }
}