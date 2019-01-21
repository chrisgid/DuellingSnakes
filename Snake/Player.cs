using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.GameObjects;
using SnakeGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Player
    {
        private Direction direction;

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
            Snake.ChangeDirection(direction);
            Snake.Update();
        }

        public void UpdateInput()
        {
            if (Keyboard.GetState().IsKeyDown(Input.North))
                direction = Direction.North;

            if (Keyboard.GetState().IsKeyDown(Input.East))
                direction = Direction.East;

            if (Keyboard.GetState().IsKeyDown(Input.South))
                direction = Direction.South;

            if (Keyboard.GetState().IsKeyDown(Input.West))
                direction = Direction.West;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Snake.Draw(spriteBatch);
        }
    }
}
