using Microsoft.Xna.Framework;
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

        public Player(Input input, Snake snake, Color color)
        {
            Input = input;
            Snake = snake;
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
    }
}
