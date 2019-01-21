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
        int updateCount = 0;

        Food food;
        Food food2;
        Wall wall;
        Player playerOne;
        Player playerTwo;

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

            Vector2 playerOneStartPosition = new Vector2((int)(GameGrid.Middle.X / 2), GameGrid.Middle.Y);
            Vector2 playerTwoStartPosition = new Vector2((int)(GameGrid.Middle.X / 3 * 4), GameGrid.Middle.Y);

            Input playerOneInput = new Input
            {
                North = Keys.Up,
                East = Keys.Right,
                South = Keys.Down,
                West = Keys.Left
            };

            playerOne = new Player(playerOneInput, Color.LimeGreen, GraphicsDevice, playerOneStartPosition);
            GameGrid.AddGameObject(playerOne.Snake);

            Input playerTwoInput = new Input
            {
                North = Keys.W,
                East = Keys.D,
                South = Keys.S,
                West = Keys.A
            };

            playerTwo = new Player(playerTwoInput, Color.Red, GraphicsDevice, playerTwoStartPosition);
            GameGrid.AddGameObject(playerTwo.Snake);

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

            FoodTexture foodTexture = new FoodTexture(GraphicsDevice, GameGrid.GridSquareSizeInPixels, Color.Gray, Color.DarkGray);
            food = new Food(GameGrid.GetRandomFreePosition(), foodTexture.Square);
            GameGrid.AddGameObject(food);
            food2 = new Food(GameGrid.GetRandomFreePosition(), foodTexture.Square);
            GameGrid.AddGameObject(food2);
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
                //DEBUG Force Snake.Eat()
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                    playerOne.Snake.Eat();

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    playerTwo.Snake.Eat();
                //DEBUG

                playerOne.UpdateInput();
                playerTwo.UpdateInput();

                if (updateCount > 5)
                {
                    updateCount = 0;

                    food.Update();
                    food2.Update();

                    playerOne.Update();
                    playerTwo.Update();
                }

                updateCount += 1;
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();
            food.Draw(spriteBatch);
            food2.Draw(spriteBatch);

            playerOne.Draw(spriteBatch);
            playerTwo.Draw(spriteBatch);
            //wall.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
