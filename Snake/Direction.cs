namespace Snake
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionMethods
    {
        public static Direction GetOpposite(this Direction direction)
        {
            switch (direction)
            {
                default:
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
            }
        }

        public static bool IsOpposite(this Direction thisDirection, Direction direction)
        {
            return thisDirection.GetOpposite() == direction;
        }
    }
}
