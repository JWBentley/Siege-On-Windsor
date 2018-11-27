using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Graphics.UI
{
    /// <summary>
    /// This class holds all of the components for a UI
    /// </summary>
    public class UIController
    {
        /// <summary>
        /// Width and height ofthe ui in pixels
        /// </summary>
        private int width, height;

        /// <summary>
        /// List of UI components
        /// </summary>
        public readonly List<UIComponent> Components;

        /// <summary>
        /// Spritebatch used to draw graphics
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Creates new UI
        /// </summary>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        public UIController(int w, int h, SpriteBatch sb)
        {
            this.width = w; //Sets the width
            this.height = h; //Sets the height
            this.spriteBatch = sb; //Sets the sprite batch
            this.Components = new List<UIComponent>(); //Creates a new list of components
        }

        /// <summary>
        /// Update function for the user interface
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach(UIComponent component in this.Components)
            {
                component.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw function for the user interface
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            foreach (UIComponent component in this.Components)
            {
                component.Draw(gameTime, this.spriteBatch);
            }
        }
    }
}
