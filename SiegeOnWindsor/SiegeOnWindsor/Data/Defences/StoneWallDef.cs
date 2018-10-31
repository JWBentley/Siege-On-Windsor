using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Data.Defences
{
    public class StoneWallDef : Defence
    {
        /// <summary>
        /// Creates a new wall defence
        /// </summary>
        public StoneWallDef() : base(Textures.stoneWallDef)
        {
            this.Health = 1000; //Sets the health
        }
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
