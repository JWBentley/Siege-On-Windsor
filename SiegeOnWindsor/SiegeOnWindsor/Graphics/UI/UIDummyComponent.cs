
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SiegeOnWindsor.Graphics.UI
{
    public class UIDummyComponent : UIComponent
    {
        public UIDummyComponent(Rectangle rectangle) : base(rectangle)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Console.WriteLine("Drawing component at ({0},{1}) that is size {2}x{3}", 
                this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height); //Test

        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("Updating component at ({0},{1}) that is size {2}x{3}",
                this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height); //Test
        }
    }
}
