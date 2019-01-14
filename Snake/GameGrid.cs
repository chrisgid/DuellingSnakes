using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Snake
{
    class GameGrid // Make this a static class? A game will only ever have one game grid
    {
        private readonly int _gridSquareSizeInPixels = 30;
        private List<IGameGridObject> _gameGridObjects;

        public GameGrid(Vector2 gridDimensions)
        {
            GridDimensions = gridDimensions;
            _gameGridObjects = new List<IGameGridObject>();
        }

        public Vector2 GridDimensions { get; }
        public int GridSquareSizeInPixels { get => _gridSquareSizeInPixels; }
        public Vector2 SizeInPixels
        {
            get => new Vector2
            {
                X = (int)GridDimensions.X * GridSquareSizeInPixels,
                Y = (int)GridDimensions.Y * GridSquareSizeInPixels
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

        public Vector2 GetWrappedPosition(Vector2 position)
        {
            return new Vector2
            {
                X = position.X % GridDimensions.X,
                Y = position.Y % GridDimensions.Y
            };
        }

        public Vector2 GetRandomFreePosition()
        {
            Random random = new Random();
            Vector2 randomPosition;

            do
            {
                randomPosition = new Vector2
                {
                    X = random.Next((int)GridDimensions.X - 1),
                    Y = random.Next((int)GridDimensions.Y - 1)
                };
            } while (GetOccupiedPositions().Contains(randomPosition));

            return randomPosition;
        }

        public Direction CalcDirectionBetweenAdjacent(Vector2 startPosition, Vector2 endPosition)
        {
            Direction direction = Direction.North;

            int xDifference = (int)startPosition.X - (int)endPosition.X;
            int yDifference = (int)startPosition.Y - (int)endPosition.Y;

            if ((yDifference != 0 && xDifference != 0) || (yDifference == 0 && xDifference == 0))
            {
                throw new ArgumentOutOfRangeException("Positions must be adjacent on the grid");
            }

            if (Math.Abs(xDifference) == Math.Abs(GridDimensions.X - 1))
            {
                if (xDifference < 0)
                    direction = Direction.West;
                else
                    direction = Direction.East;
            }
            else if (Math.Abs(xDifference) == 1)
            {
                if (xDifference < 0)
                    direction = Direction.East;
                else
                    direction = Direction.West;
            }
        
            if (Math.Abs(yDifference) == Math.Abs(GridDimensions.Y - 1))
            {
                if (yDifference < 0)
                    direction = Direction.North;
                else
                    direction = Direction.South;
            }
            else if (Math.Abs(yDifference) == 1)
            {
                if (yDifference < 0)
                    direction = Direction.South;
                else
                    direction = Direction.North;
            }

            return direction;
        }

        public void AddGameObject(IGameGridObject gameGridObject)
        {
            _gameGridObjects.Add(gameGridObject);
        }

        private List<Vector2> GetOccupiedPositions()
        {
            List<Vector2> occupiedPositions = new List<Vector2>();

            foreach (IGameGridObject gameGridObject in _gameGridObjects)
            {
                occupiedPositions.AddRange(gameGridObject.Positions);
            }

            return occupiedPositions;
        }
    }
}
