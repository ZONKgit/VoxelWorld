namespace VoxelWorld.Classes.World
{
    public static class Blocks
    {
        //                                        ID Текстур блока UP;LEFT;FRONT;RIGHT;BACK;DOWN
        public static Block air = new Block(0, new int[] {15, 15, 15, 15, 15, 15}, true, false);
        public static Block grass = new Block(1, new int[] {1, 4, 4, 4, 4, 2 }, false, true);
        public static Block stone = new Block(2, new int[] {10, 10, 10, 10, 10, 10 }, false, true);
        public static Block glass = new Block(3, new int[] {5, 5, 5, 5, 5, 5 }, true, true);
    }
}