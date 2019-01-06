using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Snake
{
    class GameGrid
    {
        public GameGrid(Vector2 gridDimensions, int gridSquareSizeInPixels)
        {
            GridDimensions = gridDimensions;
            GridSquareSize = gridSquareSizeInPixels;
        }

        public Vector2 GridDimensions { get; }
        public int GridSquareSize { get; }
        public Vector2 SizeInPixels
        {
            get => new Vector2
            {
                X = (int)GridDimensions.X * GridSquareSize,
                Y = (int)GridDimensions.Y * GridSquareSize
            };
        }
        public Vector2 Middle
        {
            get => new Vector2
            {
                X = (int)GridDimensions.X / 2,
                Y = (int)GridDimensions.Y / 2
            };
        }

        public Vector2 GetRandomPosition()
        {
            Random random = new Random();

            return new Vector2
            {
                X = random.Next((int)GridDimensions.X),
                Y = random.Next((int)GridDimensions.Y)
            };
        }
    }
}
