using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.GameObjects;
using SnakeGame.Models;

namespace SnakeGame
{
    public class Player
    {
        private Direction _direction;

        public Input Input { get; }
        public Snake Snake { get; }
        public Color Color { get; }

        public Player(Input input, Color color, GraphicsDevice graphicsDevice, Vector2 startPosition)
        {
            Input = input;
            Snake = new Snake(graphicsDevice, color, startPosition, Direction.North);
            Color = color;
        }

        public void Update()
        {
            Snake.ChangeDirection(_direction);
            Snake.Update();
        }

        public void UpdateInput()
        {
            if (Keyboard.GetState().IsKeyDown(Input.North))
            {
                _direction = Direction.North;
            }

            if (Keyboard.GetState().IsKeyDown(Input.East))
            {
                _direction = Direction.East;
            }

            if (Keyboard.GetState().IsKeyDown(Input.South))
            {
                _direction = Direction.South;
            }

            if (Keyboard.GetState().IsKeyDown(Input.West))
            {
                _direction = Direction.West;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Snake.Draw(spriteBatch);
        }
    }
}
