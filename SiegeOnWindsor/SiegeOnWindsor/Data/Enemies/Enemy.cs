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
            if (this.Speed != -1)
            {
                this.MovementProgress++;

                if (this.MovementProgress == this.Speed)
                {
                    this.MovementProgress = 0;
                    //Move to next tile is complete
                }
            }

        }

        public void UpdatePath()
        {
            this.Path = this.World.aStar.Run(this.Location, this.World.GetCrownLocation());

            foreach (Vector2 loc in this.Path)
            {
                Console.WriteLine("Path:{0},{1}", loc.X, loc.Y);
            }
        }

        public virtual Vector2 GetActualLocation()
        {
            return this.Location;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
