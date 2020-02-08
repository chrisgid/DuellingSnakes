using Microsoft.Xna.Framework;

namespace SnakeGame.Models
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

        public static Vector2 ToVector2(this Direction direction)
        {
            switch (direction)
            {
                default:
                    return DirectionVector.North;
                case Direction.East:
                    return DirectionVector.West;
                case Direction.South:
                    return DirectionVector.South;
                case Direction.West:
                    return DirectionVector.East;
            }
        }
    }

    public static class DirectionVector
    {
        public static Vector2 North => new Vector2 { X = 0, Y = -1 };
        public static Vector2 East => new Vector2 { X = -1, Y = 0 };
        public static Vector2 South => new Vector2 { X = 0, Y = 1 };
        public static Vector2 West => new Vector2 { X = 1, Y = 0 };
    }
}
