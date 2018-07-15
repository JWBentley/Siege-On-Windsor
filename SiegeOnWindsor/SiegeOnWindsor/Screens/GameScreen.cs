using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;

namespace SiegeOnWindsor.Screens
{
    public class GameScreen : IScreen
    {
        SiegeGame game;

        World world;

        public GameScreen(SiegeGame g)
        {
            this.game = g;
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            this.world = new World();
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            this.world.Update();
        }
    }
}
