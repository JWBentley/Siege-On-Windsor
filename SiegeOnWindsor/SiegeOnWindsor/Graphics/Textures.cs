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
        //List of textures

        //Backgrounds
        public static Texture menuBackground;
        public static Texture gameBackground;

        //UI
        public static Texture defencePanelUI;

        //Tiles
        public static Texture emptyTile;
        public static Texture spawnTile;

        //Defences
        public static Texture crownDef;
        public static Texture stoneWallDef;
        public static Texture guardDef;

        //Enemies
        public static Texture peasantEnemy_Left, peasantEnemy_Right;

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="game">Game with content manager</param>
        public static void Load(SiegeGame game)
        {
            menuBackground = new Texture("Backgrounds/menu_screen", game);
            gameBackground = new Texture("Backgrounds/game_screen", game);

            defencePanelUI = new Texture("UI/Panels/defence_panel", game);

            emptyTile = new Texture("Tile/empty_tile", game);
            spawnTile = new Texture("Tile/null_tile", game);

            crownDef = new Texture("Tile/Defence/crown_defence", game);
            stoneWallDef = new Texture("Tile/Defence/stone_wall_defence", game);
            guardDef = new Texture("Tile/Defence/guard_defence", game);

            peasantEnemy_Left = new Texture("Enemies/peasant_enemy_left", game);
            peasantEnemy_Right = new Texture("Enemies/peasant_enemy_right", game);
            //testAnimation = new Animation(new String[] { "Enemies/Left", "Enemies/Right" }, game);
        }

        public class Texture
        {
            /// <summary>
            /// Image to be drawn
            /// </summary>
            public Texture2D Sprite { set; get; }
            /// <summary>
            /// String ref
            /// </summary>
            public String Name { get; }

            public Texture(String s, SiegeGame game)
            {
                this.Name = s;
                game.LoadBuffer.Add(this); //Calls for the game to load the image from the string ref
            }
        }

        /*
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
        */
    }
}
