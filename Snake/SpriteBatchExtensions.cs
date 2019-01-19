using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.GameObjects;
using SnakeGame.Models;
using SnakeGame.Textures;
using System;

namespace SnakeGame
{
    static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Snake snake, SnakeTextureSet snakeTextureSet)
        {
            for (int i = 0; i < snake.Positions.Count; i++)
            {
                Vector2 position = snake.Positions[i];

                if (position == snake.HeadPosition || snake.Positions.Count - 1 == i)
                {
                    bool isHead = position == snake.HeadPosition;

                    float rotation = 0;
                    int modifier;
                    Direction direction;

                    if (isHead)
                    {
                        modifier = 0;
                        direction = snake.Direction;
                    }
                    else
                    {
                        modifier = 180;
                        direction = GameGrid.CalcDirectionBetweenAdjacent(position, snake.Positions[i - 1]);
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
                        Vector2 drawPosition = GameGrid.GetDrawPosition(snakeTextureSet.Head, position);
                        spriteBatch.Draw(snakeTextureSet.Head, drawPosition, null, Color.White, rotation, snakeTextureSet.Head.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        Vector2 drawPosition = GameGrid.GetDrawPosition(snakeTextureSet.Tail, position);
                        spriteBatch.Draw(snakeTextureSet.Tail, drawPosition, null, Color.White, rotation, snakeTextureSet.Tail.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    Direction directionNext = GameGrid.CalcDirectionBetweenAdjacent(position, snake.Positions[i - 1]);
                    Direction directionPrev = GameGrid.CalcDirectionBetweenAdjacent(position, snake.Positions[i + 1]);

                    float rotation = 0.0f;

                    if (directionNext.IsOpposite(directionPrev))
                    {
                        if (directionNext == Direction.North || directionNext == Direction.South)
                            rotation = DegreesToRadians.Calculate(0);
                        else if (directionNext == Direction.East || directionNext == Direction.West)
                            rotation = DegreesToRadians.Calculate(90);

                        Vector2 drawPosition = GameGrid.GetDrawPosition(snakeTextureSet.MiddleStraight, position);
                        spriteBatch.Draw(snakeTextureSet.MiddleStraight, drawPosition, null, Color.White, rotation, snakeTextureSet.MiddleStraight.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        if ((directionNext == Direction.East && directionPrev == Direction.South) || (directionNext == Direction.South && directionPrev == Direction.East))
                            rotation = DegreesToRadians.Calculate(0);
                        else if ((directionNext == Direction.South && directionPrev == Direction.West) || (directionNext == Direction.West && directionPrev == Direction.South))
                            rotation = DegreesToRadians.Calculate(90);
                        else if ((directionNext == Direction.West && directionPrev ==Direction.North) || (directionNext == Direction.North && directionPrev == Direction.West))
                            rotation = DegreesToRadians.Calculate(180);
                        else if ((directionNext == Direction.North && directionPrev == Direction.East) || (directionNext == Direction.East && directionPrev == Direction.North))
                            rotation = DegreesToRadians.Calculate(270);

                        Vector2 drawPosition = GameGrid.GetDrawPosition(snakeTextureSet.MiddleCorner, position);
                        spriteBatch.Draw(snakeTextureSet.MiddleCorner, drawPosition, null, Color.White, rotation, snakeTextureSet.MiddleCorner.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                    }
                }
            }
        }

        public static void Draw(this SpriteBatch spriteBatch, Food food, Texture2D texture)
        {
            Vector2 drawPosition = new Vector2
            {
                X = food.Position.X * GameGrid.GridSquareSizeInPixels,
                Y = food.Position.Y * GameGrid.GridSquareSizeInPixels,
            };

            spriteBatch.Draw(texture, drawPosition, Color.White);
        }

        
    }
}
