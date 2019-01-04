using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SiegeOnWindsor.Data.Defences
{
    public class CatapultDef : Defence
    {
        public CatapultDef() : base(Graphics.Graphics.catapultDef)
        {
            this.Health = 700; //Sets the health

            this.Cost = 1000;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
