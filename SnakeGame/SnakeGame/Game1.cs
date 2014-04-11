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
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here

            pellet = new Vector2(rand.Next(2, 70), rand.Next(2, 40));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.


           

            Snake.Add(new Vector2(40, 24));
            
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
                Snake[0] += velocity;
                snakeMovementTimer = 0f;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Up))
            {
                velocity = new Vector2(0, -1);
            }

            if (kb.IsKeyDown(Keys.Down))
            {
                velocity = new Vector2(0, 1);
            }

            if (kb.IsKeyDown(Keys.Left))
            {
                velocity = new Vector2(-1, 0);
            }

            if (kb.IsKeyDown(Keys.Right))
            {
                velocity = new Vector2(1, 0);
            }

            if (Snake[0] == pellet)
            {
                pellet = new Vector2(rand.Next(2, 70), rand.Next(2, 40));
                Snake.Add(new Vector2(Snake[0].X, Snake[0].Y));

            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
                

            spriteBatch.Begin();

             for (int i = 0; i < Snake.Count; i++) 
            {
             
                spriteBatch.Draw(SnakeTexture, Snake[i] * 10, Color.Black);
                 spriteBatch.Draw(PelletTexture, pellet * 10, Color.White);

            }

            
            spriteBatch.End();

            
            base.Draw(gameTime);

        }
    }
}
