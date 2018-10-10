using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Data.Defences
{
    public class WoodWallDef : Defence
    {
        public WoodWallDef() : base(Textures.nullTile)
        {
            this.Health = 1000;
        }
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
