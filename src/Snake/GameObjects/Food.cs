using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame.GameObjects
{
    public class Food : IGameObject
    {
        private readonly Texture2D _texture;

        public Food(Vector2 position, Texture2D texture)
        {
            _texture = texture;
            Position = position;
        }

        public Vector2 Position { get; set; }
        public IList<Vector2> Positions => new List<Vector2> { Position };
        public Type Type => typeof(Food);

        public void Update()
        {
            foreach (var gameObject in GameGrid.GameObjects)
            {
                if (gameObject.Positions.Contains(Position) && gameObject != this)
                {
                    Position = GameGrid.GetRandomFreePosition();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var drawPosition = new Vector2
            {
                X = Position.X * GameGrid.GridSquareSizeInPixels,
                Y = Position.Y * GameGrid.GridSquareSizeInPixels,
            };

            spriteBatch.Draw(_texture, drawPosition, Color.White);
        }
    }
}
