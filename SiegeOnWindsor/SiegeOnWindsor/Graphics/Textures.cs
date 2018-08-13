using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Graphics
{
    public class Textures
    {
        public static Texture emptyTile;
        public static Texture nullTile;

        public static void Load(SiegeGame game)
        {
            emptyTile = new Texture("empty_tile", game);
            nullTile = new Texture("null_tile", game);
        }

        public class Texture
        {
            public Texture2D Sprite { set; get; }
            public String Name { get; }

            public Texture(String s, SiegeGame game)
            {
                this.Name = s;
                game.loadBuffer.Add(this);
            }
        }
    }
}
