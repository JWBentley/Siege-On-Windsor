using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Graphics;
using SiegeOnWindsor.Screens;
using System;
using System.Collections.Generic;

namespace SiegeOnWindsor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SiegeGame : Game
    {
        // Data //
        public Screens.Screens currentScreen;

        // Graphics //
        public GraphicsDeviceManager _graphics;
        public List<Textures.Texture> loadBuffer;

        // Screens //
        ScreenManager screenManager;

        public SiegeGame()
        {
            currentScreen = Screens.Screens.MAIN_MENU;

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
            };
            _graphics.ApplyChanges();

            this.loadBuffer = new List<Textures.Texture>();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
            Window.Title = "Siege On Windsor Castle";

            screenManager = new ScreenManager(GraphicsDevice, this);
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

            screenManager.InitializeScreens();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            Textures.Load(this);

            foreach (Textures.Texture t in this.loadBuffer)
            {
                t.Sprite = Content.Load<Texture2D>(t.Name);
            }

            screenManager.LoadScreensContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();

            screenManager.UnloadScreensContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            screenManager.GetScreen(currentScreen).Update(gameTime);    
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.LightGray);

            screenManager.GetScreen(currentScreen).Draw(gameTime);
        }
    }
}
