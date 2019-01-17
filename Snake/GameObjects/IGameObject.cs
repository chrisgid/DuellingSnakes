using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameObjects
{
    interface IGameObject
    {
        IList<Vector2> Positions { get; }
        Type Type { get; }
    }
}
