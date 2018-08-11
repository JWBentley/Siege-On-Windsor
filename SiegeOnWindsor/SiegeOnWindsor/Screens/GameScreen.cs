using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using Microsoft.Xna.Framework.Graphics;

namespace SiegeOnWindsor.Screens
{
    public class GameScreen : Screen
    {
        SiegeGame game;

        World world;

        public GameScreen(GraphicsDevice graphicsDevice, SiegeGame g) : base(graphicsDevice)
        {
            this.game = g;
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override void Initialize()
        {
            this.world = new World();
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.world.Update();
        }
    }
}
