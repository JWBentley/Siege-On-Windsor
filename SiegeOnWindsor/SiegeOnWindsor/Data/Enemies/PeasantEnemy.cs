using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiegeOnWindsor.Graphics.Graphics;

namespace SiegeOnWindsor.Data.Enemies
{
    public class PeasantEnemy : Enemy
    {
        public PeasantEnemy(World w) : base(w)
        {
            //Sets key variables
            this.Health = 200;
            this.Speed = 70;
            this.Damage = 10;
            this.AttackCooldown = 20;
        }

        public override Graphic GetGraphic()
        {
            return this.Path.Count > 0 ? this.Location.X > this. Path.Peek().X ? Graphics.Graphics.peasantEnemy_Left : Graphics.Graphics.peasantEnemy_Right : Graphics.Graphics.peasantEnemy_Left;
            //Returns graphic so the enemy is looking in the correct location
        }
    }
}
