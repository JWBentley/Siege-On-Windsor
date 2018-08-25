using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiegeOnWindsor.Graphics.Textures;

namespace SiegeOnWindsor.Data.Enemies
{
    public class PeasantEnemy : Enemy
    {
        private Animation animation;
        private int x = 0;
        private int limit;


        public PeasantEnemy(int a) : base()
        {
            this.Speed = 100;
            this.animation = new Animation(Textures.testAnimation);
            this.limit = a;
        }

        public PeasantEnemy(Vector2 location, int a) : base(location)
        {
            this.Speed = 100;
            this.animation = new Animation(Textures.testAnimation);
            this.limit = a;
        }

        public override void Update(GameTime gameTime)
        {
            this.x++;
            if (x > this.limit)
            {
                this.animation.NextFrame();
                this.x = 0;
            }

            base.Update(gameTime);

            //Console.WriteLine(this.MovementProgress);
        }

        public override Textures.Texture GetGraphic()
        {
            return this.animation.GetFrame();
        }
    }
}
