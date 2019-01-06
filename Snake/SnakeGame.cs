using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using static Snake.Snake;

namespace Snake
{
    public class SnakeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D snakeBlockTexture;
        Snake snake;
        Food food;
        GameGrid gameGrid;
        int updateCount = 0;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gameGrid = new GameGrid(new Vector2(10, 10), 25);
            graphics.PreferredBackBufferWidth = (int)gameGrid.SizeInPixels.X;
            graphics.PreferredBackBufferHeight = (int)gameGrid.SizeInPixels.Y;
            graphics.ApplyChanges();

            snake = new Snake(gameGrid.Middle, Direction.North);
            food = new Food(gameGrid.GetRandomPosition());
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            int gridSquareSize = gameGrid.GridSquareSize;
            snakeBlockTexture = new Texture2D(GraphicsDevice, gridSquareSize, gridSquareSize);
            Color[] colorData = new Color[gridSquareSize * gridSquareSize];

            for (int i = 0; i < gridSquareSize * gridSquareSize; i++)
            {
                colorData[i] = Color.Black;
            }

            snakeBlockTexture.SetData(colorData);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                snake.Direction = Direction.North;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                snake.Direction = Direction.East;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                snake.Direction = Direction.South;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                snake.Direction = Direction.West;

            if (updateCount > 5)
            {
                updateCount = 0;

                if (snake.Alive)
                {
                    
                    if (snake.HeadPosition == food.Position)
                    {
                        snake.UpdatePosition(true);
                        do
                        {
                            food.Position = gameGrid.GetRandomPosition();
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();
            spriteBatch.Draw(snakeBlockTexture, GetDrawPosition(food.Position), Color.White);

            foreach (Vector2 position in snake.Positions)
            {
                Vector2 drawPosition = GetDrawPosition(position);
                spriteBatch.Draw(snakeBlockTexture, drawPosition, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetDrawPosition(Vector2 snakeSectionPosition)
        {
            return new Vector2
            {
                X = snakeSectionPosition.X * gameGrid.GridSquareSize,
                Y = snakeSectionPosition.Y * gameGrid.GridSquareSize,
            };
        }
    }
}
