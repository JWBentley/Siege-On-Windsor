using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Tiles
{
    public class Tile : IUpdate
    {
        /// <summary>
        /// List of enemies currently on the tile
        /// </summary>
        public List<Enemy> Enemies;
        /// <summary>
        /// Defence deployed on the tile
        /// </summary>
        public Defence Defence;

        /// <summary>
        /// Reference to the world
        /// </summary>
        public World World;
        /// <summary>
        /// Location within the world
        /// </summary>
        public Vector2 Location;

        public Tile(World w, Vector2 l) : this(w, l, null)
        {
            
        }

        public Tile(World w, Vector2 l, Defence d)
        {
            this.World = w;

            this.Location = l;

            this.Defence = d;

            if(this.Defence != null)
                this.Defence.Tile = this; //If there is a defence then the tile of the defence is set

            this.Enemies = new List<Enemy>(); //Creates a new list of enemies
        }

        public void Update(GameTime gameTime)
        {
            if (this.Defence != null)
                this.Defence.Update(gameTime); //Updates the defence

            //foreach (Enemy enemy in this.enemies)
            //{
            //    enemy.Update(gameTime);
            //}
        }

        /// <summary>
        /// Deploys a defence on the tile
        /// </summary>
        /// <param name="def">Defence to deploy</param>
        public void AddDefence(Defence def)
        {
            this.Defence = def; //Updates defence
            def.Tile = this; //Sets tile
            this.World.UpdateRiskMap(); //Updates risk map
        }

        /// <summary>
        /// Removes a defence from the tile
        /// </summary>
        public void ClearDefence()
        {
            this.Defence = null; //Sets defence to null
            this.World.UpdateRiskMap(); //Updates the risk map
        }

        /// <summary>
        /// Gets the risk value of moving to the tile based on what is on top of it
        /// </summary>
        /// <returns>int risk value</returns>
        public virtual int GetBaseRiskValue()
        {
            int risk = 1; //Defaults to one

            if (this.Defence != null)
                risk += this.Defence.GetBaseRiskValue(); //Adds defence risk

            return risk; //Returns 
        }

        /// <summary>
        /// Gets the graphic of the tile
        /// </summary>
        /// <returns></returns>
        public virtual Graphics.Graphics.Graphic GetGraphic()
        {
            return Graphics.Graphics.emptyTile;
        }
    }
}
