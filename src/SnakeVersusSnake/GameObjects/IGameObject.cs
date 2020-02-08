using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeVersusSnake.GameObjects
{
    public interface IGameObject
    {
        IList<Vector2> Positions { get; }
        Type Type { get; }
    }
}
