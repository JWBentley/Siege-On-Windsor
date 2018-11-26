using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SiegeOnWindsor.Graphics.UI
{
    public abstract class UIComponent
    {
        /// <summary>
        /// Area of screen that is taken up by the component
        /// </summary>
        public Rectangle Bounds;

        public UIComponent()
        {

        }


        public UIComponent(Rectangle rectangle)
        {
            this.Bounds = rectangle;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
