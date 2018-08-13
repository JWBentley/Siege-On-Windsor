using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiegeOnWindsor.Graphics.UI;

namespace SiegeOnWindsor.Screens
{
    public class MenuScreen : Screen
    {
        SiegeGame game;
        Button testButton;

        GraphicsDevice graphicsDevice;
        SpriteBatch menuBatch;

        public MenuScreen(GraphicsDevice graphicsDevice, SiegeGame game) : base(graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.game = game;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            this.menuBatch = new SpriteBatch(this.graphicsDevice);

            this.testButton = new Button(this.game.Content.Load<Texture2D>("Controls/Button"), this.game.Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(200, 200),
                Text = "Play Game"
            };

            this.testButton.Click += TestClick;
        }

        public override void Update(GameTime gameTime)
        {
            this.testButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.menuBatch.Begin(samplerState: SamplerState.PointClamp);

            this.testButton.Draw(gameTime, this.menuBatch);

            this.menuBatch.End();
        }

        private void TestClick(object sender, EventArgs e)
        {
            this.game.currentScreen = Screens.GAME;
        }

        public override void UnloadContent()
        {
        }


    }
}
