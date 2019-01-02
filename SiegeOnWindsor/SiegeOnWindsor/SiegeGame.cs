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
        // Graphics //
        public GraphicsDeviceManager Graphics; //GraphicsDeviceManager

        // Screens //
        public ScreenManager ScreenManager; //Draws the screen corresponding to the currentScreen variable

        // Keyboard states //
        public static KeyboardState currentKeyboard;
        public static KeyboardState prevKeyboard;

        // Mouse states //
        public static MouseState currentMouse;
        public static MouseState prevMouse;

        public SiegeGame()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                IsFullScreen = false
            };
            Graphics.ApplyChanges(); //Sets up the GraphicsDeviceManager for a screen of size 1280x720

            Content.RootDirectory = "Content"; //Sets the content root directory (Content folder) which is where to look for texture files
            IsMouseVisible = true; //Makes the mouse visable
            Window.AllowUserResizing = false; //Prevents the user from resizing the window as the program does not yet support scaling to different resolutions (may or may not be added in the future)
            Window.Title = "Siege On Windsor Castle"; //Sets the title of the window
            TargetElapsedTime = TimeSpan.FromSeconds(1.0F / 100.0F); //Forces an update to be called 100 times per second
            IsFixedTimeStep = false;

            SiegeOnWindsor.Graphics.Graphics.Load(this); //Prompts the texture class to add all textures to the loadBuffer
            ScreenManager = new ScreenManager(GraphicsDevice, this); //Creates a screen manager for the game
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

            ScreenManager.InitializeScreens();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();



            ScreenManager.LoadScreensContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();

            ScreenManager.UnloadScreensContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            prevKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();

            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();

            ScreenManager.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.LightGray);

            ScreenManager.Draw(gameTime);
        }
    }
}
