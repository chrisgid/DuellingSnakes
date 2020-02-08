using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnakeVersusSnake.GameObjects;
using SnakeVersusSnake.Models;

namespace SnakeVersusSnake
{
    public static class GameGrid
    {
        public static List<IGameObject> GameObjects { get; } = new List<IGameObject>();

        public static Vector2 GridDimensions { get; } = new Vector2(30, 20);

        public static int GridSquareSizeInPixels { get; } = 30;

        public static Vector2 SizeInPixels
        {
            get
            {
                return new Vector2
                {
                    X = (int) GridDimensions.X * GridSquareSizeInPixels,
                    Y = (int) GridDimensions.Y * GridSquareSizeInPixels
                };
            }
        }

        public static Vector2 Middle
        {
            get
            {
                return new Vector2
                {
                    X = (int) (GridDimensions.X / 2),
                    Y = (int) (GridDimensions.Y / 2)
                };
            }
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
            var random = new Random();
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
            var direction = Direction.North;

            var xDifference = (int)startPosition.X - (int)endPosition.X;
            var yDifference = (int)startPosition.Y - (int)endPosition.Y;

            if ((yDifference != 0 && xDifference != 0) || (yDifference == 0 && xDifference == 0))
            {
                throw new ArgumentOutOfRangeException("Positions must be adjacent on the grid");
            }

            if (Math.Abs(xDifference) == Math.Abs((int)GridDimensions.X - 1))
            {
                direction = xDifference < 0 ? Direction.West : Direction.East;
            }
            else if (Math.Abs(xDifference) == 1)
            {
                direction = xDifference < 0 ? Direction.East : Direction.West;
            }
        
            if (Math.Abs(yDifference) == Math.Abs((int)GridDimensions.Y - 1))
            {
                direction = yDifference < 0 ? Direction.North : Direction.South;
            }
            else if (Math.Abs(yDifference) == 1)
            {
                direction = yDifference < 0 ? Direction.South : Direction.North;
            }

            return direction;
        }

        public static void AddGameObject(IGameObject gameGridObject)
        {
            GameObjects.Add(gameGridObject);
        }

        public static Vector2 GetDrawPosition(Texture2D texture, Vector2 gameGridPosition)
        {
            var middle = texture.Bounds.Center.ToVector2();

            return new Vector2
            {
                X = (gameGridPosition.X * GridSquareSizeInPixels) + middle.X,
                Y = (gameGridPosition.Y * GridSquareSizeInPixels) + middle.Y,
            };
        }

        private static List<Vector2> GetOccupiedPositions()
        {
            var occupiedPositions = new List<Vector2>();

            foreach (var gameGridObject in GameObjects)
            {
                occupiedPositions.AddRange(gameGridObject.Positions);
            }

            return occupiedPositions;
        }
    }
}
