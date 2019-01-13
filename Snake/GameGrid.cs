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
        private readonly int _gridSquareSizeInPixels = 25;
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

        private List<Vector2> OccupiedPositions
        {
            get
            {
                List<Vector2> occupiedPositions = new List<Vector2>();

                foreach (IGameGridObject gameGridObject in _gameGridObjects)
                {
                    occupiedPositions.AddRange(gameGridObject.Positions);
                }

                return occupiedPositions;
            }
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
            } while (OccupiedPositions.Contains(randomPosition));

            return randomPosition;
        }

        public void AddGameObject(IGameGridObject gameGridObject)
        {
            _gameGridObjects.Add(gameGridObject);
        }
    }
}
