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
    /// <summary>
    /// This class creates a simple button that can be used whilst making a prototype of the game
    /// </summary>
    public class Button
    {
        private MouseState currentMouse; //Holds the current mouse state
        private SpriteFont font; //Font for the text of the button
        private bool isHovering; //bool representing if the mouse is hovering over the button
        private MouseState previousMouse; //Previous mouse state
        private Texture2D texture; //Texture of the button

        public event EventHandler Click; //Event handler for when the button is clicked

        public bool Clicked { get; private set; } //bool representing if the button has been clicked or not
        public Color PenColor { get; set; } //Colour for the text
        public Vector2 Position { get; set; } //Position of the button
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(this.font.MeasureString(Text).X * 1.1), (int)(this.font.MeasureString(Text).Y * 1.1));
            }
        } //The space which the button occupies 
        public string Text { get; set; } //Text of the button

        public Button(Texture2D t, SpriteFont f)
        {
            this.texture = t;
            this.font = f;

            this.PenColor = Color.Black;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (this.isHovering) //If the mouse if hovering over the button a grey tint is added
                color = Color.Gray;

            spriteBatch.Draw(this.texture, Rectangle, color); //Draws the button texture

            if (!string.IsNullOrEmpty(this.Text)) //If the button has text
            {
                var x = (this.Rectangle.X + (this.Rectangle.Width / 2)) - (this.font.MeasureString(Text).X / 2); //Calculates the correct x pos of the text
                var y = (this.Rectangle.Y + (this.Rectangle.Height / 2)) - (this.font.MeasureString(Text).Y / 2); //Calculates the correct y pos of the text

                spriteBatch.DrawString(this.font, this.Text, new Vector2(x, y), this.PenColor); //Draws the text
            }
        }

        public void Update(GameTime gameTime)
        {
            this.previousMouse = this.currentMouse; //Updates previousMouse
            this.currentMouse = Mouse.GetState(); //Updates currentMouse

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1); //Pos of the current mouse

            this.isHovering = false; //Sets hovering to false

            if (mouseRectangle.Intersects(this.Rectangle)) //If the mouse pos is within the buttons boundaries
            {
                this.isHovering = true; //Hovering is set to true

                if (this.currentMouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed) //If left click has been pressed and then released
                {
                    Click?.Invoke(this, new EventArgs()); //Click event is fired
                }
            }
        }
    }
}
