using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.Models;
using SnakeGame.Textures;

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
        private SnakeTextureSet _textureSet;

        public Snake(GraphicsDevice graphicsDevice, Color color, Vector2 initialPosition, Direction initialDirection = Direction.North)
        {
            _textureSet = new SnakeTextureSet(graphicsDevice, GameGrid.GridSquareSizeInPixels, color);
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

                foreach (IGameObject gameObject in GameGrid.GameObjects)
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

                        if (_postitions.Contains(newHeadPosition))
                        {
                            Kill();
                        }
                    }
                    else
                    {
                        if (_postitions.GetRange(0, _postitions.Count - 1).Contains(newHeadPosition))
                        {
                            Kill();
                        }
                        else
                        {
                            _postitions.RemoveAt(_postitions.Count - 1);
                        }
                    }
                }

                if (Alive)
                {
                    _postitions.Insert(0, newHeadPosition);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            // Please don't judge

            for (int i = 0; i < this.Positions.Count; i++)
            {
                Vector2 position = this.Positions[i];

                if (position == this.HeadPosition || this.Positions.Count - 1 == i)
                {
                    bool isHead = position == this.HeadPosition;

                    float rotation = 0;
                    int modifier;
                    Direction direction;

                    if (isHead)
                    {
                        modifier = 0;
                        direction = this.Direction;
                    }
                    else
                    {
                        modifier = 180;
                        direction = GameGrid.CalcDirectionBetweenAdjacent(position, this.Positions[i - 1]);
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
                        Vector2 drawPosition = GameGrid.GetDrawPosition(_textureSet.Head, position);
                        spriteBatch.Draw(_textureSet.Head, drawPosition, null, Color.White, rotation, _textureSet.Head.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        Vector2 drawPosition = GameGrid.GetDrawPosition(_textureSet.Tail, position);
                        spriteBatch.Draw(_textureSet.Tail, drawPosition, null, Color.White, rotation, _textureSet.Tail.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    Direction directionNext = GameGrid.CalcDirectionBetweenAdjacent(position, this.Positions[i - 1]);
                    Direction directionPrev = GameGrid.CalcDirectionBetweenAdjacent(position, this.Positions[i + 1]);

                    float rotation = 0.0f;

                    if (directionNext.IsOpposite(directionPrev))
                    {
                        if (directionNext == Direction.North || directionNext == Direction.South)
                            rotation = DegreesToRadians.Calculate(0);
                        else if (directionNext == Direction.East || directionNext == Direction.West)
                            rotation = DegreesToRadians.Calculate(90);

                        Vector2 drawPosition = GameGrid.GetDrawPosition(_textureSet.MiddleStraight, position);
                        spriteBatch.Draw(_textureSet.MiddleStraight, drawPosition, null, Color.White, rotation, _textureSet.MiddleStraight.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        if ((directionNext == Direction.East && directionPrev == Direction.South) || (directionNext == Direction.South && directionPrev == Direction.East))
                            rotation = DegreesToRadians.Calculate(0);
                        else if ((directionNext == Direction.South && directionPrev == Direction.West) || (directionNext == Direction.West && directionPrev == Direction.South))
                            rotation = DegreesToRadians.Calculate(90);
                        else if ((directionNext == Direction.West && directionPrev == Direction.North) || (directionNext == Direction.North && directionPrev == Direction.West))
                            rotation = DegreesToRadians.Calculate(180);
                        else if ((directionNext == Direction.North && directionPrev == Direction.East) || (directionNext == Direction.East && directionPrev == Direction.North))
                            rotation = DegreesToRadians.Calculate(270);

                        Vector2 drawPosition = GameGrid.GetDrawPosition(_textureSet.MiddleCorner, position);
                        spriteBatch.Draw(_textureSet.MiddleCorner, drawPosition, null, Color.White, rotation, _textureSet.MiddleCorner.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}
