using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SiegeOnWindsor.Graphics.UI
{
    public class Button : Component
    {
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;

        public event EventHandler Click;

        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(this.font.MeasureString(Text).X * 1.1), (int)(this.font.MeasureString(Text).Y * 1.1));
            }
        }
        public string Text { get; set; }

        public Button(Texture2D t, SpriteFont f)
        {
            this.texture = t;
            this.font = f;

            this.PenColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (this.isHovering)
                color = Color.Gray;

            spriteBatch.Draw(this.texture, Rectangle, color);

            if (!string.IsNullOrEmpty(this.Text))
            {
                var x = (this.Rectangle.X + (this.Rectangle.Width / 2)) - (this.font.MeasureString(Text).X / 2);
                var y = (this.Rectangle.Y + (this.Rectangle.Height / 2)) - (this.font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(this.font, this.Text, new Vector2(x, y), this.PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.previousMouse = this.currentMouse;
            this.currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(this.Rectangle))
                this.isHovering = true;

            if(this.currentMouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, new EventArgs());
            }
        }
    }
}
