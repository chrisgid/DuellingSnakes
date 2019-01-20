using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.Models;
using SnakeGame.Textures;
using SnakeGame.GameObjects;

namespace SnakeGame
{
    public class SnakeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SnakeTextureSet snakeTextureSet;
        WallTextureSet wallTextureSet;
        Snake snake;
        Food food;
        Wall wall;
        int updateCount = 0;
        Direction newDirection = Direction.North;
        Player playerOne;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this)
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
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //Input playerOneInput = new Input
            //{
            //    North = Keys.Up,
            //    East = Keys.Right,
            //    South = Keys.Down,
            //    West = Keys.Left
            //};

            //Snake playerOneSnake = new Snake(GameGrid.Middle, Direction.North);

            //playerOne = new Player(playerOneInput, playerOneSnake, Color.LimeGreen);


            snakeTextureSet = new SnakeTextureSet(GraphicsDevice, GameGrid.GridSquareSizeInPixels, Color.LimeGreen);
            wallTextureSet = new WallTextureSet(GraphicsDevice, GameGrid.GridSquareSizeInPixels, Color.SandyBrown);

            snake = new Snake(snakeTextureSet, GameGrid.Middle, Direction.North);
            GameGrid.AddGameObject(snake);

            

            wall = new Wall(wallTextureSet);
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

            food = new Food(GameGrid.GetRandomFreePosition(), snakeTextureSet.Square);
            GameGrid.AddGameObject(food);
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
            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    newDirection = Direction.North;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    newDirection = Direction.East;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    newDirection = Direction.South;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    newDirection = Direction.West;

                //DEBUG Force Snake.Eat()
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                    snake.Eat();
                //DEBUG

                if (updateCount > 5)
                {
                    updateCount = 0;

                    if (snake.Alive)
                    {
                        snake.ChangeDirection(newDirection);

                        if (snake.HeadPosition == food.Position)
                        {
                            snake.Update();

                            do
                            {
                                food.Position = GameGrid.GetRandomFreePosition();
                            } while (snake.Positions.Contains(food.Position));
                        }
                        else
                        {
                            snake.Update();
                        }
                    }

                }

                base.Update(gameTime);
                updateCount += 1;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();
            food.Draw(spriteBatch);
            snake.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
