using System;

namespace SnakeGame
{
    internal static class DegreesToRadians
    {
        internal static float Calculate(int degrees)
        {
            return (float)(Math.PI / 180) * degrees;
        }
    }
}
