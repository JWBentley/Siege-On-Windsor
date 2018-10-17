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
        public static Texture menuBackground;
        public static Texture gameBackground;

        public static Texture defencePanelUI;

        public static Texture emptyTile;
        public static Texture nullTile;
        public static Texture crownTile;

        public static Animation testAnimation;

        public static void Load(SiegeGame game)
        {
            menuBackground = new Texture("Backgrounds/menu_screen", game);
            gameBackground = new Texture("Backgrounds/game_screen", game);

            defencePanelUI = new Texture("UI/Panels/defence_panel", game);

            emptyTile = new Texture("Tile/empty_tile", game);
            nullTile = new Texture("Tile/null_tile", game);
            crownTile = new Texture("Tile/Defence/crown_defence", game);

            testAnimation = new Animation(new String[] { "Enemies/Left", "Enemies/Right" }, game);
        }

        public class Texture
        {
            public Texture2D Sprite { set; get; }
            public String Name { get; }

            public Texture(String s, SiegeGame game)
            {
                this.Name = s;
                game.LoadBuffer.Add(this);
            }
        }

        public class Animation
        {
            Texture[] Sprites;
            int currentFrame = 0;

            public Animation(Animation a)
            {
                this.Sprites = a.Sprites;
            }

            public Animation(String[] s, SiegeGame game)
            {
                this.Sprites = new Texture[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    this.Sprites[i] = new Texture(s[i], game);
                }
            }

            public Texture GetFrame()
            {
                return this.Sprites[this.currentFrame];
            }

            public void NextFrame()
            {
                if (this.Sprites != null)
                {
                    if (this.currentFrame < this.Sprites.Length - 1)
                        this.currentFrame++;
                    else
                        this.currentFrame = 0;
                }
            }
        }
    }
}
