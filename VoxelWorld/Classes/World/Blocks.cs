namespace VoxelWorld.Classes.World
{
    public static class Blocks
    {
        //                                        ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN
        public static Block air = new Block(0, new int[] {0,0,0,0,0,0}, true, false, 1);
        public static Block grass = new Block(1, new int[] {1, 4, 4, 4, 4, 2 }, false, true, 0);
        public static Block stone = new Block(2, new int[] {10, 10, 10, 10, 10, 10 }, false, true, 0);
        public static Block glass = new Block(3, new int[] {5, 5, 5, 5, 5, 5 }, true, true, 2);
        public static Block dirt = new Block(4, new int[] {2, 2, 2, 2, 2, 2 }, false, true, 0);
        public static Block water = new Block(5, new int[] {11, 11, 11, 11, 11, 11 }, true, false, 3);
        public static Block sand = new Block(6, new int[] {12, 12, 12, 12, 12, 12 }, false, true, 0);
        public static Block oak_planks = new Block(7, new[] { 6, 6, 6, 6, 6, 6 }, false, true, 0);
        public static Block oak_log = new Block(8, new[] { 8,7,7,7,7,8 }, false, true, 0);
        public static Block oak_leaves = new Block(9, new[] { 9,9,9,9,9,9 }, false, true, 0);
    }
}