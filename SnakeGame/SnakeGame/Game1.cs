using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SnakeGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Vector2 velocity = new Vector2(0, -1);
        Vector2 pellet;
        List<Vector2> Snake = new List<Vector2>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand = new Random();
        Texture2D SnakeTexture, PelletTexture;

        float snakeMovementTimer = 0f;
        float snakeMovementTime = 50f;

        int snakeDimension = 20;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Score: 1";
        }

       
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            pellet = new Vector2(rand.Next(2, this.Window.ClientBounds.Width / snakeDimension - 1), rand.Next(2, this.Window.ClientBounds.Height / snakeDimension - 1));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.




            Snake.Add(new Vector2(this.Window.ClientBounds.Width / snakeDimension / 2, this.Window.ClientBounds.Height / snakeDimension / 2));
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SnakeTexture = Content.Load<Texture2D>(@"Snake");
            PelletTexture = Content.Load<Texture2D>(@"pellet");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void RescaleSnake(int newDimension)
        {
            float ratio = ((float)this.Window.ClientBounds.Width / ((float)newDimension)) / ((float)this.Window.ClientBounds.Width / ((float)snakeDimension));

            for (int i = 0; i < Snake.Count; i++)
            {
                Snake[i] *= ratio;
                Snake[i] = new Vector2((float)(Snake[i].X), (float)(Snake[i].Y));
            }

            pellet *= ratio;
            pellet = new Vector2((float)(pellet.X), (float)(pellet.Y));

            snakeDimension = newDimension;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit

            snakeMovementTimer += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (snakeMovementTimer > snakeMovementTime)
            {
                for (int i = Snake.Count - 1; i > 0; i--)
                {
                    Snake[i] = Snake[i - 1];

                    

                }


                Snake[0] += velocity;
                snakeMovementTimer = 0f;

                for (int i = 1; i < Snake.Count; i++)
                {

                    int score = 0;
                    score = Snake.Count;
                    this.Window.Title = "Score: " + score;


                    if (Snake[0] == Snake[i])
                    {
                       

                        Snake.Clear();
                        Snake.Add(new Vector2(this.Window.ClientBounds.Width/snakeDimension/2, this.Window.ClientBounds.Height/snakeDimension/2));

                        score = 1;
                        this.Window.Title = "Score: " + score;
                        break;
                    }
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Up) && velocity.Y != 1)
            {
                velocity = new Vector2(0, -1);
            }

            if (kb.IsKeyDown(Keys.Down) && velocity.Y != -1)
            {
                velocity = new Vector2(0, 1);
            }

            if (kb.IsKeyDown(Keys.Left) && velocity.X != 1)
            {
                velocity = new Vector2(-1, 0);
            }

            if (kb.IsKeyDown(Keys.Right) && velocity.X != -1)
            {
                velocity = new Vector2(1, 0);
            }

            if (kb.IsKeyDown(Keys.OemPeriod))
            {
                if (snakeDimension < 40)
                    RescaleSnake(snakeDimension + 1);
            }

            if (kb.IsKeyDown(Keys.OemComma))
            {
                if (snakeDimension > 5)
                    RescaleSnake(snakeDimension - 1);
            }
          
            if ((int)Snake[0].X == (int)pellet.X && (int)Snake[0].Y == (int)pellet.Y)
            {
                pellet = new Vector2(rand.Next(2, this.Window.ClientBounds.Width/snakeDimension - 1), rand.Next(2, this.Window.ClientBounds.Height/snakeDimension - 1));
                Snake.Add(Snake[0]);

                
            }

            //float dist = (int)Math.Abs(Vector2.Distance(Snake[0], pellet));
            //snakeDimension = (int)MathHelper.Clamp(40f - dist, 5, 40);

            // TODO: Add your update logic here
            base.Update(gameTime);



        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
                

            spriteBatch.Begin();

            int ofsx = 1, ofsy = 1;

             for (int i = 0; i < Snake.Count; i++) 
            {
                spriteBatch.Draw(SnakeTexture, new Rectangle((int)Snake[i].X * (snakeDimension + 1), (int)Snake[i].Y * (snakeDimension + 1), snakeDimension, snakeDimension), new Rectangle(0, 0, SnakeTexture.Width, SnakeTexture.Height), Color.Cyan);

                //spriteBatch.Draw(SnakeTexture, Snake[i] * 10, Color.Cyan);


            }


            spriteBatch.Draw(PelletTexture, new Rectangle((int)pellet.X * (snakeDimension + 1), (int)pellet.Y * (snakeDimension + 1), snakeDimension, snakeDimension), new Rectangle(0, 0, PelletTexture.Width, PelletTexture.Height), Color.White);
            
            spriteBatch.End();

            
            base.Draw(gameTime);

        }
    }
}
