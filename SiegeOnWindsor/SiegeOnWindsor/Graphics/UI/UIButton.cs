using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Graphics.UI
{
    public class UIButton : UIComponent
    {
        private SpriteFont font; //Font for the text of the button
        private bool isHovering; //bool representing if the mouse is hovering over the button
        private Texture2D texture; //Texture of the button

        public event EventHandler Click; //Event handler for when the button is clicked

        //public bool Clicked { get; private set; } //bool representing if the button has been clicked or not
        public Color PenColor { get; set; } //Colour for the text
        public string Text { get; set; } //Text of the button

        public UIButton(Texture2D t, SpriteFont f, string s)
        {
            this.texture = t;
            this.font = f;
            this.Text = s;
            this.Bounds = new Rectangle(0, 0, (int)(this.font.MeasureString(Text).X * 1.1), (int)(this.font.MeasureString(Text).Y * 1.1));
            this.PenColor = Color.Black;



            this.Click += (o, i) => { this.OnClick(); }; //Sets the OnClick method to run when button is clicked
        }

        public UIButton(Texture2D t, SpriteFont f, string s, Vector2 l)
        {
            this.texture = t;
            this.font = f;
            this.Text = s;
            this.Bounds = new Rectangle((int)l.X, (int)l.Y, (int)(this.font.MeasureString(Text).X * 1.1), (int)(this.font.MeasureString(Text).Y * 1.1));
            this.PenColor = Color.Black;



            this.Click += (o, i) => { this.OnClick(); }; //Sets the OnClick method to run when button is clicked
        }

        public virtual void OnClick()
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = new Color(165, 133, 88); //Brown tint

            if (this.isHovering) //If the mouse if hovering over the button a grey tint is added
                color = Color.Gray;

            spriteBatch.Draw(this.texture, this.Bounds, color); //Draws the button texture

            if (!string.IsNullOrEmpty(this.Text)) //If the button has text
            {
                var x = (this.Bounds.X + (this.Bounds.Width / 2)) - (this.font.MeasureString(Text).X / 2); //Calculates the correct x pos of the text
                var y = (this.Bounds.Y + (this.Bounds.Height / 2)) - (this.font.MeasureString(Text).Y / 2); //Calculates the correct y pos of the text

                spriteBatch.DrawString(this.font, this.Text, new Vector2(x, y), this.PenColor); //Draws the text
            }
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle mouseRectangle = new Rectangle(SiegeGame.currentMouse.X, SiegeGame.currentMouse.Y, 1, 1); //Pos of the current mouse

            this.isHovering = false; //Sets hovering to false

            if (mouseRectangle.Intersects(this.Bounds)) //If the mouse pos is within the buttons boundaries
            {
                this.isHovering = true; //Hovering is set to true

                if (SiegeGame.currentMouse.LeftButton == ButtonState.Released && SiegeGame.prevMouse.LeftButton == ButtonState.Pressed) //If left click has been pressed and then released
                {
                    Click?.Invoke(this, new EventArgs()); //Click event is fired
                }
            }
        }
    }
}
