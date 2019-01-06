using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class CollisionDetector
    {
        public static bool SnakeOutOfBounds(Snake snake, GameGrid gameGrid)
        {
            int snakeX = (int)snake.HeadPosition.X;
            int snakeY = (int)snake.HeadPosition.Y;

            int gridX = (int)gameGrid.GridDimensions.X;
            int gridY = (int)gameGrid.GridDimensions.Y;

            if (snakeX < 0 ||
                snakeY < 0 ||
                snakeX > gridX ||
                snakeY > gridY) { }

            return true;
        }
    }
}
