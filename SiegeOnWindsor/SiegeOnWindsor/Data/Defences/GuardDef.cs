using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Data.Defences
{
    public class GuardDef : Defence
    {
        public GuardDef()
        {
            
        }


        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override Textures.Texture GetGraphic()
        {
            return Textures.guardDef;
        }
    }

}
