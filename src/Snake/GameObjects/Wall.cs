using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.Textures;

namespace SnakeGame.GameObjects
{
    public class Wall : IGameObject
    {
        private readonly List<Vector2> _positions = new List<Vector2>();
        private readonly WallTextureSet _textureSet;

        public Wall(WallTextureSet textureSet)
        {
            _textureSet = textureSet;
        }

        public IList<Vector2> Positions => _positions;
        public Type Type => typeof(Wall);

        public void AddSection(Vector2 position)
        {
            if (!_positions.Contains(position))
            {
                _positions.Add(position);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var wallSection in _positions)
            {
                var drawPosition = new Vector2
                {
                    X = wallSection.X * GameGrid.GridSquareSizeInPixels,
                    Y = wallSection.Y * GameGrid.GridSquareSizeInPixels,
                };

                if (wallSection.X == 0 && wallSection.Y == 0)
                    spriteBatch.Draw(_textureSet.TopLeft, drawPosition, Color.White);
                else if (wallSection.X == GameGrid.GridDimensions.X - 1 && wallSection.Y == 0)
                    spriteBatch.Draw(_textureSet.TopRight, drawPosition, Color.White);
                else if (wallSection.X == 0 && wallSection.Y == GameGrid.GridDimensions.Y - 1)
                    spriteBatch.Draw(_textureSet.BottomLeft, drawPosition, Color.White);
                else if (wallSection.X == GameGrid.GridDimensions.X - 1 && wallSection.Y == GameGrid.GridDimensions.Y - 1)
                    spriteBatch.Draw(_textureSet.BottomRight, drawPosition, Color.White);
                else if (wallSection.X == 0)
                    spriteBatch.Draw(_textureSet.Left, drawPosition, Color.White);
                else if (wallSection.X == GameGrid.GridDimensions.X - 1)
                    spriteBatch.Draw(_textureSet.Right, drawPosition, Color.White);
                else if (wallSection.Y == 0)
                    spriteBatch.Draw(_textureSet.Top, drawPosition, Color.White);
                else if (wallSection.Y == GameGrid.GridDimensions.Y - 1)
                    spriteBatch.Draw(_textureSet.Bottom, drawPosition, Color.White);
            }
        }
    }
}
