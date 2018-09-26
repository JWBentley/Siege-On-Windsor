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
        Button testButton; //Play game button - TO BE REPLACED

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
            this.testButton = new Button(this.game.Content.Load<Texture2D>("Controls/Button"), this.game.Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(789, 352),
                Text = "Play Game"
            };

            this.testButton.Click += (o, i) => { this.game.screenManager.SwitchScreen(Screens.GAME); }; //Adds the on click event which switches the screen to the game screen
        }

        public override void Update(GameTime gameTime)
        {
            this.testButton.Update(gameTime); //Updates the button
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin(samplerState: SamplerState.PointClamp); //Starts the sprite batch using PointClamp to avoid any AA or blur

            this.spriteBatch.Draw(Textures.menuBackground.Sprite, new Rectangle(0,0, this.game._graphics.PreferredBackBufferWidth, this.game._graphics.PreferredBackBufferHeight), Color.White); //Draws the background fullscreen
            this.testButton.Draw(gameTime, this.spriteBatch); //Draws the button

            this.spriteBatch.End(); //Ends the sprite batch
        }

        public override void UnloadContent()
        {

        }
    }
}
