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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace sidescrollerdemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        KeyboardState keyboardState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level1 level1;
        Texture2D gameOverScreen;
        Texture2D startScreen;
        enum GameState { start, play, end };
        GameState state = GameState.start;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();            
        }

        protected override void LoadContent()
        {
            if (state.Equals(GameState.start))
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                startScreen = Content.Load<Texture2D>("splashscreenstart");
            }

            if (state.Equals(GameState.play))
            {
                level1 = new Level1(Content, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            }

            if (state.Equals(GameState.end))
            {
                gameOverScreen = Content.Load<Texture2D>("splashscreenlose");
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (state.Equals(GameState.play))
            {
                level1.Update(gameTime);
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (state.Equals(GameState.start))
            {
                DrawStartScreen();
            }

            if (state.Equals(GameState.play))
            {
                GraphicsDevice.Clear(Color.White);
                spriteBatch.Begin();
                level1.Draw(spriteBatch);
                spriteBatch.End();
                base.Draw(gameTime);
                if (level1.EndLevel())
                {
                    state = GameState.end;
                    LoadContent();
                }
            }

            if (state.Equals(GameState.end))
            {
                DrawGameOver();
            }

        }

        private void DrawGameOver()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Y))
            {
                state = GameState.start;
                LoadContent();
            }
            if (keyboardState.IsKeyDown(Keys.N))
            {
                Exit();
            }
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(gameOverScreen, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }

        private void DrawStartScreen()
        {
            
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                state = GameState.play;
                LoadContent();
            }
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
