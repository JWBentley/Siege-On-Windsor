using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Tiles;
//using SiegeOnWindsor.Data.Enemies.Pathfinding;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Enemies
{
    public abstract class Enemy : IUpdate
    {
        public int Health = 1;

        public int Damage = 0;
        protected int AttackCooldown = 0;
        protected int AttackProgress = 0;

        public int Speed = -1;
        protected int MovementProgress = 0;

        public World World;
        public Vector2 Location;
        protected Stack<Vector2> Path;

        
        protected Textures.Texture graphic;

        public Enemy(World w)
        {
            this.World = w;
            this.World.Enemies.Add(this);
            //Console.WriteLine(this.World.GetCrownLocation().X);
            //Console.WriteLine(this.World.GetCrownLocation().Y);
        }

        public Enemy(Vector2 l)
        {
            this.Location = l;
        }

        public Enemy(Textures.Texture g)
        {
            this.graphic = g;
        }

        public virtual void Update(GameTime gameTime)
        {
            //Control of movement
            if (this.Speed != -1 && this.CanMove())
            {
                this.MovementProgress++;

                if (this.MovementProgress == this.Speed)
                {
                    this.MovementProgress = 0;
                    //Move to next tile is complete
                    Vector2 newLoc = this.Path.Pop();
                    Vector2 oldLoc = this.Location;
                    this.Location = newLoc;
                    this.World.GetTileAt((int)oldLoc.X, (int)oldLoc.Y).Enemies.Remove(this);
                    this.World.GetTileAt((int)newLoc.X, (int)newLoc.Y).Enemies.Add(this);
                }
            }
            else if(this.Damage > 0 && this.Path.Count > 0 && this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).Defence != null)
            {
                if (this.AttackProgress >= this.AttackCooldown)
                {
                    this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).Defence.DealDamage(this.Damage);
                    this.AttackProgress = 0;
                }
                else
                    this.AttackProgress++;
            }

        }

        public void DealDamage(int damage)
        {
            if (this.Health - damage > 0)
                this.Health -= damage;
            else
                this.Die();
        }

        public virtual void Die()
        {
            this.World.GetTileAt(this.Location).Enemies.Remove(this);
            this.World.Enemies.Remove(this);
        }


        private bool CanMove()
        {
            return this.Path.Count <= 0 ? false : this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).Defence == null;
        }

        public void UpdatePath(Vector2 goal)
        {
            this.Path = this.World.aStar.Run(this.Location, goal);
            this.Path.Pop(); //removes current location from stack
            //Console.WriteLine("peeking {0},{1}", this.Path.Peek().X, this.Path.Peek().X);
        }

        public virtual Vector2 GetActualLocation()
        {
            //Here I had a problem where (progress / speed) returned zero, caused by them being ints FIX: cast to float
            if (this.CanMove())
                return new Vector2(this.Location.X + (Math.Sign(this.Path.Peek().X - this.Location.X)) * ((float)this.MovementProgress / (float)this.Speed),
                                   this.Location.Y + (Math.Sign(this.Path.Peek().Y - this.Location.Y)) * ((float)this.MovementProgress / (float)this.Speed));
            else
                return this.Location;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
