using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Screens
{
    public abstract class Screen
    {
        private GraphicsDevice graphics;

        public Screen(GraphicsDevice graphicsDevice)
        {
            this.graphics = graphicsDevice;
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        
    }
}
