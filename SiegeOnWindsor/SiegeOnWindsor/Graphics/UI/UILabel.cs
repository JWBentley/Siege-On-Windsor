﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SiegeOnWindsor.Graphics.UI
{
    public class UILabel : UIComponent
    {
        private string text; //Text of the label
        private Graphics.Font font; //Font of the text

        public delegate string TextGetter();
        public TextGetter textGetter { set; get; }

        public string Text { get => text; set => text = value; }
        public Color Color { get; internal set; }

        /*
        public UILabel(string text, Graphics.Font font, Color color, Rectangle rectangle) : base(rectangle)
        {
            this.Text = text;
            this.font = font;
            this.color = color;
        }*/

        public UILabel(string text, Graphics.Font font, Color color) : base(new Rectangle(new Point(0,0), font.Object.MeasureString(text).ToPoint()))
        {
            this.font = font;
            this.Color = color;
            this.Text = text;
        }

        public UILabel(string text, Graphics.Font font, Color color, Point point) : base(new Rectangle(point, font.Object.MeasureString(text).ToPoint()))
        {
            this.font = font;
            this.Color = color;
            this.Text = text;
        }

        public UILabel(TextGetter text, Graphics.Font font, Color color, Rectangle rectangle) : base(rectangle)
        {
            this.textGetter = text;
            this.font = font;
            this.Color = color;
            this.Text = "";
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draws the string
            spriteBatch.DrawString(this.font.Object, this.Text, new Vector2(this.Bounds.X, this.Bounds.Y), this.Color);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.textGetter != null)
                this.Text = this.textGetter();
        }
    }
}
