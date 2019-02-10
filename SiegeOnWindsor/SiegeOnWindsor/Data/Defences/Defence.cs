using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiegeOnWindsor.Graphics;
using SiegeOnWindsor.data;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Data.Tiles;

namespace SiegeOnWindsor.Data.Defences
{
    public abstract class Defence : IUpdate
    {
        /// <summary>
        /// List of all types of defence
        /// </summary>
        public static List<Defence> Types = new List<Defence>();

        /// <summary>
        /// Health of the defence, this is how much damage it can take before being destroyed
        /// </summary>
        public int Health = 1;

        /// <summary>
        /// The amount of damage the defence will deal to an enemy
        /// </summary>
        public int Damage = 0;
        /// <summary>
        /// The number of updates the defence must wait before it can attack again
        /// </summary>
        protected int AttackCooldown = 0;
        /// <summary>
        /// The number of updates since the previous attack
        /// </summary>
        protected int AttackProgress = 0;

        /// <summary>
        /// Total kills of a defence
        /// </summary>
        public int Kills { get; set; } = 0;

        /// <summary>
        /// The tile that the defence is placed on
        /// </summary>
        public Tile Tile;

        /// <summary>
        /// Default texture for the defence
        /// </summary>
        private Graphics.Graphics.Graphic graphic;

        /// <summary>
        /// Cost of placing a defence
        /// </summary>
        public int Cost { protected set; get; } 

        /// <summary>
        /// Creates a new defence
        /// </summary>
        public Defence()
        {

        }

        /// <summary>
        /// Creates a new defence
        /// </summary>
        /// <param name="g">Default Texture</param>
        public Defence(Graphics.Graphics.Graphic g)
        {
            this.graphic = g;
        }

        /// <summary>
        /// Updates the defence
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Deals damage to the defence
        /// </summary>
        /// <param name="damage">Amount of damage to be dealt</param>
        public virtual void DealDamage(int damage)
        {
            if (this.Health - damage > 0)
                this.Health -= damage;
            else
                this.Die();
        }

        /// <summary>
        /// Destroys the defence
        /// </summary>
        public virtual void Die()
        {
            this.Tile.World.TotalKills -= this.Kills;

            if (this.Tile.World.WaveController.target == this)
                this.Tile.World.WaveController.target = null;

            this.Tile.ClearDefence();
        }

        /// <summary>
        /// Gets the risk that the defence applies to the Tile that it is located on
        /// </summary>
        /// <returns>Risk as an integer</returns>
        public virtual int GetBaseRiskValue()
        {
            return (this.Health / 100) + (this.Kills / 5);
        }

        /// <summary>
        /// Gets a list of tiles that the defence impacts
        /// </summary>
        /// <returns>All tiles that the defence impacts as a list</returns>
        public virtual List<Vector2> GetImpactedTiles()
        {
            return new List<Vector2>();
        }

        /// <summary>
        /// Gets the impact that the defence has on a specific location
        /// </summary>
        /// <param name="location">The location being tested</param>
        /// <returns>Risk value as an integer</returns>
        public virtual int GetImpactOnTile(Vector2 location)
        {
            return 0; //Defaults to 0
        }

        /// <summary>
        /// Gets the texture of the defence
        /// </summary>
        /// <returns>Current texture of the defence</returns>
        public virtual Graphics.Graphics.Graphic GetGraphic()
        {
            return this.graphic ?? Graphics.Graphics.emptyTile; //Returns the graphic or emptyTile if the graphic is null
        }

        /// <summary>
        /// Returns the defence as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3}", 
                this.GetType().ToString(),
                String.Format("[{0}|{1}]", (int)this.Tile.Location.X, (int)this.Tile.Location.Y),
                this.Health.ToString(), 
                this.Kills.ToString());
        }

        /// <summary>
        /// Loads data from a file and applies it to the defence
        /// </summary>
        /// <param name="data"></param>
        public void LoadData(string[] data)
        {
            if (data.Length == 2)
            {
                this.Health = int.Parse(data[0]);
                this.Kills = int.Parse(data[1]);
            }
        }
    }
}
