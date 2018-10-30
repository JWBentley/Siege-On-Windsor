using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Defences
{
    public class CrownDef : Defence
    {
        public CrownDef() : base(Textures.crownDef)
        {
            this.Health = 5000;
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Die()
        {
            this.Tile.World.HasEnded = true;
            base.Die();
        }
    }
}
