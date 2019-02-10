using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiegeOnWindsor.Graphics.Graphics;

namespace SiegeOnWindsor.Data.Enemies
{
    public class ShieldEnemy : Enemy
    {
        public ShieldEnemy() : base()
        {
            //Sets key variables
            this.Health = 700;
            this.Speed = 100;
            this.Damage = 1;
            this.AttackCooldown = 20;

            this.Cost = 150;
        }

        public override Graphic GetGraphic()
        {
            return this.Path.Count > 0 ? this.Location.X > this.Path.Peek().X ? Graphics.Graphics.shieldEnemy_Left : Graphics.Graphics.shieldEnemy_Right : Graphics.Graphics.shieldEnemy_Left;
            //Returns graphic so the enemy is looking in the correct location
        }
    }
}
