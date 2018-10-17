using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiegeOnWindsor.Graphics.UI;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Screens
{
    public class MenuScreen : Screen
    {
        Button newGameButton;
        Button loadGameButton;
        Button exitGameButton;

        public MenuScreen(SiegeGame game) : base(game)
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice); //Starts up the sprite batch using the game's graphics device

            //Creates a new button by loading the sprite and font
            this.newGameButton = new Button(this.game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(805, 300),
                Text = "New Game"
            };

            this.loadGameButton = new Button(this.game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(798, 360),
                Text = "Load Game"
            };

            this.exitGameButton = new Button(this.game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(807, 420),
                Text = "Quit Game"
            };

            this.newGameButton.Click += (o, i) => { this.game.ScreenManager.SwitchScreen(Screens.GAME); }; //Adds the on click event which switches the screen to the game screen

            this.exitGameButton.Click += (o, i) => { this.game.Exit(); };
        }

        public override void Update(GameTime gameTime)
        {
            this.newGameButton.Update(gameTime); //Updates the button
            this.loadGameButton.Update(gameTime); //Updates the button
            this.exitGameButton.Update(gameTime); //Updates the button
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();

            this.spriteBatch.Draw(Textures.menuBackground.Sprite, new Rectangle(0,0, this.game.Graphics.PreferredBackBufferWidth, this.game.Graphics.PreferredBackBufferHeight), Color.White); //Draws the background fullscreen
            this.newGameButton.Draw(gameTime, this.spriteBatch); //Draws the button
            this.loadGameButton.Draw(gameTime, this.spriteBatch); //Draws the button
            this.exitGameButton.Draw(gameTime, this.spriteBatch); //Draws the button

            this.spriteBatch.End(); //Ends the sprite batch
        }

        public override void UnloadContent()
        {

        }
    }
}
