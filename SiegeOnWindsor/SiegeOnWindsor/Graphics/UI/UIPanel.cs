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
    /// UI element that is a single image or box which can contain chiild elements
    /// </summary>
    public class UIPanel : UIComponent
    {
        /// <summary>
        /// Image for the panel
        /// </summary>
        private Graphics.Graphic panelImage;
        /// <summary>
        /// Any components contained within the panel
        /// </summary>
        public List<UIComponent> Children;

        public Color Color = Color.White;

        public UIPanel(Graphics.Graphic graphic, Rectangle rectangle) : base(rectangle)
        {
            this.panelImage = graphic; //Sets the image for the panel
            this.Children = new List<UIComponent>(); //Creates a new list to store any children
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.panelImage.Object, this.Bounds, this.Color); //By default draws panel image

            //Draws all children
            foreach (UIComponent child in this.Children)
            {
                child.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Updates all children
            foreach(UIComponent child in this.Children)
            {
                child.Update(gameTime);
            }
            //Nothing to do here
        }
    }
}
