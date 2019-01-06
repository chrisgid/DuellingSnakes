using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Snake
{
    class Food
    {
        public Food(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; set; }
    }
}
