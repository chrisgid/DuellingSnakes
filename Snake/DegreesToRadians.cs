using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
