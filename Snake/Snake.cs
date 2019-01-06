using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Snake
{
    class Snake
    {
        private bool _alive = true;
        private List<Vector2> _postitions = new List<Vector2>();

        public Snake(
            Vector2 initialPosition,
            Direction initialDirection)
        {
            _postitions.Add(initialPosition);
            Direction = initialDirection;
            AddDefaultTailPositions();
        }

        public bool Alive { get => _alive; }
        public Vector2 HeadPosition { get => _postitions[0]; }
        public Vector2[] Positions { get => _postitions.ToArray(); }
        public Direction Direction;
        public int Length { get => _postitions.Count(); }

        public void UpdatePosition(bool grow = false)
        {
            int headPositionX = (int)HeadPosition.X;
            int headPositionY = (int)HeadPosition.Y;

            switch (Direction)
            {
                case Direction.North:
                    headPositionY--;
                    break;
                case Direction.East:
                    headPositionX++;
                    break;
                case Direction.South:
                    headPositionY++;
                    break;
                case Direction.West:
                    headPositionX--;
                    break;
            }

            _postitions.Insert(0, new Vector2(headPositionX, headPositionY));

            if (!grow)
                _postitions.RemoveAt(_postitions.Count() - 1);
        }

        private void AddDefaultTailPositions()
        {
            int xModifier = 0;
            int yModifier = 0;

            switch (Direction)
            {
                case Direction.North:
                    yModifier = 1;
                    break;
                case Direction.East:
                    xModifier = -1;
                    break;
                case Direction.South:
                    yModifier = -1;
                    break;
                case Direction.West:
                    xModifier = 1;
                    break;
            }

            _postitions.Add(new Vector2
            (
                HeadPosition.X + xModifier,
                HeadPosition.Y + yModifier
            ));

            _postitions.Add(new Vector2
            (
                HeadPosition.X + (xModifier * 2),
                HeadPosition.Y + (yModifier * 2)
            ));
        }
    }
}
