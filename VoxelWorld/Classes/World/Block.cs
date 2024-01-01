namespace VoxelWorld.Classes.World
{
    public struct Block
    {
        public int Id { get; set; } // ID Блока
        public int[] TextureFaces { get; set; } // ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN
        public bool isTransparent { get; set; }
        public bool IsSolid { get; set; }

        // Конструктор для удобства создания блока
        public Block(int id = 0, int[] textureFaces = null, bool istransparent = false, bool issolid = false)
        {
            Id = id;
            TextureFaces = textureFaces ?? new int[]{0, 0, 0, 0, 0, 0};
            isTransparent = istransparent;
            IsSolid = issolid;
        }
    }
}