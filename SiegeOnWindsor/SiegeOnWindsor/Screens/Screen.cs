using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiegeOnWindsor.Graphics.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Screens
{
    /// <summary>
    /// A set of graphics and data objects that make up a scene in the game
    /// </summary>
    public abstract class Screen
    {
        protected SiegeGame game; //Local copy of the game
        protected SpriteBatch spriteBatch; //Sprite batch for drawing
        protected UIController uiController; //Controller for all UI elements

        public Screen(SiegeGame g)
        {
            this.game = g;
        }

        public abstract void Initialize(); //Set up screen code
        public abstract void LoadContent(); //Load screen visuals
        public abstract void UnloadContent(); //Unload screen visuals
        public abstract void Update(GameTime gameTime); //Update screen data
        public abstract void Draw(GameTime gameTime); //Draw updated graphics
        
    }
}
