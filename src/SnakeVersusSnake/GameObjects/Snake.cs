using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeVersusSnake.Models;
using SnakeVersusSnake.Textures;

namespace SnakeVersusSnake.GameObjects
{
    public class Snake : IGameObject
    {
        private bool _alive = true;
        private int _defaultLength = 3;
        private bool _justEaten = false;
        private readonly List<Vector2> _positions = new List<Vector2>();
        private Vector2 _gridDimensions = GameGrid.GridDimensions;
        private Direction _direction;
        private readonly SnakeTextureSet _textureSet;

        public Snake(GraphicsDevice graphicsDevice, Color color, Vector2 initialPosition, Direction initialDirection = Direction.North)
        {
            _textureSet = new SnakeTextureSet(graphicsDevice, GameGrid.GridSquareSizeInPixels, color);
            _positions.Add(initialPosition);
            _direction = initialDirection;
            AddDefaultTailPositions();
        }

        public bool Alive => _alive;
        public Vector2 HeadPosition => _positions[0];
        public IList<Vector2> Positions => _positions;
        public Direction Direction => _direction;
        public int Length => _positions.Count();
        public Type Type => typeof(Snake);

        private Vector2 DirectionVector => Direction.ToVector2();

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
                var headPositionX = (int)HeadPosition.X + (int)DirectionVector.X;
                var headPositionY = (int)HeadPosition.Y + (int)DirectionVector.Y;

                var newHeadPosition = GameGrid.GetWrappedPosition(headPositionX, headPositionY);

                foreach (var gameObject in GameGrid.GameObjects)
                {
                    if (gameObject.Positions.Contains(newHeadPosition))
                    {
                        if (gameObject.Type == typeof(Food))
                        {
                            Eat();
                        }
                        else if (gameObject.Type == typeof(Wall))
                        {
                            Kill();
                        }
                        else if (gameObject.Type == typeof(Snake) && gameObject != this)
                        {
                            Kill();
                        }
                    }
                }

                // Detect collision with self
                if (Alive)
                {
                    if (_justEaten)
                    {
                        _justEaten = false;

                        if (_positions.Contains(newHeadPosition))
                        {
                            Kill();
                        }
                    }
                    else
                    {
                        if (_positions.GetRange(0, _positions.Count - 1).Contains(newHeadPosition))
                        {
                            Kill();
                        }
                        else
                        {
                            _positions.RemoveAt(_positions.Count - 1);
                        }
                    }
                }

                if (Alive)
                {
                    _positions.Insert(0, newHeadPosition);
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
            var xModifier = (int)DirectionVector.X * -1;
            var yModifier = (int)DirectionVector.Y * -1;

            for (var i = 1; i < _defaultLength; i++)
            {
                _positions.Add(new Vector2
                {
                    X = HeadPosition.X + (xModifier * i),
                    Y = HeadPosition.Y + (yModifier * i)
                });
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Positions.Count; i++)
            {
                var position = Positions[i];

                if (position == HeadPosition || Positions.Count - 1 == i)
                {
                    var isHead = position == HeadPosition;

                    float rotation = 0;
                    int modifier;
                    Direction direction;

                    if (isHead)
                    {
                        modifier = 0;
                        direction = Direction;
                    }
                    else
                    {
                        modifier = 180;
                        direction = GameGrid.CalcDirectionBetweenAdjacent(position, Positions[i - 1]);
                    }

                    switch (direction)
                    {
                        case Direction.North:
                            rotation = DegreesToRadians.Calculate(0 + modifier);
                            break;
                        case Direction.East:
                            rotation = DegreesToRadians.Calculate(90 + modifier);
                            break;
                        case Direction.South:
                            rotation = DegreesToRadians.Calculate(180 + modifier);
                            break;
                        case Direction.West:
                            rotation = DegreesToRadians.Calculate(270 + modifier);
                            break;
                    }


                    if (isHead)
                    {
                        var drawPosition = GameGrid.GetDrawPosition(_textureSet.Head, position);
                        spriteBatch.Draw(_textureSet.Head, drawPosition, null, Color.White, rotation, _textureSet.Head.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        var drawPosition = GameGrid.GetDrawPosition(_textureSet.Tail, position);
                        spriteBatch.Draw(_textureSet.Tail, drawPosition, null, Color.White, rotation, _textureSet.Tail.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    var directionNext = GameGrid.CalcDirectionBetweenAdjacent(position, Positions[i - 1]);
                    var directionPrev = GameGrid.CalcDirectionBetweenAdjacent(position, Positions[i + 1]);

                    var rotation = 0.0f;

                    if (directionNext.IsOpposite(directionPrev))
                    {
                        switch (directionNext)
                        {
                            case Direction.North:
                            case Direction.South:
                                rotation = DegreesToRadians.Calculate(0);
                                break;
                            case Direction.East:
                            case Direction.West:
                                rotation = DegreesToRadians.Calculate(90);
                                break;
                        }

                        var drawPosition = GameGrid.GetDrawPosition(_textureSet.MiddleStraight, position);
                        spriteBatch.Draw(_textureSet.MiddleStraight, drawPosition, null, Color.White, rotation, _textureSet.MiddleStraight.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        switch (directionNext)
                        {
                            case Direction.East when directionPrev == Direction.South:
                            case Direction.South when directionPrev == Direction.East:
                                rotation = DegreesToRadians.Calculate(0);
                                break;
                            case Direction.South when directionPrev == Direction.West:
                            case Direction.West when directionPrev == Direction.South:
                                rotation = DegreesToRadians.Calculate(90);
                                break;
                            case Direction.West when directionPrev == Direction.North:
                            case Direction.North when directionPrev == Direction.West:
                                rotation = DegreesToRadians.Calculate(180);
                                break;
                            case Direction.North when directionPrev == Direction.East:
                            case Direction.East when directionPrev == Direction.North:
                                rotation = DegreesToRadians.Calculate(270);
                                break;
                        }

                        var drawPosition = GameGrid.GetDrawPosition(_textureSet.MiddleCorner, position);
                        spriteBatch.Draw(_textureSet.MiddleCorner, drawPosition, null, Color.White, rotation, _textureSet.MiddleCorner.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}
