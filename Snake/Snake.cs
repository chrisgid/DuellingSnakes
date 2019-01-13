using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Snake
{
    class Snake : IGameGridObject
    {
        private bool _alive = true;
        private int _defaultLength = 3;
        private bool _justEaten = false;
        private Direction _direction;
        private List<Vector2> _postitions = new List<Vector2>();
        private Vector2 _gridDimensions;

        public Snake(
            Vector2 gridDimensions,
            Vector2 initialPosition,
            Direction initialDirection = Direction.North)
        {
            _gridDimensions = gridDimensions;
            _postitions.Add(initialPosition);
            _direction = initialDirection;
            AddDefaultTailPositions();
        }

        public bool Alive { get => _alive; }
        public Vector2 HeadPosition { get => _postitions[0]; }
        public IList<Vector2> Positions { get => _postitions; }
        public Direction Direction { get => _direction; }
        public int Length { get => _postitions.Count(); }

        public Snake Eat()
        {
            if (Alive)
                _justEaten = true;

            return this;
        }

        public void UpdatePosition()
        {
            if (Alive)
            {
                int headPositionX = (int)HeadPosition.X;
                int headPositionY = (int)HeadPosition.Y;

                ModifyPositionBasedOnDirection(ref headPositionX, ref headPositionY, positive: true);

                // Wrap X Axis
                if (headPositionX > _gridDimensions.X - 1)
                    headPositionX = 0;
                else if (headPositionX < 0)
                    headPositionX = (int)_gridDimensions.X - 1;
                // Wrap Y Axis
                if (headPositionY > _gridDimensions.Y - 1)
                    headPositionY = 0;
                else if (headPositionY < 0)
                    headPositionY = (int)_gridDimensions.Y - 1;


                _postitions.Insert(0, new Vector2(headPositionX, headPositionY));

                Vector2 lastTailPosition = new Vector2(-1, -1);

                if (_justEaten)
                {
                    _justEaten = false;
                }
                else
                {
                    lastTailPosition = _postitions[_postitions.Count() - 1];
                    _postitions.RemoveAt(_postitions.Count() - 1);
                }

                if (_postitions.Count != _postitions.Distinct().Count())
                {
                    if (lastTailPosition != new Vector2(-1, -1))
                    {
                        _postitions.Add(lastTailPosition);
                    }

                    _postitions.RemoveAt(0);
                    Kill();
                }
            }
        }

        public void Kill()
        {
            _alive = false;
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (!newDirection.IsOpposite(Direction))
            {
                _direction = newDirection;
            }
        }

        private void AddDefaultTailPositions()
        {
            int xModifier = 0;
            int yModifier = 0;

            ModifyPositionBasedOnDirection(ref xModifier, ref yModifier, positive: false);

            for (int i = 1; i < _defaultLength; i++)
            {
                _postitions.Add(new Vector2
                {
                    X = HeadPosition.X + (xModifier * i),
                    Y = HeadPosition.Y + (yModifier * i)
                });
            }
        }

        private void ModifyPositionBasedOnDirection(ref int x, ref int y, bool positive)
        {
            int modifier;

            if (positive)
                modifier = 1;
            else
                modifier = -1;

            switch (Direction)
            {
                case Direction.North:
                    y -= modifier;
                    break;
                case Direction.East:
                    x += modifier;
                    break;
                case Direction.South:
                    y += modifier;
                    break;
                case Direction.West:
                    x -= modifier;
                    break;
            }
        }
    }
}
