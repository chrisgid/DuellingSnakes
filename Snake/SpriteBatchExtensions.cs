using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Snake snake, SnakeTextureSet snakeTextureSet, GameGrid gameGrid)
        {
            for (int i = 0; i < snake.Positions.Count; i++)
            {
                Vector2 position = snake.Positions[i];

                Vector2 drawPosition = new Vector2
                {
                    X = position.X * gameGrid.GridSquareSizeInPixels,
                    Y = position.Y * gameGrid.GridSquareSizeInPixels,
                };

                if (position == snake.HeadPosition)
                    spriteBatch.Draw(snakeTextureSet.SnakeHead, drawPosition, Color.White);
                else
                    spriteBatch.Draw(snakeTextureSet.SnakeMiddleStraight, drawPosition, Color.White);
            }
        }

        public static void Draw(this SpriteBatch spriteBatch, Food food, Texture2D texture, GameGrid gameGrid)
        {
            Vector2 drawPosition = new Vector2
            {
                X = food.Position.X * gameGrid.GridSquareSizeInPixels,
                Y = food.Position.Y * gameGrid.GridSquareSizeInPixels,
            };

            spriteBatch.Draw(texture, drawPosition, Color.White);
        }
    }
}
