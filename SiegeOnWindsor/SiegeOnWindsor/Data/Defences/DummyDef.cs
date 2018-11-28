using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Data.Defences
{
    public class DummyDef : Defence
    {
        public DummyDef(Graphics.Graphics.Graphic g) : base(g)
        {
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
