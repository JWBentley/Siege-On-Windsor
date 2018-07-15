using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Screens;
using System;

namespace SiegeOnWindsor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SiegeGame : Game
    {
        // Data //
        Screen currentScreen;

        // Graphics //
        GraphicsDeviceManager _graphics;

        // Screens //
        ScreenManager screenManager;

        public SiegeGame()
        {
            currentScreen = Screen.GAME;

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
            };


            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;

            screenManager = new ScreenManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            screenManager.InitializeScreens();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            screenManager.LoadScreensContent();

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            screenManager.UnloadScreensContent();

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            screenManager.GetScreen(currentScreen).Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            screenManager.GetScreen(currentScreen).Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
