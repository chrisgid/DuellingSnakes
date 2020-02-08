using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeVersusSnake.GameObjects;
using SnakeVersusSnake.Models;
using SnakeVersusSnake.Textures;

namespace SnakeVersusSnake
{
    public class SnakeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _updateCount = 0;
        private GameMode _gameMode = GameMode.TwoPlayer;

        private Food _food;
        private Food _food2;
        private Wall _wall;
        private Player _playerOne;
        private Player _playerTwo;

        public SnakeGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = (int)GameGrid.SizeInPixels.X,
                PreferredBackBufferHeight = (int)GameGrid.SizeInPixels.Y
            };

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerOneStartPosition = new Vector2((int)(GameGrid.Middle.X / 2), GameGrid.Middle.Y);
            
            var playerOneInput = new Input
            {
                North = Keys.Up,
                East = Keys.Right,
                South = Keys.Down,
                West = Keys.Left
            };

            _playerOne = new Player(playerOneInput, Color.LimeGreen, GraphicsDevice, playerOneStartPosition);
            GameGrid.AddGameObject(_playerOne.Snake);


            if (_gameMode == GameMode.TwoPlayer)
            {
                var playerTwoStartPosition = new Vector2((int)(GameGrid.Middle.X / 3 * 4), GameGrid.Middle.Y);

                var playerTwoInput = new Input
                {
                    North = Keys.W,
                    East = Keys.D,
                    South = Keys.S,
                    West = Keys.A
                };

                _playerTwo = new Player(playerTwoInput, Color.Red, GraphicsDevice, playerTwoStartPosition);
                GameGrid.AddGameObject(_playerTwo.Snake);
            }

            /*
            wall = new Wall(new WallTextureSet(GraphicsDevice, GameGrid.GridSquareSizeInPixels, Color.Gray));
            GameGrid.AddGameObject(wall);

            for (int x = 0; x < GameGrid.GridDimensions.X; x++)
            {
                for (int y = 0; y < GameGrid.GridDimensions.Y; y++)
                {
                    if (x == 0 || y == 0 || x == GameGrid.GridDimensions.X - 1 || y == GameGrid.GridDimensions.Y - 1)
                    {
                        wall.AddSection(new Vector2(x, y));
                    }
                }
            }
            */

            var foodTexture = new FoodTexture(GraphicsDevice, GameGrid.GridSquareSizeInPixels, Color.Gray, Color.DarkGray);
            _food = new Food(GameGrid.GetRandomFreePosition(), foodTexture.Square);
            GameGrid.AddGameObject(_food);
            _food2 = new Food(GameGrid.GetRandomFreePosition(), foodTexture.Square);
            GameGrid.AddGameObject(_food2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return;
            }

            _playerOne.UpdateInput();

            //DEBUG Force Snake.Eat()
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                _playerOne.Snake.Eat();
            //DEBUG

            if (_gameMode == GameMode.TwoPlayer)
            {
                _playerTwo.UpdateInput();

                //DEBUG Force Snake.Eat()
                if (Keyboard.GetState().IsKeyDown(Keys.O))
                    _playerTwo.Snake.Eat();
                //DEBUG
            }
                
            if (_updateCount > 5)
            {
                _updateCount = 0;

                _food.Update();
                _food2.Update();

                _playerOne.Update();

                if (_gameMode == GameMode.TwoPlayer)
                {
                    _playerTwo.Update();
                }
            }

            _updateCount += 1;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            _playerOne.Draw(_spriteBatch);

            if (_gameMode == GameMode.TwoPlayer)
            {
                _playerTwo.Draw(_spriteBatch);
            }

            _food.Draw(_spriteBatch);
            _food2.Draw(_spriteBatch);
            //wall.Draw(spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
