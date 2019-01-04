using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SiegeOnWindsor.Data.Defences
{
    public class ArcherDef : Defence
    {
        public ArcherDef() : base(Graphics.Graphics.archerDef)
        {
            this.Health = 350; //Sets the health

            this.Cost = 750;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
