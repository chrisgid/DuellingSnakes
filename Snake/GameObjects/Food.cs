using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SnakeGame.GameObjects
{
    class Food : IGameObject
    {
        public Food(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; set; }
        public IList<Vector2> Positions { get => new List<Vector2> { Position } ; }
        public Type Type { get => typeof(Food); }
    }
}
