using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SiegeOnWindsor.Data.Defences
{
    public class ArcherDef : Defence
    {
        public ArcherDef() : base(Graphics.Graphics.archerDef)
        {
            this.Health = 350; //Sets the health
            this.Damage = 25; //Sets the damage to 70

            this.AttackCooldown = 25; //Sets the cooldown period to 50

            this.Cost = 750;
        }


        public override void Update(GameTime gameTime)
        {
            if (this.AttackProgress >= this.AttackCooldown) //Cooldown period is over
            {
                //Attacks one enemy on one of the adjacent tiles
                if (this.Tile.World.GetTileAt((int)this.Tile.Location.X + 1, (int)this.Tile.Location.Y).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X + 1, (int)this.Tile.Location.Y).Enemies.First().DealDamage(this.Damage, this);
                else if (this.Tile.World.GetTileAt((int)this.Tile.Location.X - 1, (int)this.Tile.Location.Y).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X - 1, (int)this.Tile.Location.Y).Enemies.First().DealDamage(this.Damage, this);
                else if (this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y + 1).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y + 1).Enemies.First().DealDamage(this.Damage, this);
                else if (this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y - 1).Enemies.Count > 0)
                    this.Tile.World.GetTileAt((int)this.Tile.Location.X, (int)this.Tile.Location.Y - 1).Enemies.First().DealDamage(this.Damage, this);

                this.AttackProgress = 0; //Resets the cooldown
            }
            else //Cooldown is not over
                this.AttackProgress++; //Cooldown progress is increased
        }
    }
}
