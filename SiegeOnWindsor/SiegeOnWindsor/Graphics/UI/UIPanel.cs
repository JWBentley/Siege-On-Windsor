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
                if (child.isVisible)
                    child.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Updates all children
            foreach(UIComponent child in this.Children)
            {
                if (child.isActive)
                    child.Update(gameTime);
            }
            //Nothing to do here
        }

        /// <summary>
        /// Adds component as a child using the realtionship between the two
        /// </summary>
        /// <param name="component"></param>
        /// <param name="relation"></param>
        public void AddComponent(UIComponent component, Point relation)
        {
            this.AddComponent(component, new Rectangle(relation, component.Bounds.Size));
        }

        /// <summary>
        /// Adds component as a child using the realtionship between the two
        /// </summary>
        /// <param name="component"></param>
        /// <param name="relation"></param>
        public void AddComponent(UIComponent component, Rectangle relation)
        {
            component.Bounds = new Rectangle(this.Bounds.Location + relation.Location, relation.Size);
            this.AddComponent(component);
        }

        /// <summary>
        /// Adds component as a child
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(UIComponent component)
        {
            this.Children.Add(component);
        }
    }
}
