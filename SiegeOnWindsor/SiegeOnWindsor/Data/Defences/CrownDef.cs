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
            this.Health = 5000; //Sets the health of the defence
        }

        public override void Update(GameTime gameTime)
        {
           //Nothing to do here
        }

        public override int GetBaseRiskValue()
        {
            return 0; //Returns a risk of zero as the crown is the eventual goal for enemies so it would be bad to deter enemies away by applying a risk
        }

        public override void Die()
        {
            this.Tile.World.IsRunning = true; //Makes the game end
            base.Die(); //Destroys the object
        }
    }
}
