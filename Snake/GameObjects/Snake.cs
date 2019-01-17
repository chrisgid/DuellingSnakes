using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SnakeGame.Models;

namespace SnakeGame.GameObjects
{
    public class Snake : IGameObject
    {
        private bool _alive = true;
        private int _defaultLength = 3;
        private bool _justEaten = false;
        private List<Vector2> _postitions = new List<Vector2>();
        private Vector2 _gridDimensions = GameGrid.GridDimensions;
        private Direction _direction;

        public Snake(Vector2 initialPosition, Direction initialDirection = Direction.North)
        {
            _postitions.Add(initialPosition);
            _direction = initialDirection;
            AddDefaultTailPositions();
        }

        public bool Alive { get => _alive; }
        public Vector2 HeadPosition { get => _postitions[0]; }
        public IList<Vector2> Positions { get => _postitions; }
        public Direction Direction { get => _direction; }
        public int Length { get => _postitions.Count(); }
        public Type Type { get => typeof(Snake); }

        private Vector2 DirectionVector { get => Direction.ToVector2(); }

        public Snake Eat()
        {
            if (Alive)
                _justEaten = true;

            return this;
        }

        public void Update()
        {
            if (Alive)
            {
                int headPositionX = (int)HeadPosition.X + (int)DirectionVector.X;
                int headPositionY = (int)HeadPosition.Y + (int)DirectionVector.Y;

                Vector2 newHeadPosition = GameGrid.GetWrappedPosition(headPositionX, headPositionY);

                // Calculate new positions,
                // check if new positions collide with anything,
                // 

                // New collision detection here
                foreach (IGameObject gameObject in GameGrid.GameObjects)
                {
                    if (gameObject.Positions.Contains(newHeadPosition))
                    {
                        if (gameObject.Type == typeof(Food))
                        {
                            Eat();
                        }
                        //else if (gameObject.Type == typeof(Wall))
                        //{
                        //    Kill();
                        //}
                        //else if (gameObject.Type == typeof(Snake) && gameObject != this)
                        //{
                        //    Kill();
                        //}
                    }
                }
                // New collision detection here

                _postitions.Insert(0, newHeadPosition);

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
            if (!newDirection.IsOpposite(Direction) && Alive)
            {
                _direction = newDirection;
            }
        }

        private void AddDefaultTailPositions()
        {
            int xModifier = (int)DirectionVector.X * -1;
            int yModifier = (int)DirectionVector.Y * -1;

            for (int i = 1; i < _defaultLength; i++)
            {
                _postitions.Add(new Vector2
                {
                    X = HeadPosition.X + (xModifier * i),
                    Y = HeadPosition.Y + (yModifier * i)
                });
            }
        }
    }
}
