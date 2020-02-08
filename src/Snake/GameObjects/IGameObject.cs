using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SnakeGame.GameObjects
{
    public interface IGameObject
    {
        IList<Vector2> Positions { get; }
        Type Type { get; }
    }
}
