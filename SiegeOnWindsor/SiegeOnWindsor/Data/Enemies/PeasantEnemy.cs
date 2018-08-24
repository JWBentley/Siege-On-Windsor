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
        
        public PeasantEnemy(int a)
        {
            this.animation = new Animation(Textures.testAnimation);
            this.limit = a;
        }

        public override void Update()
        {
            this.x++;
            if (x > this.limit)
            {
                this.animation.NextFrame();
                this.x = 0;
            }
        }

        public override Textures.Texture GetGraphic()
        {
            return this.animation.GetFrame();
        }
    }
}
