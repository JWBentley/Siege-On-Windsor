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

        public Vector2 Location;
        protected Stack<Vector2> Path;

        
        private Textures.Texture graphic;

        public Enemy()
        {

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
