namespace VoxelWorld.Classes.World
{
    public struct Block
    {
        public int Id { get; set; } // ID Блока
        public int[] TextureFaces { get; set; } // ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN
        public bool IsTransparent { get; set; }
        public bool IsSolid { get; set; }
        public int DrawGroup { get; set; }

        // Конструктор для удобства создания блока
        public Block(int id = 0, int[] textureFaces = null, bool istransparent = false, bool issolid = false, int drawGroup = 0)
        {
            Id = id;
            TextureFaces = textureFaces ?? new int[]{0, 0, 0, 0, 0, 0};
            IsTransparent = istransparent;
            IsSolid = issolid;
            DrawGroup = drawGroup;
        }
    }
}