using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeGame.GameObjects;
using SnakeGame.Models;

namespace SnakeGame
{
    static class GameGrid // Make this a static class? A game will only ever have one game grid
    {
        private static readonly int _gridSquareSizeInPixels = 30;
        private static List<IGameObject> _gameObjects = new List<IGameObject>();
        private static Vector2 _gridDimensions = new Vector2(30, 20);

        public static List<IGameObject> GameObjects { get => _gameObjects; }
        public static Vector2 GridDimensions { get => _gridDimensions; }
        public static int GridSquareSizeInPixels { get => _gridSquareSizeInPixels; }
        public static Vector2 SizeInPixels
        {
            get => new Vector2
            {
                X = (int)GridDimensions.X * GridSquareSizeInPixels,
                Y = (int)GridDimensions.Y * GridSquareSizeInPixels
            };
        }
        public static Vector2 Middle
        {
            get => new Vector2
            {
                X = (int)GridDimensions.X / 2,
                Y = (int)GridDimensions.Y / 2
            };
        }

        public static Vector2 GetWrappedPosition(Vector2 position)
        {
            return new Vector2
            {
                X = (position.X + GridDimensions.X) % GridDimensions.X,
                Y = (position.Y + GridDimensions.Y) % GridDimensions.Y
            };
        }

        public static Vector2 GetWrappedPosition(int x, int y)
        {
            return GetWrappedPosition(new Vector2 { X = x, Y = y });
        }

        public static Vector2 GetRandomFreePosition()
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

        public static Direction CalcDirectionBetweenAdjacent(Vector2 startPosition, Vector2 endPosition)
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

        public static void AddGameObject(IGameObject gameGridObject)
        {
            _gameObjects.Add(gameGridObject);
        }

        public static Vector2 GetDrawPosition(Texture2D texture, Vector2 gameGridPosition)
        {
            Vector2 middle = texture.Bounds.Center.ToVector2();

            return new Vector2
            {
                X = (gameGridPosition.X * _gridSquareSizeInPixels) + middle.X,
                Y = (gameGridPosition.Y * _gridSquareSizeInPixels) + middle.Y,
            };
        }

        private static List<Vector2> GetOccupiedPositions()
        {
            List<Vector2> occupiedPositions = new List<Vector2>();

            foreach (IGameObject gameGridObject in _gameObjects)
            {
                occupiedPositions.AddRange(gameGridObject.Positions);
            }

            return occupiedPositions;
        }
    }
}
