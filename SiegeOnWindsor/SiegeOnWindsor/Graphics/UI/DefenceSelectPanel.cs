using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private World world; //ref to world

        public Defence SelectedDefence { get; private set; } = null;

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

            if (!this.world.isPaused)
            {

                Rectangle mouseRectangle = new Rectangle(SiegeGame.currentMouse.X, SiegeGame.currentMouse.Y, 1, 1); //Pos of the current mouse


                if (SiegeGame.currentMouse.LeftButton == ButtonState.Released && SiegeGame.prevMouse.LeftButton == ButtonState.Pressed)
                {
                    //Code for dropping def
                    if (this.SelectedDefence != null)
                    {
                        //Gets grid location, however the method should return false if this is not valid so nothing should happen if the user attempts to place the defence somewhere that isn't a tile
                        if (this.world.GetLocationFromScreen(Mouse.GetState().Position.ToVector2(), out Vector2 gridLoc))
                        {
                            //Gets the tile from the location using a floor method
                            Tile tile = this.world.GetTileAt(gridLoc);

                            //If a defence can be placed on the tile then the selected defence will be placed
                            if (!(tile is SpawnTile) && tile.Defence == null)
                            {
                                tile.AddDefence((Defence)Activator.CreateInstance(this.SelectedDefence.GetType()));

                                //Take money off player
                            }
                        }
                        this.SelectedDefence = null;
                    }
                }
                else if (SiegeGame.currentMouse.LeftButton == ButtonState.Pressed && SiegeGame.prevMouse.LeftButton == ButtonState.Released)
                {
                    //Code for picking up a defence
                    foreach (DefencePanel defence in this.Children)
                    {
                        if (mouseRectangle.Intersects(defence.Bounds) && (defence.Defence.Cost < this.world.Money))
                            this.SelectedDefence = defence.Defence; //Sets the held defence
                    }
                }

                //Debugging
                Console.WriteLine(this.SelectedDefence);
            }
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
                this.Children.Add(new DefencePanel(this.defences[i], this.world,
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
            private World world;

            public DefencePanel(Defence d, World w,  Rectangle rectangle) : base(d.GetGraphic(), rectangle)
            {
                this.Defence = d;
                this.world = w;
            }

            public override void Update(GameTime gameTime)
            {

                base.Update(gameTime);

                //Blacks out sprite if the player cannot afford the defence
                if (this.Defence.Cost > this.world.Money)
                    this.Color = Color.Black;
                else
                    this.Color = Color.White;

            }
        }
    }
}
