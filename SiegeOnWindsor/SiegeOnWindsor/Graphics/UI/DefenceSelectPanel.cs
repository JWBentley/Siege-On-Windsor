using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Data.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Graphics.UI
{
    /// <summary>
    /// Specific panel that displays all defences for the player to select from
    /// </summary>
    public class DefenceSelectPanel : UIPanel
    {
        private MouseState currentMouse; //Holds the current mouse state
        private MouseState previousMouse; //Previous mouse state

        /// <summary>
        /// Used to convert to different sizes
        /// </summary>
        //private float scale;

        /// <summary>
        /// Current page of defences
        /// </summary>
        private int page;

        /// <summary>
        /// List of defences
        /// </summary>
        private List<Defence> defences;

        public Defence SelectedDefence = null;

        private World world; //ref to world

        public DefenceSelectPanel(World w,Rectangle bounds, List<Defence> defences) : base(Graphics.defencePanelUI, bounds)
        {
            //this.scale = bounds.Height / Graphics.defencePanelUI.Object.Height;
            this.page = 1;
            this.defences = defences;

            this.world = w;

            this.CreatePage();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.previousMouse = this.currentMouse; //Upates prev mouse
            this.currentMouse = Mouse.GetState(); //Updates current mouse
            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1); //Pos of the current mouse


            if (this.currentMouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed)
            {
                //Code for dropping def
                if (this.SelectedDefence != null)
                {
                    this.world.PlaceDefence(this.SelectedDefence);
                    this.SelectedDefence = null;
                }
            }
            else if(this.currentMouse.LeftButton == ButtonState.Pressed && this.previousMouse.LeftButton == ButtonState.Released)
            {
                //Code for picking up a defence
                foreach (DefencePanel defence in this.Children)
                {
                    if (mouseRectangle.Intersects(defence.Bounds))
                        this.SelectedDefence = defence.Defence; //Sets the held defence
                }
            }

            //Debugging
            Console.WriteLine(this.SelectedDefence);
        }


        /// <summary>
        /// Creates children to match page
        /// </summary>
        private void CreatePage()
        {
            this.Children.Clear(); //Clears children

            for(int i = (page-1)*8; i < ((page - 1) * 8) + 8; i++) //Loops through each defence on the page
            {
                int visualIndex = i - (page - 1) * 8;
                if(i < this.defences.Count && this.defences[i] != null) //Checks defence is not null
                this.Children.Add(new DefencePanel(this.defences[i],
                    new Rectangle(this.Bounds.X + 14 + ((visualIndex % 2) * 94),
                    this.Bounds.Y + 14 + (94 * (int)Math.Floor((float)visualIndex / 2F)),
                    80,
                    80)));
            }
        }

        /// <summary>
        /// Moves to next page of defences when possible
        /// </summary>
        public void NextPage()
        {
            if (this.defences.Count / 8 > this.page + 1) //Validation
            {
                this.page++; //Next page
                this.CreatePage(); //Recreates page objects
            }
        }

        /// <summary>
        /// Moves to previous page of defences when possible
        /// </summary>
        public void PrevPage()
        {
            if (this.page >= 2) //Validation
            {
                this.page--; //Prev page
                this.CreatePage(); //Recreates page objects
            }
        }

        public class DefencePanel : UIPanel
        {
            public Defence Defence;

            public DefencePanel(Defence d, Rectangle rectangle) : base(d.GetGraphic(), rectangle)
            {
                this.Defence = d;
            }
        }
    }
}
