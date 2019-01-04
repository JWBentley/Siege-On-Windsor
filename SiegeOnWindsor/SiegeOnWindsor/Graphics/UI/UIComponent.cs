using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SiegeOnWindsor.Graphics.UI
{
    /// <summary>
    /// An element that is drawn onto the screen as part of the user interface
    /// </summary>
    public abstract class UIComponent
    {
        /// <summary>
        /// Area of screen that is taken up by the component
        /// </summary>
        public Rectangle Bounds;

        /// <summary>
        /// Should the component be drawn
        /// </summary>
        public bool isVisible { get; set; } = true;

        /// <summary>
        /// Should the component update
        /// </summary>
        public bool isActive { get; set; } = true;


        public UIComponent() : this(new Rectangle(0,0,0,0))
        {
            
        }


        public UIComponent(Rectangle rectangle)
        {
            this.Bounds = rectangle;
            this.isActive = true;
            this.isVisible = true;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);


        public void Toggle()
        {
            this.isActive = !this.isActive;
            this.isVisible = !this.isVisible;
        }
    }
}
