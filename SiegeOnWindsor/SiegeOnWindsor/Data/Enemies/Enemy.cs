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
        protected int Health = 1;

        protected int Damage = -1;
        protected int AttackCooldown = 0;

        protected int Speed = -1;
        protected int MovementProgress = 0;

        public World World;
        public Vector2 Location;
        protected Stack<Vector2> Path;

        
        private Textures.Texture graphic;

        public Enemy(World w)
        {
            this.World = w;
            this.World.enemies.Add(this);
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
                    this.World.GetTileAt((int)oldLoc.X, (int)oldLoc.Y).enemies.Remove(this);
                    this.World.GetTileAt((int)newLoc.X, (int)newLoc.Y).enemies.Add(this);
                }
            }

        }

        private bool CanMove()
        {
            return this.Path.Count <= 0 ? false : this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).defence == null;
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
