using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Data.Defences
{
    public class GuardDef : Defence
    {
        /// <summary>
        /// Creates a new guard enemy
        /// </summary>
        public GuardDef() : base(Graphics.Graphics.guardDef)
        {
            this.Health = 500; //Sets the health to 500
            this.Damage = 50; //Sets the damage to 70

            this.AttackCooldown = 50; //Sets the cooldown period to 50

        }


        public override void Update(GameTime gameTime)
        {
            if (this.AttackProgress >= this.AttackCooldown) //Cooldown period is over
            {
                //Attacks one enemy on one of the adjacent tiles
                if (this.Tile.World.GetTileAt((int)this.Tile.Location.X + 1, (int)this.Tile.Location.Y).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X + 1, (int)this.Tile.Location.Y).Enemies.First().DealDamage(this.Damage);
                else if (this.Tile.World.GetTileAt((int)this.Tile.Location.X - 1, (int)this.Tile.Location.Y).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X - 1, (int)this.Tile.Location.Y).Enemies.First().DealDamage(this.Damage);
                else if (this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y + 1).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y + 1).Enemies.First().DealDamage(this.Damage);
                else if (this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y - 1).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y - 1).Enemies.First().DealDamage(this.Damage);

                this.AttackProgress = 0; //Resets the cooldown
            }
            else //Cooldown is not over
                this.AttackProgress++; //Cooldown progress is increased
        }


        public override List<Vector2> GetImpactedTiles()
        {
            //Retuns a list of all adjacent tiles
            return new List<Vector2>()
            {
                new Vector2((int)this.Tile.Location.X + 1, (int)this.Tile.Location.Y),
                new Vector2((int)this.Tile.Location.X - 1, (int)this.Tile.Location.Y),
                new Vector2((int)this.Tile.Location.X, (int)this.Tile.Location.Y + 1),
                new Vector2((int)this.Tile.Location.X, (int)this.Tile.Location.Y - 1),

            };
        }

        public override int GetImpactOnTile(Vector2 location)
        {
            //Console.WriteLine((int)((float)(this.Damage / this.AttackCooldown) * 10F));
            if (this.Tile.World.GetCrownLocation().Equals(location)) //If the location is of the crown 0 is returned
                return 0;
            else return (int)((float)this.Damage / (float)this.AttackCooldown * 10F); //Returns an impact using the stats of the guard
        }
    }
}
