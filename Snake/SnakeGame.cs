using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Snake
{
    public class SnakeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SnakeTextureSet snakeTextureSet;
        Snake snake;
        Food food;
        GameGrid gameGrid;
        int updateCount = 0;
        Direction newDirection = Direction.North;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gameGrid = new GameGrid(new Vector2(30, 20));
            graphics.PreferredBackBufferWidth = (int)gameGrid.SizeInPixels.X;
            graphics.PreferredBackBufferHeight = (int)gameGrid.SizeInPixels.Y;
            graphics.ApplyChanges();

            snake = new Snake(gameGrid.GridDimensions, gameGrid.Middle, Direction.East);
            gameGrid.AddGameObject(snake);
            food = new Food(gameGrid.GetRandomFreePosition());
            gameGrid.AddGameObject(food);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            snakeTextureSet = new SnakeTextureSet(GraphicsDevice, gameGrid.GridSquareSizeInPixels, Color.LimeGreen);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
                            snake.Eat().UpdatePosition();

                            do
                            {
                                food.Position = gameGrid.GetRandomFreePosition();
                            } while (snake.Positions.Contains(food.Position));
                        }
                        else
                        {
                            snake.UpdatePosition();
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
            spriteBatch.Draw(food, snakeTextureSet.Square, gameGrid);
            spriteBatch.Draw(snake, snakeTextureSet, gameGrid);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
