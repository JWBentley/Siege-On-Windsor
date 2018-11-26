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
        public MenuScreen(SiegeGame game) : base(game)
        {

        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice); //Starts up the sprite batch using the game's graphics device
            this.uiController = new UIController(game.Graphics.PreferredBackBufferWidth, game.Graphics.PreferredBackBufferHeight, this.spriteBatch); //Creates UI controller

            
            UIButton newGameButton = new UIButton(this.game.Content.Load<Texture2D>("UI/Buttons/blankButton"),
                                                Graphics.Graphics.arial32.Object,
                                                "New game",
                                                new Vector2(805, 300));
            newGameButton.Click += (o, i) => { this.game.ScreenManager.SwitchScreen(Screens.GAME); };

            this.uiController.Components.Add(newGameButton);

            UIButton loadGameButton = new UIButton(this.game.Content.Load<Texture2D>("UI/Buttons/blankButton"),
                                                 Graphics.Graphics.arial32.Object,
                                                "Load game",
                                                new Vector2(798, 360));
            loadGameButton.Click += (o, i) => { };

            this.uiController.Components.Add(loadGameButton);

            UIButton exitGameButton = new UIButton(this.game.Content.Load<Texture2D>("UI/Buttons/blankButton"),
                                                 Graphics.Graphics.arial32.Object,
                                                "Quit Game",
                                                new Vector2(807, 420));
            exitGameButton.Click += (o, i) => { this.game.Exit(); };

            this.uiController.Components.Add(exitGameButton);

            //TESTING UI CONTROLLER
            this.uiController.Components.Add(new UIDummyComponent(new Rectangle(0, 0, 5, 5)));
            this.uiController.Components.Add(new UIDummyComponent(new Rectangle(18,67, 50, 50)));
            this.uiController.Components.Add(new UIDummyComponent(new Rectangle(1000, 700, 4, 20)));
        }

        public override void Update(GameTime gameTime)
        {
            this.uiController.Update(gameTime); //Updates UI
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();

            this.spriteBatch.Draw(Graphics.Graphics.menuBackground.Object, new Rectangle(0,0, this.game.Graphics.PreferredBackBufferWidth, this.game.Graphics.PreferredBackBufferHeight), Color.White); //Draws the background fullscreen

            this.uiController.Draw(gameTime); //Draws UI

            this.spriteBatch.End(); //Ends the sprite batch
        }

        public override void UnloadContent()
        {

        }
    }
}
