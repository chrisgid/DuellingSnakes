using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame.GameObjects
{
    class Food : IGameObject
    {
        private Texture2D _texture;

        public Food(Vector2 position, Texture2D texture)
        {
            _texture = texture;
            Position = position;
        }

        public Vector2 Position { get; set; }
        public IList<Vector2> Positions { get => new List<Vector2> { Position }; }
        public Type Type { get => typeof(Food); }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawPosition = new Vector2
            {
                X = Position.X * GameGrid.GridSquareSizeInPixels,
                Y = Position.Y * GameGrid.GridSquareSizeInPixels,
            };

            spriteBatch.Draw(_texture, drawPosition, Color.White);
        }
    }
}
