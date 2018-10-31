using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
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
        public PeasantEnemy(World w) : base(w)
        {
            this.Health = 200;
            this.Speed = 70;
            this.Damage = 20;
            this.AttackCooldown = 20;
        }

        public override Texture GetGraphic()
        {
            return this.Path.Count > 0 ? this.Location.X > this. Path.Peek().X ? Textures.peasantEnemy_Left : Textures.peasantEnemy_Right : Textures.peasantEnemy_Left;
        }
    }
}
